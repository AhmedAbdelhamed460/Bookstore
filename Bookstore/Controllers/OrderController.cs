using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderRep rep;
        public OrderController(IOrderRep rep)
        {
            this.rep = rep;
        }
        [HttpGet]
        public ActionResult getorders()
        {
            List<Order> orders = rep.getOrders();
            List<OrderDTO> ordersDTO = new List<OrderDTO>();
            foreach(Order item in orders)
            {
                OrderDTO dTO = new OrderDTO()
                {
                    orderid=item.Id,
                    ShopingDate=item.ShopingDate,
                    Shopingcost=item.Shopingcost,
                    ArrivalDate=item.ArrivalDate,
                    Discount=item.Discount,
       
                };
                ordersDTO.Add(dTO);
            }
            return Ok(ordersDTO);
        }
        [HttpPost]
        public ActionResult add(Order order)
        {
            Order o = rep.add(order);
            OrderDTO orderdto = new OrderDTO()
            {
                Shopingcost = o.Shopingcost,
                ShopingDate = o.ShopingDate,
                ArrivalDate = o.ArrivalDate,
                Discount = o.Discount,
            };
            return Ok(orderdto);
        }
        [HttpPut]
        public ActionResult update(Order order,string userid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order o = rep.update(order, userid);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else return BadRequest();
        }

        [HttpDelete]
        public ActionResult deleteOrder(int orderid)
        {
            Order order = rep.deleteOrder( orderid);
            if (order == null) { return NotFound(); }
            else
            {
                return Ok(order);
            }
        }
    }
}
