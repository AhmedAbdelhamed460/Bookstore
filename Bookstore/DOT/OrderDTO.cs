namespace Bookstore.DOT
{
    public class OrderDTO
    {
        public int orderId { get; set; }
        public string? AppUserId { get; set; }
        public double Shopingcost { get; set; }
        public DateTime ShopingDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double Discount { get; set; }
    }
}
