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
        private readonly IPublisherRepository rep;
        public PublisherController(IPublisherRepository rep)
        {
            this.rep = rep;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Publisher> publishers = await rep.GetAll();
            List<PublisherBookDTO> listpublisherDTO = new List<PublisherBookDTO>();

            foreach (Publisher p in publishers)
            {
                PublisherBookDTO publisherDTO = new PublisherBookDTO()
                {
                    publisherId = p.Id,
                    name = p.Name,
                    location = p.Location
                };

                listpublisherDTO.Add(publisherDTO);

            }
            return Ok(listpublisherDTO);
        }

        //getbyid
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var publisher = await rep.GetById(id);
            if (publisher == null)
                return NotFound($"no publisher Found with Id {id}");
            PublisherBookDTO publisherBookDTO = new PublisherBookDTO()
            {
                publisherId = publisher.Id,
                name = publisher.Name,
                location = publisher.Location
            };
            return Ok(publisherBookDTO);
        }

        //getbyname
        [HttpGet("{name:alpha}")]
        public async Task<ActionResult> getbyname(string name)
        {
            var publisher = await rep.getbyname(name);
            if (publisher == null)
                return NotFound($"no publisher Found with name {name}");
            PublisherBookDTO publisherBookDTO = new PublisherBookDTO()
            {
                // publisherId = publisher.Id,
                name = publisher.Name,
                location = publisher.Location
            };
            return Ok(publisherBookDTO);
        }

        //add
        [HttpPost]
        public async Task<IActionResult> Add(PublisherBookDTO dTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var publisher = new Publisher()
                    {
                        // Id=dTO.publisherId,
                        Name = dTO.name,
                        Location = dTO.location
                    };
                    rep.Add(publisher);
                    return Ok(dTO);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }

            }
            else { return BadRequest(); }
        }
        //update
        [HttpPut]
        public async Task<ActionResult> update(int id, Publisher dto)
        {
            var publisher = await rep.GetById(id);
            if (publisher == null) return NotFound($"no publisher with id {id}");

            //  publisher.Id = dto.publisherId;
            publisher.Name = dto.Name;
            publisher.Location = dto.Location;

            rep.update(publisher);
            return Ok("Update completed successfully");
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
