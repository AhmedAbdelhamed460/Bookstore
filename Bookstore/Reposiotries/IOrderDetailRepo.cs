using Bookstore.DOT;

namespace Bookstore.Reposiotries
{
    public interface IOrderDetailRepo
    {
        Task<List<OrdersPerUser>> getOrdersPerUser(string id);
    }
}