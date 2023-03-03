using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Bookstore.Models
{
    public class Order
    {
       
        public int Id { get; set; }

        //public int Number { get; set; }

        [Column(TypeName ="money")]
        public double Shopingcost { get; set; }

        [DataType(DataType.Date)]
        public DateTime ShopingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        public double Discount { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual List<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
