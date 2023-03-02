namespace Bookstore.DOT
{
    public class ShopingCartDTO
    {
        public string userId { get; set; }
        public int bookId { get; set; } 
        public int amount { get; set; } = 1;
    }
}
