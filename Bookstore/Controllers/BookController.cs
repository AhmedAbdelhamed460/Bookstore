﻿using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                        Title = book.Title,
                        Description = book.Describtion,
                        Image = book.Image,
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
                    Title = book.Title,
                    Description = book.Describtion,
                    Image = book.Image,
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
                    Title = book.Title,
                    Description = book.Describtion,
                    Image = book.Image,
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
        public ActionResult add(Book book)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    bookRepo.add(book);
                    BookDTO bookDTO = new BookDTO()
                    {
                        Title = book.Title,
                        Description = book.Describtion,
                        Image = book.Image,
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

    }
}
