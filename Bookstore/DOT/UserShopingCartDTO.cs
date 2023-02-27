using Bookstore.Models;

namespace Bookstore.DOT
{
    public class UserShopingCartDTO
    {
        public string UserId { get; set; }
        public Dictionary<int, int> bookIdAmount { get; set; } = new Dictionary<int, int>();


    }
}
