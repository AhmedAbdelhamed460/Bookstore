using Microsoft.EntityFrameworkCore;
using System;

namespace Bookstore.Models
{
    public class applicationdbcontext :DbContext
    {
        public applicationdbcontext(DbContextOptions<applicationdbcontext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopingCart>().HasKey("customrId", "bookId");
        }
        public  DbSet<Book> Books { get; set; }
        public  DbSet<Author> Author { get; set; }
        public  DbSet<category> Category { get; set; }
        public  DbSet<Customer> Customer { get; set; }
        public  DbSet<Order> Order { get; set; }
        public  DbSet<Publisher> Publisher { get; set; }
        public  DbSet<Review> Reviews { get; set; }
        public DbSet<ShopingCart> shopingCarts { get; set; }


    }
}
