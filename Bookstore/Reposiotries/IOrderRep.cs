using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IOrderRep
    {
         public List<Order> getOrders();
        public Order add(Order order);
        public Order update(Order order, string userid);
        public Order deleteOrder( int orderid);
    }
}
