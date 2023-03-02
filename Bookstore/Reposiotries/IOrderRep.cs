using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IOrderRep
    {
        Task <List<Order>> getOrders();
        Task<Order> GetById(int id);
        Task<Order> add(Order order);
      
        Order update(Order order);
        public Order deleteOrder( int orderid);
       
    }
}
