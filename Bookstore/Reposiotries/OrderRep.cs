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
        public List<Order> getOrders()
        {
           // return db.Orders.Include(a=>a.AppUser.firstName).ToList();
            return db.Orders.ToList();
        }

        //add
        public Order add(Order order)
        {
            db.Orders.Add(order);
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
    }
}
