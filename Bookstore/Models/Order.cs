using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Order
    {
       
        public int Id { get; set; }

        [Column(TypeName ="money")]
        public double Shopingcost { get; set; }

        [DataType(DataType.Date)]
        public DateTime shopingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime arrivalDate { get; set; }
        public double discount { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual Customer Customer { get; set; }


    }
}
