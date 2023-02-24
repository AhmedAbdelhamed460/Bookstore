﻿using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult getall()
        {
            List<Category> categories = rep.getall();
            List<CategoryBookDTO> categoryBookDTOs = new List<CategoryBookDTO>();
            foreach (Category c in categories)
            {
                CategoryBookDTO categoryBook = new CategoryBookDTO()
                {
                    categoryId = c.Id,
                    name = c.Name,
                };
                foreach (Book b in c.Books)
                {
                    categoryBook.booksName.Add(b.Title);
                }
                categoryBookDTOs.Add(categoryBook);
            }
            return Ok(categoryBookDTOs);
        }
        [HttpGet("{id:int}")]
        public ActionResult getbyid(int id)
        {
            Category c = rep.getbyid(id);
            CategoryBookDTO bookDTO = new CategoryBookDTO()
            {
                categoryId = c.Id,
                name = c.Name
            };
            foreach (Book b in c.Books)
            {
                bookDTO.booksName.Add(b.Title);
            }
            return Ok(bookDTO);
        }

        [HttpGet("{name:alpha}")]

        public ActionResult getbyname(string name)
        {
            Category c = rep.getbyname(name);
            CategoryBookDTO bookDTO = new CategoryBookDTO()
            {
                categoryId = c.Id,
                name = c.Name
            };
            foreach (Book b in c.Books)
            {
                bookDTO.booksName.Add(b.Title);
            }
            return Ok(bookDTO);

        }

        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category c = rep.Add(category);
                    CategoryBookDTO categoryBookDTO = new CategoryBookDTO()
                    {
                        categoryId=c.Id,
                        name=c.Name
                    };
                    foreach (Book b in c.Books)
                    {
                        categoryBookDTO.booksName.Add(b.Title);
                    }
                    return Ok(categoryBookDTO);
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
        public ActionResult update(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category au = rep.update(id, category);
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