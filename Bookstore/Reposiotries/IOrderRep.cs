using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IOrderRep
    {
        Task <List<Order>> getOrders();
        //Task<Order?> getById(int id);
        Order add(Order order);
        Order update(Order order, string userid);
        Order deleteOrder( int orderid);
        List<MostUsersHavOrdersDTO> getMostUsersHavOrders();

    }
}
