using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Book
    {
        public int id { get; set; }
        [StringLength(200)]
        public string Describtion { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        [StringLength(50)]
        public string? Title { get; set; }
        [Column(TypeName ="money")]
        public double Price { get; set; }
        //[StringLength(100)]
        //public string Feedback { get; set; }
        public int Page { get; set; }

        //[StringLength(50)]
        //public string Publisher { get; set; }
        //[StringLength(50)]
        //public string Author { get; set; }
        [Column(TypeName = "date")]
        public DateTime PublisherDate { get; set; }

        //public int Book_N { get; set; }

        //relation 
        [ForeignKey("author")]
        public int AuthorID { get; set; }
        public virtual Author? author { get; set; }
        [ForeignKey("category")]
        public int CategoryID { get; set; }
        public virtual category category { get; set; }
        [ForeignKey("publisher")]
        public int PublisherID { get; set; }
        public virtual Publisher publisher { get; set; }

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
       
        

    }
}
