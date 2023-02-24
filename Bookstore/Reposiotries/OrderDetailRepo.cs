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

        public async Task<List<OrdersPerUser>> getOrdersPerUser(string id)
        { 

            var orderDetails = await (from o in dbContext.orderDetails
                                where o.Order.AppUserId == id
                                group o.bookId by o.orderId into g
                                select new { odrerId = g.Key, Books = g.ToList() }).ToListAsync();

            List<OrdersPerUser> ordersPerUsers = new List<OrdersPerUser>();
            foreach (var orderDetail in orderDetails)
            {
                OrdersPerUser ordersPerUser = new OrdersPerUser();
                ordersPerUser.orderId = orderDetail.odrerId;
                ordersPerUser.bookId.AddRange(orderDetail.Books);

                ordersPerUsers.Add(ordersPerUser);

            }

            return ordersPerUsers;
        }

    }
}

