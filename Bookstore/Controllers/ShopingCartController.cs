using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                       
                        Title = book.Title,
                        Describtion = book.Describtion,
                        poster = book.poster,
                        Price = book.Price,
                        Page = book.Page,
                        PublisherDate = book.PublisherDate,
                        //Author = $"{book.Author.Firstname} {book.Author.Lastname}",
                        AuthorFirstname = book.Author.Firstname,
                        AuthorLastname = book.Author.Lastname,
                        ArrivalDate = book.ArrivalDate,
                        CategoryName = book.Category.Name,
                        PublisherName = book.Publisher.Name,
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


        [HttpPut]
        public async Task<ActionResult> edit(ShopingCartDTO ShopingCartDTO)
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
                    await shopingCartrRepo.edit(shopingCart);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
            else return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int bookId, string userId)
        {
            if(bookId !=null && userId != null)
            {
                await shopingCartrRepo.delete(bookId, userId);
                return Ok($"shoping card with bookId{bookId} and userId{userId} deleted");
            }
            else return BadRequest();

        }
        [HttpDelete("/api/deleteAllShopingCart")]
        public async Task<IActionResult> deleteAll(string userId)
        {
            if (userId != null)
            {
                await shopingCartrRepo.deleteAll(userId);
                return Ok($"All Shoping Card With UserId {userId} Deleted");
            }
            else return BadRequest();
        }

    }
}
