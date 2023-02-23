﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Rateing { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string ReviewText { get; set; }

        [ForeignKey("Book")]
        public int bookID { get; set; }
        public virtual Book Book { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        
    }
}
