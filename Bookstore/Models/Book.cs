﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }
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
        [ForeignKey("Author")]
        public int authorID { get; set; }
        public virtual Author? Author { get; set; }

        [ForeignKey("Category")]
        public int categoryID { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Publisher")]
        public int publisherID { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
       
        

    }
}
