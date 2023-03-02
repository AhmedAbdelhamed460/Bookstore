using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IOrderRep
    {
        Task <List<Order>> getOrders();
        Task<Order?> getById(int id);

        Task add(Order order);
        Order update(Order order);
        Order deleteOrder( int orderid);
       
    }
}
