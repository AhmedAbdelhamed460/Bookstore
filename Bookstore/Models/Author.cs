
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bookstore.Models
{
    public class Author
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Firstname { get; set; }
        [StringLength(50)]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Image { get; set; }
        public string Bio { get; set; }

        public virtual List<Book>? Books { get; set; }=new List<Book>();
    }
}
