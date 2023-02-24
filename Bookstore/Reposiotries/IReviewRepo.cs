using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IReviewRepo
    {
        Task<Review> AddReview(Review review);
        Review EditReview(Review review);
        Review DeleteReview (Review review);
        Task<Review?> GetbyId(int id);
        Task<List<Review>> getAll(int bookID);


    }
}
