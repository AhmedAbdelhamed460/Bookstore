using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Bookstore.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IAuthorRepository rep;
        public AuthorsController(IAuthorRepository rep)
        {
            this.rep = rep;
           
        }

        [HttpPost]
       
        public async Task<IActionResult> add( AuthorAddDto dto)
        {
            //using var dataStream = new MemoryStream();

            //await dto.Image.CopyToAsync(dataStream);
            if (ModelState.IsValid)
            {
                try
                {
                    var author = new Author()
                    {
                        Bio = dto.Bio,
                        Firstname = dto.Firstname,
                        Lastname = dto.Lastname,
                        Image = dto.Image,

                    };
                    await rep.add(author);
                    return Ok(dto);
                }catch(Exception ex) { return BadRequest(ex); }
            }
            return BadRequest();

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> getALL()
        {
            List<Author> authorList = await rep.getAll();
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

       // [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task <IActionResult> getById(int id)
        {
            Author au = await rep.getById(id);
            AuthorBookDTO authorBookDTO = new AuthorBookDTO()
            {
                authorId = au.Id,
                Firstname = au.Firstname,
                Lastname = au.Lastname,
                 Image = au.Image,
                Bio = au.Bio,

            };
            foreach (Book bb in au.Books)
            {
                authorBookDTO.bookName.Add(bb.Title);
            }

            return Ok(authorBookDTO);
        }

        //[AllowAnonymous]
        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            Author au = await rep.GetByName(name);
            AuthorBookDTO authorBookDTO = new AuthorBookDTO()
            {
                authorId = au.Id,
                Firstname = au.Firstname,
                Lastname = au.Lastname,
                     Image = au.Image,
                Bio = au.Bio,

            };
            foreach (Book b in au.Books)
            {
                authorBookDTO.bookName.Add(b.Title);
            }
            return Ok(authorBookDTO);

        }

        //delete

        [HttpDelete]
        public async Task< ActionResult> deleteAuthor(int id)
        {
            Author au = await rep.getById(id);
            if (au == null) { return NotFound(); }
            else
            {
                rep.Delete(au);
                return Ok(au);
            }

        }

        //edite
        [HttpPut("{id}")]
        public async Task<IActionResult> edit(int id,  AuthorAddDto dTO)
        {
            var author = await rep.getById(id);
            if (author == null) return NotFound($"no book with id {id}");
            //using var dataStream = new MemoryStream();
            //await dTO.Image.CopyToAsync(dataStream);
            if (ModelState.IsValid)
            {
                try
                {
                    author.Bio = dTO.Bio;
                    author.Firstname = dTO.Firstname;
                    author.Lastname = dTO.Lastname;
                    author.Image = dTO.Image;
                    rep.edit(author);
                    return Ok(dTO);
                }catch(Exception ex) { return BadRequest(ex.Message); }
            }
            else { return BadRequest(); }   
        }


    }
}
