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
        public async Task<Order> GetById(int id)
        {
            return await db.Orders.SingleOrDefaultAsync(o => o.Id == id);
        }
        //getall
        public async Task <List<Order>> getOrders()
        {
            return await db.Orders.ToListAsync();
        }
        public async Task<Order?> getById(int id)
        {
            return await db.Orders.Include(o => o.AppUser)
                  .Include(o => o.OrderDetails)
                  .SingleOrDefaultAsync(o => o.Id == id);
        }
        //add
        public async Task add(Order order)
        {
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

        }
        //public async Task add(Order order,string userid)
        //{


        //}
        public Order update(Order order)
        {
            db.Orders.Update(order);
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

       
    }
}
