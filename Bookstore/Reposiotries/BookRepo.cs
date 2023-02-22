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
        public Task<List<Book>> getAll()
        {
            return dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).ToListAsync();

        }

        public Task<Book?> getById(int id)
        {
            return dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == id);
        }
        public Task<Book?> getByName(string title)
        {
            return dbContext.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category).SingleOrDefaultAsync(b => b.Title == title);
        }

        public  int add(Book book)
        {
            try
            {
                dbContext.Books.Add(book);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int edit(Book book)
        {
            try
            {
                dbContext.Entry(book).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
