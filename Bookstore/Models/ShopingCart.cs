using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    [Index(nameof(AppUserId), IsUnique = false)]
    [Index(nameof(bookId), IsUnique = false)]
    public class ShopingCart
    {
        //need copmosit PK of This 2 FK : Fluent Api =>   modelBuilder.Entity<ShopingCart>().HasKey("customrId", "bookId");
        public int Amount { get; set; }

        [ForeignKey("AppUser")]
        
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        
        [ForeignKey("Book")]
        public int bookId { get; set; }
        public virtual Book? Book { get; set; }

    }
}
