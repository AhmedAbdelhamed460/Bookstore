using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRep rep;
        private readonly IShopingCartrRepo shopingCartrRepo;
        private readonly IOrderDetailRepo orderDetailRepo;
        private readonly IbooksRepo booksRepo;

        public OrderController(IOrderRep rep, IShopingCartrRepo shopingCartrRepo, IOrderDetailRepo orderDetailRepo, IbooksRepo booksRepo )
        {
            this.rep = rep;
            this.shopingCartrRepo = shopingCartrRepo;
            this.orderDetailRepo = orderDetailRepo;
            this.booksRepo = booksRepo;
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
                    orderId = item.Id,
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
        public async Task<ActionResult> add(OrderDTO orderDTO)
        {

            Order? order = new Order()
            {
                Shopingcost = orderDTO.Shopingcost,
                ShopingDate = orderDTO.ShopingDate,
                ArrivalDate = orderDTO.ArrivalDate,
                Discount = orderDTO.Discount,
                AppUserId = orderDTO.AppUserId
            };
            order = rep.add(order);
            order = await rep.getById(order.Id);
            OrderToReturnDTO orderToReturnDTO = new OrderToReturnDTO()
            {
                orderId = order.Id,
                AppUserId = order.AppUserId,
                Shopingcost = order.Shopingcost,
                ShopingDate = order.ShopingDate,
                ArrivalDate = order.ArrivalDate,
                Discount = order.Discount,
                UserName = order.AppUser.UserName,
                
                
            };
            return Ok(orderToReturnDTO);
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

        [HttpPost("/api/orderNow")]
        public async Task<ActionResult> orderNow(OrderNowDTO orderNowDTO )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order order = new Order()
                    {
                        Shopingcost = orderNowDTO.Shopingcost,
                        ShopingDate = orderNowDTO.ShopingDate,
                        ArrivalDate = orderNowDTO.ArrivalDate,
                        Discount = orderNowDTO.Discount,
                        AppUserId = orderNowDTO.AppUserId

                    };
                    order = rep.add(order);
                    UserShopingCartDTO userShopingCartDTO = await shopingCartrRepo.getByUserId(order.AppUserId);

                    foreach (KeyValuePair<int, int> item in userShopingCartDTO.bookIdAmount)
                    {
                        Book book = await booksRepo.getById(item.Key);
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            orderId = order.Id,
                            bookId = item.Key,
                            Quantity = item.Value,
                            Price = book.Price
  
                        }; 
                        await orderDetailRepo.add(orderDetail);
                    }
                   
                    return Ok(userShopingCartDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }
            }
            else return BadRequest();
        }
    }
}
