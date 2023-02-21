using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }
        public virtual List<Book> Books { get; set; }


    }
}
