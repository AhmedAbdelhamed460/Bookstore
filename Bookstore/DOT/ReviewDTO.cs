namespace Bookstore.DOT
{
    public class ReviewDTO
    {
        public double Rateing { get; set; }
        public DateTime Date { get; set; }
        public string ReviewText { get; set; }
        public int bookID { get; set; }
        public string AppUserId { get; set; }
    }
}
