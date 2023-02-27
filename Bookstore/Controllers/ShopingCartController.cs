using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopingCartController : ControllerBase
    {
        private readonly IShopingCartrRepo shopingCartrRepo;
        private readonly IBookRepo bookRepo;

        public ShopingCartController(IShopingCartrRepo shopingCartrRepo, IBookRepo bookRepo) 
        {
            this.shopingCartrRepo = shopingCartrRepo;
            this.bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<ActionResult> getByUserId(string userId)
        {
            UserShopingCartDTO userShopingCartDTO = await shopingCartrRepo.getByUserId(userId);
            List<ShopingCartBooksDTO> shopingCartBooksDTOs = new List<ShopingCartBooksDTO>();
            if (userShopingCartDTO.UserId != null)
            {
                foreach (KeyValuePair<int, int> item in userShopingCartDTO.bookIdAmount)
                {
                    Book book = await bookRepo.getById(item.Key);
                    shopingCartBooksDTOs.Add(new ShopingCartBooksDTO()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Describtion,
                        //Image = book.Image,
                        Price = book.Price,
                        Page = book.Page,
                        PublisherDate = book.PublisherDate,
                        Author = $"{book.Author.Firstname} {book.Author.Lastname}",
                        Category = book.Category.Name,
                        Publisher = book.Publisher.Name,
                        Amount = item.Value
                    }); 
                }
         
                return Ok(shopingCartBooksDTOs);
            }
            else return NotFound();    
        }

        [HttpPost]
        public async Task<ActionResult> add(ShopingCartDTO ShopingCartDTO)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    ShopingCart shopingCart = new ShopingCart()
                    {
                        AppUserId = ShopingCartDTO.userId,
                        bookId = ShopingCartDTO.bookId,
                        Amount = ShopingCartDTO.amount
                    };
                    await shopingCartrRepo.add(shopingCart);
                    shopingCart = await shopingCartrRepo.getByUserIdBookID(shopingCart.AppUserId,shopingCart.bookId);
                    ShopingCartDTO shopingCartDTO = new ShopingCartDTO()
                    {
                        userId = shopingCart.AppUserId,
                        bookId = shopingCart.bookId,
                        amount = shopingCart.Amount                       
                    };
                    return Ok(shopingCartDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
            else return BadRequest();
        }
    }
}
