namespace Bookstore.DOT
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Describtion { get; set; }
        public string poster { get; set; }
        public double Price { get; set; }
        public int Page { get; set; }
        public DateTime PublisherDate { get; set; }
        public string AuthorFirstname { get; set; }
        public string AuthorLastname { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
        public string? MainCategory { get; set; }

    }
}
