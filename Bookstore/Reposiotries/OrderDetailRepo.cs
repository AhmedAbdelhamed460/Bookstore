using Bookstore.DOT;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        private readonly BookStoreDbContext dbContext;

        public OrderDetailRepo(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<OrdersPerUserDTO>> getOrdersPerUser(string id)
        { 

            var orderDetails = await (from o in dbContext.orderDetails
                                where o.Order.AppUserId == id
                                group o.bookId by o.orderId into g
                                select new { odrerId = g.Key, Books = g.ToList() }).ToListAsync();

            List<OrdersPerUserDTO> ordersPerUsers = new List<OrdersPerUserDTO>();
            foreach (var orderDetail in orderDetails)
            {
                OrdersPerUserDTO ordersPerUser = new OrdersPerUserDTO();
                ordersPerUser.orderId = orderDetail.odrerId;
                ordersPerUser.bookId.AddRange(orderDetail.Books);

                ordersPerUsers.Add(ordersPerUser);

            }

            return ordersPerUsers;
        }

    }
}

