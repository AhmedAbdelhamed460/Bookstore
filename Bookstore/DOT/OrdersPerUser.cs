namespace Bookstore.DOT
{
    public class OrdersPerUser
    {
        public int orderId { get; set; }
        public List<int> bookId { get; set; } = new List<int>();
    }
}
