using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class AuthorRepository:IAuthorRepository
    {
        BookStoreDbContext db;
        public AuthorRepository(BookStoreDbContext db)
        {
            this.db = db;
        }

        //getall
        public List<Author> getAll()
        {
            return db.Authors.Include(n => n.Books).ToList();
        }

        //getbyid
        public Author getById(int id)
        {
            return db.Authors.Include(b=>b.Books).FirstOrDefault(a=>a.Id==id);
        }

        public Author getbyname(string name)
        {
            return db.Authors.Include(n => n.Books).FirstOrDefault(d=>d.Firstname==name);
        }

        public Author Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();
            author = db.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == author.Id);
            return author;
        }
        //update
        public Author update(int id, Author author)
        {
            db.Entry(author).State = EntityState.Modified;
            db.SaveChanges();
            return author;
        }
        //delete
        public Author deleteAuthor(int id)
        {
            Author a= db.Authors.Find(id);
            db.Authors.Remove(a);
            db.SaveChanges();
           return a;
        }
        //getall top authores

        public List<Author> TopAuthores()
        {
            return db.Authors.Where(b => b.Top == true).Include(b=>b.Books).ToList();
        }
    }
}
