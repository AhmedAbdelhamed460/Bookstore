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

        //public Task add(Book book)
        //{  
        //        dbContext.Books.AddAsync(book);
        //        return dbContext.SaveChangesAsync();
        //}
        public async Task edit(Book book)
        {   
              dbContext.Entry(book).State = EntityState.Modified;
              await dbContext.SaveChangesAsync();
        }
    }
}
