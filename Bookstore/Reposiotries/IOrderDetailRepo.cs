using Bookstore.DOT;

namespace Bookstore.Reposiotries
{
    public interface IOrderDetailRepo
    {
        Task<List<OrdersPerUserDTO>> getOrdersPerUser(string id);
    }
}