using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Reposiotries
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly BookStoreDbContext _dbContext;
        public ReviewRepo(BookStoreDbContext dbContext)
        {
                
            _dbContext = dbContext;
        }

        public async Task<Review> AddReview(Review review)
        {
            await _dbContext.AddAsync(review);
            _dbContext.SaveChanges();
            return review;
        }

        public Review DeleteReview(Review review)
        {
            _dbContext.Remove(review);
            _dbContext.SaveChanges();
            return review;
        }

        public Review EditReview(Review review)
        {
            _dbContext.Update(review);
            _dbContext.SaveChanges();
            return review;
        }

        public async Task<List<Review>> getAll(int bookID)
        {
            return await _dbContext.Reviews.Where(n => n.bookID == bookID)
                .OrderBy(n => n.Rateing)
                .Include(n=>n.Book).Include(n => n.AppUser)
                .ToListAsync();
        }

        public async Task<Review?> GetbyId(int id)
        {
            return await _dbContext.Reviews.Include(n=>n.AppUser).Include(n=>n.Book).SingleOrDefaultAsync(n=>n.Id == id);
        }
    }
}
