using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class ShopingCart
    {
        //need copmosit PK of This 2 FK : Fluent Api =>   modelBuilder.Entity<ShopingCart>().HasKey("customrId", "bookId");
        public int Amount { get; set; }

        [ForeignKey("Customer")]
        public int customrId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Book")]
        public int bookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
