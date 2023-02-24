using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Reposiotries;
using Bookstore.Models;
using Bookstore.DOT;
using Bookstore.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Bookstore.Responses;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IAuthorRepository rep;
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorRepository rep, IAuthorService authorService)
        {
            this.rep = rep;
            this.authorService = authorService;
        }

        //gatAll authors
        [HttpGet]
        public async Task<ActionResult>getAll()
        {
            List<Author> authorList =await rep.getAll();
            //dto
            List<AuthorBookDTO> authorBooks = new List<AuthorBookDTO>();
            foreach (Author item in authorList)
            {
                AuthorBookDTO bookDTO = new AuthorBookDTO()
                {
                    authorId = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Image = item.Image,
                    Bio = item.Bio,
                    Top=item.Top
                };
                foreach (Book bb in item.Books)
                {
                    bookDTO.bookName.Add(bb.Title);
                }
                authorBooks.Add(bookDTO);
            }
            return Ok(authorBooks);
        }

        //getbyid
        [HttpGet("{id:int}")]
        public async Task<ActionResult> getById(int id)
        {
            Author au =await rep.getById(id);
            AuthorBookDTO authorBookDTO = new AuthorBookDTO()
            {
                authorId = au.Id,
                Firstname = au.Firstname,
                Lastname = au.Lastname,
                Image = au.Image,
                Bio = au.Bio,
                Top=au.Top
            };
            foreach (Book bb in au.Books)
            {
                authorBookDTO.bookName.Add(bb.Title);
            }

            return Ok(authorBookDTO);
        }

        //getbyname
        [HttpGet("{name:alpha}")]
        public async Task<ActionResult> getbyname(string name)
        {
            Author au =await rep.getbyname(name);
            AuthorBookDTO authorBookDTO = new AuthorBookDTO()
            {
                authorId = au.Id,
                Firstname = au.Firstname,
                Lastname = au.Lastname,
                Image = au.Image,
                Bio = au.Bio,
                Top=au.Top
            };
            foreach (Book b in au.Books)
            {
                authorBookDTO.bookName.Add(b.Title);
            }
            return Ok(authorBookDTO);
        }
        //add
        [HttpPost]
        public async Task<ActionResult> Add(Author a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Author author =await rep.Add(a);
                    AuthorBookDTO authorBookDTO = new AuthorBookDTO()
                    {
                        authorId = author.Id,
                        Firstname = author.Firstname,
                        Lastname = author.Lastname,
                        Image = author.Image,
                        Bio = author.Bio,
                        Top=author.Top

                    };
                    foreach (Book b in author.Books)
                    {
                        authorBookDTO.bookName.Add(b.Title);
                    }
                    return Ok(authorBookDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return BadRequest();
        }
        //update
        [HttpPut]
        public async Task<ActionResult> update(int id, Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Author au =await rep.update(id, author);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return BadRequest();
        }
        //delete
        [HttpDelete]
        public async Task<ActionResult> deleteAuthor(int id)
        {
            Author au =await rep.deleteAuthor(id);
            if (au == null) { return NotFound(); }
            else
            {
                return Ok(au);
            }
        }
        //get top author
        [HttpGet("TopAuthores")]
        public async Task<ActionResult> TopAuthores()
        {

            List<Author> a =await rep.TopAuthores();
            List<AuthorBookDTO> authorBooks = new List<AuthorBookDTO>();
            foreach (Author item in a)
            {
                AuthorBookDTO bookDTO = new AuthorBookDTO()
                {
                    authorId = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Image = item.Image,
                    Bio = item.Bio,
                    Top = item.Top

                };
                foreach (Book bb in item.Books)
                {
                    bookDTO.bookName.Add(bb.Title);
                }
                authorBooks.Add(bookDTO);
            }
            return Ok(authorBooks);
        }
        //uploadimage
        [HttpPost]
        [Route("uploadphoto")]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> uploadphoto([FromForm] AuthorBookDTO postRequest)
        {
            if (postRequest == null)
            {
                return BadRequest(new AuthorResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });
            }
            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
            {
                return BadRequest(new AuthorResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
            }
            if (postRequest.Image != null)
            {
                await authorService.SavePostImageAsync(postRequest);
            }
            var postResponse = await authorService.CreatePostAsync(postRequest);
            if (!postResponse.Success)
            {
                return NotFound(postResponse);
            }
            return Ok(postResponse.author_);
        
        }

    }
}
