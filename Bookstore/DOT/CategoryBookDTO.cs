namespace Bookstore.DOT
{
    public class CategoryBookDTO
    {
        public int categoryId { get; set; }
        public string name { get; set; }
        public List<string> booksName { get; set; } = new List<string>();
    }
}
