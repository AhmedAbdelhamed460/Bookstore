using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IBookRepo
    {
        Task add(Book book);
        Task edit(Book book);
        Task<List<Book>> getAll();

        List<OrderDetailDTO> getBestSeller();


        Task<List<Book>> getByNewArrival();
        Task<List<Book>> getAllByCategoryName(string CategoryName);
        List<Book> getBestSeller();

        Task<Book?> getById(int id);
        Task<Book?> getByName(string title);
    }
}