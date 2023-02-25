using Bookstore.DOT;
using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Reposiotries;
namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo bookRepo;

        public BookController(IBookRepo bookRepo) 
        {
            this.bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<ActionResult> getAll()
        {
            List<Book> books = await bookRepo.getAll();
            List<BookDTO> bookDTOs = new List<BookDTO>();
            if (books != null) {
                foreach (var book in books)
                {
                    bookDTOs.Add(new BookDTO()
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
                        Publisher = book.Publisher.Name
                    });
                }
                return Ok(bookDTOs);
            }
            else return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> getById(int id)
        {
            Book? book = await bookRepo.getById(id);
            if (book != null) 
            {
                BookDTO bookDTO = new BookDTO()
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
                    Publisher = book.Publisher.Name
                };

                return Ok(bookDTO);
            }
            else return NotFound();
        }
        [HttpGet("/api/bookname/{name}")]
        public async Task<ActionResult> getByName(string name)
        {
            Book? book = await bookRepo.getByName(name);
            if (book != null)
            {
                BookDTO bookDTO = new BookDTO()
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
                    Publisher = book.Publisher.Name
                };

                return Ok(bookDTO);
            }
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> add(Book book)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    await bookRepo.add(book);
                    book = await bookRepo.getById(book.Id);
                    BookDTO bookDTO = new BookDTO()
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
                        Publisher = book.Publisher.Name
                    };
                    return Ok(bookDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
            else return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> edit(Book book)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    await bookRepo.edit(book);
                    book = await bookRepo.getById(book.Id);
                    BookDTO bookDTO = new BookDTO()
                    {
                        Title = book.Title,
                        Description = book.Describtion,
                        //Image = book.Image,
                        Price = book.Price,
                        Page = book.Page,
                        PublisherDate = book.PublisherDate,
                        Author = $"{book.Author.Firstname} {book.Author.Lastname}",
                        Category = book.Category.Name,
                        Publisher = book.Publisher.Name
                    };
                    return Ok(bookDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
            else return BadRequest();
        }

        [HttpGet("/api/bookBestSeller")]
        public async Task<ActionResult> getBestSellerAsync()
        {
            //List<Book> books = bookRepo.getBestSeller();

            List<OrderDetailDTO> orderDetailDTOs = bookRepo.getBestSeller();
            List<BookDTO> bookDTOs = new List<BookDTO>();
            if (orderDetailDTOs != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    Book? book = await bookRepo.getById(orderDetailDTOs[i].BookID);
                    BookDTO bookDTO = new BookDTO()
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

