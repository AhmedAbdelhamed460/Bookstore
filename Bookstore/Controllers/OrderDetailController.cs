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

            List<OrdersPerUserDTO> ordersPerUserDTOs = await orderDetailRepo.getOrdersPerUser(id);
            if(ordersPerUserDTOs.Count !=0) { 
               
                return Ok(ordersPerUserDTOs);
            }
            else return NotFound();
        }
    }
}
