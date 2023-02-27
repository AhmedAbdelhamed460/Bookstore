using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        IPublisherRepository rep;
        public PublisherController(IPublisherRepository rep)
        {
            this.rep = rep;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            List<Publisher> publishers = rep.GetAll();
            List<PublisherBookDTO> listpublisherDTO = new List<PublisherBookDTO>();

            foreach (Publisher p in publishers)
            {
                PublisherBookDTO publisherDTO = new PublisherBookDTO()
                {
                    publisherId = p.Id,
                    name = p.Name,
                    location = p.Location
                };
                foreach (Book bb in p.Books)
                {
                    publisherDTO.booksName.Add(bb.Title);
                }
                listpublisherDTO.Add(publisherDTO);

            }
            return Ok(listpublisherDTO);
        }

        //getbyid
        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            Publisher p = rep.GetById(id);
            PublisherBookDTO publisherDTO = new PublisherBookDTO()
            {
                publisherId = p.Id,
                name = p.Name,
                location = p.Location
            };
            foreach (Book b in p.Books)
            {
                publisherDTO.booksName.Add(b.Title);
            }
            return Ok(publisherDTO);
        }

        //getbyname
        [HttpGet("{name:alpha}")]
        public ActionResult getbyname(string name)
        {
            Publisher p = rep.getbyname(name);
            PublisherBookDTO publisherDTO = new PublisherBookDTO()
            {
                publisherId = p.Id,
                name = p.Name,
                location = p.Location
            };
            foreach (Book b in p.Books)
            {
                publisherDTO.booksName.Add(b.Title);
            }
            return Ok(publisherDTO);
        }

        //add
        [HttpPost]
        public ActionResult Add(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Publisher p = rep.Add(publisher);
                    PublisherBookDTO publisherBookDTO = new PublisherBookDTO()
                    {
                        publisherId = p.Id,
                        name = p.Name,
                        location = p.Location
                    };
                    foreach (Book b in p.Books)
                    {
                        publisherBookDTO.booksName.Add(b.Title);
                    }
                    return Ok(publisherBookDTO);
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
        public ActionResult Update(int id, Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Publisher p = rep.Update(id, publisher);
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
        public ActionResult deletePublisher(int id)
        {
            Publisher p = rep.deletePublisher(id);
            if (p == null) { return NotFound(); }
            else
            {
                return Ok(p);
            }
        }
    }
}
