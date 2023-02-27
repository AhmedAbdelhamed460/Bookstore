using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IAuthorRepository
    {
        Task<List<Author>> getAll();
        Task<Author?> getById(int id);
        Task<Author?> GetByName(string name);
        Task<Author> add(Author author);
        Author edit(Author author);
        Author Delete(Author author);

    }
}
