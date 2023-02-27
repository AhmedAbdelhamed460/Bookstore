﻿using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IShopingCartrRepo
    {
        Task add(ShopingCart shopingCart);
        Task delete(int bookId);
        Task edit(ShopingCart shopingCart);
        //Task<List<ShopingCart>> getByUserId(string userId);
        Task<UserShopingCartDTO> getByUserId(string userId);

       Task<ShopingCart> getByUserIdBookID(string userId, int bookId);

    }
}