using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IBookRepo bookRepo;
        private readonly UserManager<AppUser> userManager;

        public OrderController(IOrderRep rep, IShopingCartrRepo shopingCartrRepo, IOrderDetailRepo orderDetailRepo, IBookRepo bookRepo, UserManager<AppUser> userManager)
        {
            this.rep = rep;
            this.shopingCartrRepo = shopingCartrRepo;
            this.orderDetailRepo = orderDetailRepo;
            this.bookRepo = bookRepo;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult> getorders()
        {
            List<Order> orders =await rep.getOrders();
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
        public  ActionResult add(Order order)
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
                        AppUserId = orderNowDTO.UserId

                    };
                    order = rep.add(order);
                    UserShopingCartDTO userShopingCartDTO = await shopingCartrRepo.getByUserId(order.AppUserId);

                    foreach (KeyValuePair<int, int> item in userShopingCartDTO.bookIdAmount)
                    {
                        Book book = await bookRepo.getById(item.Key);
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

        [HttpGet("/api/MostUsersHavOrders")]
        public async Task<ActionResult> getMostUsersHavOrders()
        {
            //List<Book> books = bookRepo.getBestSeller();

            List<MostUsersHavOrdersDTO> mostUsersHavOrdersDTOs = rep.getMostUsersHavOrders();
            List<AppUser> users = new List<AppUser>();
            if (mostUsersHavOrdersDTOs != null)
            {
               foreach(var mostUserHavOrdersDTO in mostUsersHavOrdersDTOs)
                {
                    AppUser? user = await userManager.FindByIdAsync(mostUserHavOrdersDTO.UserId);

                    users.Add(user);
                }
                return Ok(users);
            }
            else return NotFound();
        }
    }
}
