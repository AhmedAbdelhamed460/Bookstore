using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class CategoryRepository:ICategoryRepository
    {
        BookStoreDbContext db;
        public CategoryRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
        //getall
        public List<Category> getall()
        {
            return db.Categorys.Include(b=>b.Books).ToList();
        }

        //getbyid

        public Category getbyid(int id) 
        {
            return db.Categorys.Include(b => b.Books).FirstOrDefault(c => c.Id == id);
        }

        //getbyname
        public Category getbyname(string name)
        {
            return db.Categorys.Include(b => b.Books).FirstOrDefault(s => s.Name == name);
        }

        //add
        public Category Add(Category category)
        {
            db.Categorys.Add(category);
            db.SaveChanges();
            category = db.Categorys.Include(b => b.Books).SingleOrDefault(a => a.Id == category.Id);
            return category;
        }

        //update
        public Category update(int id, Category category)
        {
            db.Entry(category).State = EntityState.Modified;
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
