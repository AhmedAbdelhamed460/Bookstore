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

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await rep.GetById(id);
            if (order == null)
                return NotFound($"no order Found with Id {id}");
            OrderDTO orderDTO = new OrderDTO()
            {
                Id = order.Id,
                //number = order.Number,
                Shopingcost = order.Shopingcost,
                ShopingDate = order.ShopingDate,
                ArrivalDate = order.ArrivalDate,
                Discount = order.Discount,
                AppUserId = order.AppUserId
            };
            return Ok(orderDTO);
        }
        [HttpGet]
        public async Task <ActionResult>getorders()
        {
            List<Order> orders =await rep.getOrders();
            List<OrderDTO> ordersDTO = new List<OrderDTO>();
            foreach(Order item in orders)
            {
                OrderDTO dTO = new OrderDTO()
                {
                    Id =item.Id,
                   // number=item.Number,
                    ShopingDate=item.ShopingDate,
                    Shopingcost=item.Shopingcost,
                    ArrivalDate=item.ArrivalDate,
                    Discount=item.Discount,
                    AppUserId=item.AppUserId
       
                };
                ordersDTO.Add(dTO);
            }
            return Ok(ordersDTO);
        }
        [HttpPost]
        public async Task<ActionResult> add(OrderDTO dTO)
        {

            var order = new Order()
            {
               // Id = dTO.Id,
                Shopingcost = dTO.Shopingcost,
                ShopingDate = dTO.ShopingDate,
                ArrivalDate = dTO.ArrivalDate,
                Discount = dTO.Discount,
                AppUserId = dTO.AppUserId
            };
            rep.add(order);
            return Ok(dTO);
        }
        [HttpPut]
        public async Task<ActionResult> update(OrderDTO dTO, int id)
        {

            var order = await rep.GetById(id);
            if (order == null) return NotFound($"no order with id {id}");

            order.Id = dTO.Id;
            order.Shopingcost = dTO.Shopingcost;
            order.ShopingDate = dTO.ShopingDate;
            order.ArrivalDate = dTO.ArrivalDate;
            order.Discount = dTO.Discount;
            order.AppUserId = dTO.AppUserId;


            rep.update(order);
            return Ok("Update completed successfully");
        }
        [HttpDelete]
        public ActionResult deleteOrder(int orderid)
        {
            Order order = rep.deleteOrder(orderid);
            if (order == null) { return NotFound(); }
            else
            {
                return Ok(order);
            }
        }

    }
}
