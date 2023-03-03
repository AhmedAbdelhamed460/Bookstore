 using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class BookStoreDbContext : IdentityDbContext<AppUser>
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopingCart>().HasKey(s => new { s.AppUserId,s.bookId});          
            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.orderId, o.bookId });

            base.OnModelCreating(modelBuilder);
        }
        public  DbSet<Book> Books { get; set; }
        public  DbSet<Author> Authors { get; set; }
        public  DbSet<Category> Categorys { get; set; }
        public  DbSet<AppUser> AppUsers { get; set; }
        public  DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public  DbSet<Review> Reviews { get; set; }
        public DbSet<ShopingCart> shopingCarts { get; set; }



    }
}
