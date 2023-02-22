namespace Bookstore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }= new List<Book>();
    }
}
