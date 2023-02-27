namespace Bookstore.DOT
{
    public class BookDTO
    {

       
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile poster { get; set; }
        public double Price { get; set; }
        public int Page { get; set; }
        public DateTime PublisherDate { get; set; }
        public int AuthorID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int PublisherID { get; set; }
        public int CategoryID { get; set; }
        public string MainCategory { get; set; }

    }
}
