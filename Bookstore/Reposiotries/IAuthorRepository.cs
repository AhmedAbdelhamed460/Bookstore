using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> getAll();
        public Task <Author> getById(int id);
        public Task<Author> getbyname(string name);
        public Task<Author> Add(Author author);
        public Task<Author> update(int id, Author author);
        public Task<Author> deleteAuthor(int id);
        public Task<List<Author>> TopAuthores();

    }
}
