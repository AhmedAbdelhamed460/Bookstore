using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IBookRepo
    {
        int add(Book book);
        int edit(Book book);
        Task<List<Book>> getAll();
        Task<Book?> getById(int id);
        Task<Book?> getByName(string title);
    }
}