using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class PublisherRepository: IPublisherRepository
    {
        BookStoreDbContext db;
        public PublisherRepository(BookStoreDbContext db)
        {
            this.db = db;
        }

        public List<Publisher> GetAll()
        {
            return db.Publishers.Include(b=>b.Books).ToList();
        }

        public Publisher GetById(int id) 
        {
            return db.Publishers.Include(b => b.Books).FirstOrDefault(p => p.Id == id);
        }

        public Publisher getbyname(string name)
        {
            return db.Publishers.Include(b => b.Books).FirstOrDefault(p => p.Name == name);
        }

        public Publisher Add(Publisher publisher)
        {
            db.Publishers.Add(publisher);
            db.SaveChanges();
            publisher=db.Publishers.Include(b=>b.Books).SingleOrDefault(d=>d.Id == publisher.Id);
            return publisher;
        }

        public Publisher Update(int id, Publisher publisher)
        {
            db.Entry(publisher).State = EntityState.Modified;
            db.SaveChanges();
            return publisher;
        }


        public Publisher deletePublisher(int id)
        {
            Publisher p = db.Publishers.Find(id);
            db.Publishers.Remove(p);
            db.SaveChanges();
            return p;
        }
    }
}
