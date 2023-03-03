using Bookstore.DOT;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class OrderRep: IOrderRep
    {
        private readonly BookStoreDbContext db;
        public OrderRep(BookStoreDbContext db)
        {
            this.db = db;
        }
        //getall
        public async Task<List<Order>> getOrders()
        {
           // return db.Orders.Include(a=>a.AppUser.firstName).ToList();
            return await db.Orders.ToListAsync();
        }

        //add
        public Order add(Order order)
        {
            db.Orders.AddAsync(order);
            db.SaveChanges();
            return order;
        }

        public Order update(Order order, string userid)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return order;
        }

        public Order deleteOrder(int orderid)
        {
            Order order = db.Orders.Find(orderid);
            db.Orders.Remove(order);
            db.SaveChanges();
            return order;
        }

        public List<MostUsersHavOrdersDTO> getMostUsersHavOrders()
        {

            var orders =  db.Orders.GroupBy(o => o.AppUserId)
                                    .Select(group => new {
                                     userId = group.Key,
                                     Count = group.Count()
                                     })
                                    .OrderByDescending(x => x.Count);

            List<MostUsersHavOrdersDTO> getOrdersPerUserDTOs = new List<MostUsersHavOrdersDTO>();
            foreach (var order in orders)
            {
                getOrdersPerUserDTOs.Add(new MostUsersHavOrdersDTO() 
                {
                    UserId = order.userId,
                    Count = order.Count
                });        
            }
            return getOrdersPerUserDTOs;
        }
    }
}
