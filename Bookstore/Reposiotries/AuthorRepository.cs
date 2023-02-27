using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class AuthorRepository:IAuthorRepository
    {
     private readonly   BookStoreDbContext db;
        public AuthorRepository(BookStoreDbContext db)
        {
            this.db = db;
        }

        //getall
        //public List<Author> getAll()
        //{
        //    return db.Authors.Include(n => n.Books).ToList();
        //}

        ////getbyid
        //public Author getById(int id)
        //{
        //    return db.Authors.Include(b=>b.Books).FirstOrDefault(a=>a.Id==id);
        //}

        //public Author getbyname(string name)
        //{
        //    return db.Authors.Include(n => n.Books).FirstOrDefault(d=>d.Firstname==name);
        //}

        ////public async Task<Author> add(Author author)
        ////{
        ////    db.Authors.Add(author);
        ////    db.SaveChanges();
        ////    author = db.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == author.Id);
        ////    return author;
        ////}
        ////update
        //public Author update(int id, Author author)
        //{
        //    db.Entry(author).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return author;
        //}
        ////delete
        //public Author deleteAuthor(int id)
        //{
        //    Author a= db.Authors.Find(id);
        //    db.Authors.Remove(a);
        //    db.SaveChanges();
        //   return a;
        //}

        //public async Task<Author> add(Author author)
        //{
        //    await db.AddAsync(author);
        //    db.SaveChanges();
        //    return author;
        //}

        public async Task<List<Author>> getAll()
        {
         return   await db.Authors.Include(n => n.Books).ToListAsync();
            
        }

        public async Task<Author?> getById(int id)
        {
         return   await db.Authors.Include(n => n.Books).SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Author?> GetByName(string name)
        {
            return await db.Authors.Include(n => n.Books).SingleOrDefaultAsync(n => n.Firstname == name);
        }

        public Author edit(Author author)
        {
            db.Authors.Update(author);
            db.SaveChanges();
            return author;
        }

        public Author Delete(Author author)
        {
            db.Remove(author);
            db.SaveChanges();
            return author;
        }

        public async Task<Author> add(Author author)
        {
            await db.AddAsync(author);
            db.SaveChanges();
            return author;
        }
    }
}
