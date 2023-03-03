using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await rep.getById(id);
            if (order == null)
                return NotFound($"no order Found with Id {id}");
            OrderDTO orderDTO = new OrderDTO()
            {
                orderId = order.Id,
                //number = order.Number,
                Shopingcost = order.Shopingcost,
                ShopingDate = order.ShopingDate,
                ArrivalDate = order.ArrivalDate,
                Discount = order.Discount,
                //userId = order.AppUserId
            };
            return Ok(orderDTO);
        }
        [HttpGet]
        public async Task<ActionResult> getorders()
        {
            List<Order> orders = await rep.getOrders();
            List<OrderDTO> ordersDTO = new List<OrderDTO>();
            foreach (Order item in orders)
            {
                OrderDTO dTO = new OrderDTO()
                {
                    orderId = item.Id,
                    // number=item.Number,
                    ShopingDate = item.ShopingDate,
                    Shopingcost = item.Shopingcost,
                    ArrivalDate = item.ArrivalDate,
                    Discount = item.Discount,

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
            await rep.add(order);
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
        public async Task<ActionResult> update(OrderDTO dTO, int id)
        {

            var order = await rep.getById(id);
            if (order == null) return NotFound($"no order with id {id}");

            order.Id = dTO.orderId;
            order.Shopingcost = dTO.Shopingcost;
            order.ShopingDate = dTO.ShopingDate;
            order.ArrivalDate = dTO.ArrivalDate;
            order.Discount = dTO.Discount;


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

        [HttpPost("/api/orderNow")]
        public async Task<ActionResult> orderNow(OrderNowDTO orderNowDTO)
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
                    await rep.add(order);
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
            return BadRequest("model satate invalid");
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
