﻿using Bookstore.DOT;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class ShopingCartrRepo : IShopingCartrRepo
    {
        private readonly BookStoreDbContext dbContext;

        public ShopingCartrRepo(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<List<ShopingCart>> getAll()
        //{
        //    return await dbContext.shopingCarts.Include(sc => sc.Book).Include(sc => sc.AppUser).ToListAsync();

        //}
        public async Task<UserShopingCartDTO> getByUserId(string userId)
        {
            //return await dbContext.shopingCarts.Include(sc => sc.Book).Include(sc => sc.AppUser).Where(sc => sc.AppUserId == userId).ToListAsync();
            var shopingCarts = await dbContext.shopingCarts.Include(sc => sc.Book).Include(sc => sc.AppUser).Where(sc => sc.AppUserId == userId).GroupBy(sc=> sc.AppUserId).ToListAsync();
            UserShopingCartDTO userShopingCart = new UserShopingCartDTO();
            foreach(var group in shopingCarts) 
            {
                userShopingCart.UserId = group.Key;
                foreach(var shopingCart in group)
                {
                    userShopingCart.bookIdAmount.Add(shopingCart.bookId, shopingCart.Amount);
                }
            }
            return (userShopingCart);

        }
        public async Task<ShopingCart> getByUserIdBookID(string userId ,int bookId)
        {
            return await dbContext.shopingCarts.Include(sc => sc.Book).Include(sc => sc.AppUser).SingleOrDefaultAsync(sc => sc.AppUserId == userId && sc.bookId == bookId );
        }
        public async Task add(ShopingCart shopingCart)
        {
            await dbContext.shopingCarts.AddAsync(shopingCart);
            await dbContext.SaveChangesAsync();
        }
        public async Task edit(ShopingCart shopingCart)
        {
            dbContext.Entry(shopingCart).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
        public async Task delete(int bookId)
        {
            ShopingCart? shopingCart = await dbContext.shopingCarts.SingleOrDefaultAsync(sc => sc.bookId == bookId);
            dbContext.shopingCarts.Remove(shopingCart);
            await dbContext.SaveChangesAsync();
        }
    }
}
