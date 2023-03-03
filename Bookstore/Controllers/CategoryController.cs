using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository rep;
        public CategoryController(ICategoryRepository rep)
        {
            this.rep = rep;
        }

        //getall
        [HttpGet]
        public async Task<ActionResult> getall()
        {
            List<Category> categories = await rep.getall();
            List<CategoryBookDTO> categoryBookDTOs = new List<CategoryBookDTO>();
            foreach (Category c in categories)
            {
                CategoryBookDTO categoryBook = new CategoryBookDTO()
                {
                    categoryId = c.Id,
                    name = c.Name,
                };

                categoryBookDTOs.Add(categoryBook);
            }
            return Ok(categoryBookDTOs);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> getbyid(int id)
        {
            var category = await rep.getbyid(id);
            if (category == null)
                return NotFound($"no category Found with Id {id}");
            CategoryBookDTO categoryBookDTO = new CategoryBookDTO()
            {
                categoryId = category.Id,
                name = category.Name,
            };
            return Ok(categoryBookDTO);

        }

        [HttpGet("{name:alpha}")]

        public async Task<ActionResult> getbyname(string name)
        {
            var category = await rep.getbyname(name);
            if (category == null)
                return NotFound($"no category Found with name {name}");
            CategoryBookDTO categoryBookDTO = new CategoryBookDTO()
            {
                categoryId = category.Id,
                name = category.Name,
            };
            return Ok(categoryBookDTO);

        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryBookDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category()
                    {
                        // Id=dto.categoryId,
                        Name = dto.name
                    };
                    rep.Add(category);
                    return Ok(dto);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }

            }
            else { return BadRequest(); }
        }
        //update
        [HttpPut]
        public async Task<ActionResult> update(CategoryBookDTO dto, int id)
        {
            var category = await rep.getbyid(id);
            if (category == null) return NotFound($"no category with id {id}");

            category.Name = dto.name;

            rep.update(category);
            return Ok("Update completed successfully");
        }

        //delete
        [HttpDelete]
        public ActionResult deletecategory(int id)
        {
            Category c = rep.deletecategory(id);
            if (c == null) { return NotFound(); }
            else
            {
                return Ok(c);
            }
        }
    }
}
