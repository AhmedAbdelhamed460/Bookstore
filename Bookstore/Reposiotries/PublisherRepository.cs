using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class PublisherRepository : IPublisherRepository
    {
        BookStoreDbContext db;
        public PublisherRepository(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Publisher>> GetAll()
        {
            return await db.Publishers.Include(b => b.Books).ToListAsync();
        }

        public async Task<Publisher> GetById(int id)
        {
            return await db.Publishers.Include(b => b.Books).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Publisher> getbyname(string name)
        {
            return await db.Publishers.Include(b => b.Books).SingleOrDefaultAsync(p => p.Name == name);
        }
        public async Task<Publisher> Add(Publisher publisher)
        {
            await db.AddAsync(publisher);
            db.SaveChanges();
            return publisher;
        }

        public Publisher update(Publisher publisher)
        {
            db.Publishers.Update(publisher);
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
