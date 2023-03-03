namespace Bookstore.DOT
{
    public class PublisherBookDTO
    {
        public int publisherId { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public List<string> booksName { get; set; } = new List<string>();
    }
}
