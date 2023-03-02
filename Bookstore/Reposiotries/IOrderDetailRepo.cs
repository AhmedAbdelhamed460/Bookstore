using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IOrderDetailRepo
    {
        Task add(OrderDetail orderDetail);
        Task<List<OrdersPerUserDTO>> getOrdersPerUser(string id);
    }
}