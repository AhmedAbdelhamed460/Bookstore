using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IbooksRepo
    {
        Task<List<Book>> getAll();
        Task<List<Book>> getByNewArrival();
        double sumAllPrice();



        Task<List<Book>> getByMainCategory(string MainCategory);
        Task<List<Book>> getByMainCategoryByCategoryname(string MainCategory ,string Categoryname);


        Task<List<Book>> getAllbyPriceDescending();
        Task<List<Book>> getAllbyPriceAescending();
        List<OrderDetailDTO> getBestSeller();
        Task<Book?> getByName(string name);
        Task<Book?> getById(int id);
        Task<Book> add(Book book);
        Book edit(Book book);
        Book DeleteBook(Book book);
        Task<List<Book>> decreaseinstock();


    }
}
