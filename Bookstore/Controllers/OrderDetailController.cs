using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepo orderDetailRepo;

        public OrderDetailController(IOrderDetailRepo orderDetailRepo)
        {
            this.orderDetailRepo = orderDetailRepo;
        }

        [HttpGet]
        public async Task<ActionResult> getOrdersPerUser(string id)
        {
            List<OrdersPerUser> OrdersPerUser = await orderDetailRepo.getOrdersPerUser(id);
            if(OrdersPerUser != null) { 
               
                return Ok(OrdersPerUser);
            }
            else return NotFound();
        }
    }
}
