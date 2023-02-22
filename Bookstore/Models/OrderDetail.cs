using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class OrderDetail
    {
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public double Price { get; set; }

        [ForeignKey("Order")]
        public int orderId { get; set; }
        public virtual Order Order  { get; set; }

        [ForeignKey("Book")]
        public int bookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
