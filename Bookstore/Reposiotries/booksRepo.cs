using Bookstore.DOT;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class booksRepo : IbooksRepo
    {
        private readonly BookStoreDbContext dbContext;

        public booksRepo(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> add(Book book)
        {
            await dbContext.AddAsync(book);
            dbContext.SaveChanges();
            return book;
        }

        public Book DeleteBook(Book book)
        {
            dbContext.Remove(book);
            dbContext.SaveChanges();
            return book;
        }

        public Book edit(Book book)
        {
            dbContext.Update(book);
            dbContext.SaveChanges();
            return book;
        }

        public async Task<List<Book>> getAll()
        {
            return await dbContext.Books
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                .ToListAsync();
            
        }

        public async Task<List<Book>> getAllbyPriceAescending()
        {
            return await dbContext.Books
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                 .OrderBy(n => n.Price)
                
                .ToListAsync();
        }

        public async Task<List<Book>> getAllbyPriceDescending()
        {
            return await dbContext.Books
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                 .OrderByDescending(n => n.Price)

                .ToListAsync();

        }

        public List<OrderDetailDTO> getBestSeller()
        {
            var oredDetails = dbContext.orderDetails.Include(od => od.Book)
                             .GroupBy(od => od.bookId)
                             .Select(od => new { book = od.Key, bestSeller = od.Sum(od => od.Quantity) }).ToList();
            List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();

            foreach (var orderDetail in oredDetails)
            {
                orderDetailDTOs.Add(new OrderDetailDTO()
                {
                    BookID = orderDetail.book,
                    BestSeller = orderDetail.bestSeller
                });
            }
            return orderDetailDTOs;
        }

        public async Task<Book?> getById(int id)
        {
          return await dbContext.Books.Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                .SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Book>> getByMainCategory(string MainCategory)
        {
            return await dbContext.Books
               .Include(n => n.Category)
               .Include(n => n.Author)
               .Include(n => n.Publisher)
               .Where(n=>n.MainCategory == MainCategory)
               .ToListAsync();
        }

        public async Task<List<Book>> getByMainCategoryByCategoryname(string MainCategory, string Categoryname)
        {
            return await dbContext.Books
               .Include(n => n.Category)
               .Include(n => n.Author)
               .Include(n => n.Publisher)
               .Where(n => n.MainCategory == MainCategory && n.Category.Name == Categoryname)
               .ToListAsync();
        }

        public async Task<Book?> getByName(string name)
        {
            return await dbContext.Books.Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                .SingleOrDefaultAsync(n => n.Title == name);
        }

        public async Task<List<Book>> getByNewArrival()
        {
            return await dbContext.Books
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Include(n => n.Publisher)
                .OrderByDescending(n => n.ArrivalDate)
     
                .ToListAsync();
        }

        public  double sumAllPrice()
        {
           return   dbContext.Books.Sum(n=>n.Price);
        }

        public async Task<List<Book>> decreaseinstock()
        {
            return await dbContext.Books
                  .Include(n => n.Category)
                  .Include(n => n.Author)
                  .Include(n => n.Publisher)
                  .Where(n => n.stock <= 3)
                  .ToListAsync();
        }
    }
}
