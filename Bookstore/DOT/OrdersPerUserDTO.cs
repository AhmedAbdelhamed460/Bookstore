namespace Bookstore.DOT
{
    public class OrdersPerUserDTO
    {
        public int orderId { get; set; }
        public List<int> bookId { get; set; } = new List<int>();
    }
}
