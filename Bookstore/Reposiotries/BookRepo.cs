using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.DOT;
using System.Web.Http.ModelBinding;

namespace Bookstore.Reposiotries
{
    public class BookRepo : IBookRepo
    {
        private readonly BookStoreDbContext dbContext;

        public BookRepo(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Book>> getAll()
        {
            return await dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).ToListAsync();

        }

        public async Task<Book?> getById(int id)
        {
            return await dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Book?> getByName(string title)
        {
            return await dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).SingleOrDefaultAsync(b => b.Title == title);
        }

        public async Task add(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task edit(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public List<OrderDetailDTO> getBestSeller()
        {
            var oredDetails = dbContext.orderDetails.Include(od => od.Book)
                              .GroupBy(od => od.bookId)
                              .Select(od => new { book = od.Key, bestSeller = od.Sum(od => od.Quantity) }).ToList();
            List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();
           
            foreach (var orderDetail in oredDetails)
            { 
                orderDetailDTOs.Add(new OrderDetailDTO()
                {
                    BookID = orderDetail.book,
                    BestSeller = orderDetail.bestSeller
                });
            }
            return orderDetailDTOs;
        }

    
    



        public async Task<List<Book>> getAllByCategoryName(string CategoryName)
        {
            return await dbContext.Books.Where(n => n.Category.Name == CategoryName).Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).ToListAsync();
        }

        public async Task<List<Book>> getByNewArrival()
        {
            return await dbContext.Books.OrderByDescending(n=>n.ArrivalDate)
                .Include(b => b.Author).Include(b => b.Publisher)
                .Include(b => b.Category).ToListAsync();

        }

        
    }
}
