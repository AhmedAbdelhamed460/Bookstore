using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Rateing { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [MaxLength(200)]
        public string review { get; set; }

        [ForeignKey("Book")]
        public int bookID { get; set; }
        public virtual Book Book { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual Customer Customer { get; set; }
        
    }
}
