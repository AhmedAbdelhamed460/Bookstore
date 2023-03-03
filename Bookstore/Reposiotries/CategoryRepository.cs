using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class CategoryRepository : ICategoryRepository
    {
        BookStoreDbContext db;
        public CategoryRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
        //getall
        public async Task<List<Category>> getall()
        {
            return await db.Categorys.Include(b => b.Books).ToListAsync();
        }

        //getbyid

        public async Task<Category> getbyid(int id)
        {
            return await db.Categorys.Include(b => b.Books).SingleOrDefaultAsync(c => c.Id == id);
        }

        //getbyname
        public async Task<Category> getbyname(string name)
        {
            return await db.Categorys.Include(b => b.Books).SingleOrDefaultAsync(s => s.Name == name);
        }

        //add
        public async Task<Category> Add(Category category)
        {
            await db.AddAsync(category);
            db.SaveChanges();
            return category;
        }

        //update
        public Category update(Category category)
        {
            db.Categorys.Update(category);
            db.SaveChanges();
            return category;
        }
        //delete
        public Category deletecategory(int id)
        {
            Category c = db.Categorys.Find(id);
            db.Categorys.Remove(c);
            db.SaveChanges();
            return c;
        }


    }
}
