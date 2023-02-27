using AutoMapper;
using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly IbooksRepo bookRepo;
        private readonly IMapper mapper;

        public booksController(IbooksRepo bookRepo, IMapper mapper)
        {
            this.bookRepo = bookRepo;
            this.mapper = mapper;
        }


        // create book

        [HttpPost]
        public async Task<ActionResult> add([FromForm] BookDTO dTO)
        {
            using var dataStream = new MemoryStream();

            await dTO.poster.CopyToAsync(dataStream);
            var book = new Book()
            {
                ArrivalDate = dTO.ArrivalDate,
                authorID = dTO.AuthorID,
                Describtion = dTO.Description,
                Title = dTO.Title,
                Page = dTO.Page,
                Price = dTO.Price,
                categoryID = dTO.CategoryID,
                publisherID = dTO.PublisherID,
                PublisherDate = dTO.PublisherDate,
                poster = dataStream.ToArray(),
            };
            bookRepo.add(book);
            return Ok(book);
        }

        //GetALL
        [HttpGet]
        public async Task<ActionResult> getAll()
        {
            var book = await bookRepo.getAll();
            //
            var data =mapper.Map<IEnumerable<BookDetailsDto>>(book);

            return Ok(data);
        }

        // Get By ID 

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var book = await bookRepo.getById(id);
            if (book == null)
            {
                return NotFound();
            }

           var dto = mapper.Map<BookDetailsDto>(book);
            return Ok(dto);
        }

        [HttpGet("/api/bookname/{name}")]
        public async Task<ActionResult> getByName(string name)
        {
            var book = await bookRepo.getByName(name);
            if (book == null)
            {
                return NotFound();
            }

           var dto=mapper.Map<BookDetailsDto>(book);
            return Ok(dto);
        }





        [HttpGet("getByNewArrival")]
        public async Task<ActionResult> getByNewArrival()
        {
            var book = await bookRepo.getByNewArrival();
             var data =mapper.Map<IEnumerable<BookDetailsDto>>(book);

            return Ok(data);
        }


        [HttpGet("getAllbyPriceDescending")]
        public async Task<ActionResult> getAllbyPriceDescending()
        {
            var book = await bookRepo.getAllbyPriceDescending();
            var data = mapper.Map<IEnumerable<BookDetailsDto>>(book);

            return Ok(data);
        }
        [HttpGet("getAllbyPriceAescending")]
        public async Task<ActionResult> getAllbyPriceAescending()
        {
            var book = await bookRepo.getAllbyPriceAescending();
            return Ok(book);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = await bookRepo.getById(id);
            if (book == null) return NotFound($"no book with id {id}");
            bookRepo.DeleteBook(book);
            return Ok(book);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> edit(int id , [FromForm] BookDTO dTO)
        {
            var Book = await  bookRepo.getById(id);
            if (Book == null) return NotFound($"no book with id {id}");
            using var dataStream = new MemoryStream();

            await dTO.poster.CopyToAsync(dataStream);

            Book.ArrivalDate = dTO.ArrivalDate;
            Book.Title = dTO.Title;
            Book.Price = dTO.Price;
            Book.authorID = dTO.AuthorID;
            Book.categoryID= dTO.CategoryID;
            Book.publisherID= dTO.PublisherID;
            Book.Describtion = dTO.Description;
            Book.PublisherDate= dTO.PublisherDate;
            Book.Page= dTO.Page;
            Book.poster = dataStream.ToArray();

           bookRepo.edit(Book);
            return Ok(Book);
        }





        [HttpGet("/api/bookBestSeller")]
        public async Task<ActionResult> getBestSellerAsync()
        {
            //List<Book> books = bookRepo.getBestSeller();

            List<OrderDetailDTO> orderDetailDTOs = bookRepo.getBestSeller();
            List<BookBestDto> bookDTOs = new List<BookBestDto>();
            if (orderDetailDTOs != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    Book? book = await bookRepo.getById(orderDetailDTOs[i].BookID);
                    BookBestDto bookDTO = new BookBestDto()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Describtion,
                        Image = book.poster,
                        Price = book.Price,
                        Page = book.Page,
                        PublisherDate = book.PublisherDate,
                        Author = $"{book.Author.Firstname} {book.Author.Lastname}",
                        Category = book.Category.Name,
                        Publisher = book.Publisher.Name
                    };
                    bookDTOs.Add(bookDTO);
                }
                return Ok(bookDTOs);
            }
            else return NotFound();
        }






    }
}
