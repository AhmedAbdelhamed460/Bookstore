using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {



        private readonly IReviewRepo _repo;
        public ReviewController(IReviewRepo repo)
        {

            _repo = repo;
        }

        [HttpGet("GetbyBookID")]
        public async Task<IActionResult> GetbyBookID(int bookID)
        {
            List<Review> reviews = await _repo.getAll(bookID);
            List<ReviewBook> reviewBooks = new List<ReviewBook>();
            if (reviews == null)
            {
                foreach (Review item in reviews)
                {

                    ReviewBook reviewBook = new ReviewBook()
                    {
                        bookID = item.bookID,
                        Rateing = item.Rateing,
                        Date = item.Date,
                        ReviewText = item.ReviewText,
                        UserName = $"{item.AppUser.firstName}{item.AppUser.LastName}",
                         appUserId = item.appUserID

                    };
                    reviewBooks.Add(reviewBook);
                }
                return Ok(reviewBooks);
            }
            else { return BadRequest(); }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id )
        {
            var Review = await _repo.GetbyId(id);
            if (Review == null)
                return NotFound($"no Review Found with Id {id}");
            ReviewBook reviewBook = new ReviewBook()
            {
                bookID = Review.bookID,
                Rateing = Review.Rateing,
                Date = Review.Date,
                ReviewText = Review.ReviewText,
                UserName = $"{Review.AppUser.firstName} {Review.AppUser.LastName}",
                appUserId = Review.appUserID
            };
            return Ok(reviewBook);
          
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(  ReviewDTO dTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Review = new Review()
                    {
                        bookID = dTO.bookID,
                        Rateing = dTO.Rateing,
                        Date = dTO.Date,
                        ReviewText = dTO.ReviewText,
                        appUserID = dTO.AppUserId,
                    };
                    _repo.AddReview(Review);
                    return Ok(dTO);
                } catch (Exception ex) { return BadRequest(ex.Message); }
               
            }
            else { return BadRequest(); }
        }


        [HttpPut("Id")]
        public async Task<IActionResult> EditReview(int id ,ReviewDTO dTO)
        {
            var Review = await _repo.GetbyId(id);
            if (Review == null)
                return NotFound($"no Review Found with Id {id}");
                Review.ReviewText = dTO.ReviewText;
                Review.Rateing = dTO.Rateing;
                Review.Date = dTO.Date;
                Review.bookID = dTO.bookID;
                Review.appUserID = dTO.AppUserId;
               _repo.EditReview(Review);
                return Ok(Review);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteReview(int id )
        {
            var Review = await _repo.GetbyId(id);
            if (Review == null)
                return NotFound($"no Review Found with Id {id}");
           _repo.DeleteReview(Review);
            return Ok(Review);
        }



       
    }
}










//public ActionResult AddOrUpdateReview(int userId, int productId, string review)
//{
//    var query = _context.ReviewsOnProducts.Where(R => R.UserId == userId && R.ProductId == productId).SingleOrDefault();
//    if (query != null)
//    {
//        query.Description = review;
//        _context.SaveChanges();
//        return Ok("Review Updated");
//    }
//    else
//    {
//        ReviewsOnProduct reviewsOnProduct = new ReviewsOnProduct()
//        {
//            UserId = userId,
//            ProductId = productId,
//            Description = review

//        };
//        _context.ReviewsOnProducts.Add(reviewsOnProduct);
//        _context.SaveChanges();
//        return Ok("Review Made");
//    }
//}