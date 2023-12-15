using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Enums;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.OrderDtos;
using E_commerceApp.Service.Dtos.ShopDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(WatchesDbContext context,UserManager<AppUser> userManager,IOrderService orderService,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("checkout")]
        public async Task<IActionResult> CheckOut()
        {
           return Ok( await _orderService.CheckOut());
        }

        [HttpPost("")]
        public IActionResult Create(OrderCreateDto createDto)
        {
            var userId = (User.Identity.IsAuthenticated && User.IsInRole("Member")) ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;

            Order order = new Order
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Phone = createDto.PhoneNumber,
                Address = createDto.Address,
                Text = createDto.Text,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                AppUserId = userId
            };


            if (userId != null)
            {

                var basketItems = _context.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == userId).ToList();

                if (basketItems!=null)
                {
                    order.OrderItems = basketItems.Select(x => new OrderItem
                    {

                        ProductId = x.ProductId,
                        Count = x.Count,
                        UnitSalePrice = x.Product.SalePrice,
                        UnitCostPrice = x.Product.CostPrice,
                        UnitDiscountedPrice = x.Product.DiscountedPrice,

                    }).ToList();

                }
                else
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "Item", "Item not found for order");

                   
                }


            }
            else
            {
                var basketStr = Request.Cookies["basket"];

                List<BasketCookieItemDto> cookieItems = null;

                if (basketStr != null)
                    cookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemDto>>(basketStr);
                else
                    cookieItems = new List<BasketCookieItemDto>();

                foreach (var item in cookieItems)
                {
                    var product = _context.Products.Find(item.ProductId);

                    OrderItem orderItem = new OrderItem()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        UnitSalePrice = product.SalePrice,
                        UnitCostPrice = product.CostPrice,
                        UnitDiscountedPrice = product.DiscountedPrice,
                    };

                    order.OrderItems.Add(orderItem);
                }
            }

            order.TotalAmount = order.OrderItems.Sum(x => x.Count * (x.UnitDiscountedPrice > 0 ? x.UnitDiscountedPrice : x.UnitSalePrice));


            _context.Orders.Add(order);
            _context.SaveChanges();

            _context.RemoveRange(_context.BasketItems.Where(x => x.AppUserId == userId));
            _context.SaveChanges();

            return StatusCode(201, new { id = order.Id });

        }

        [Authorize(Roles ="Member")]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _context.Orders.Include(x=>x.OrderItems).Include(x=>x.AppUser).Where(x => x.AppUserId == userId).ToList();

            var getAllDto = orders.Select(x => new OrderGetAllDto
            {
                Id = x.Id,  
                IsCreateAt = x.CreatedAt,
                Status = x.Status,
                TotalItem = x.OrderItems.Count,
                TotalAmount = x.TotalAmount,
                User = new UserInOrderGetAllDto
                { 
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber=x.Phone
                }

            }).ToList();

            return Ok(getAllDto);

        }


        [HttpGet("dash-all")]
        public IActionResult DashAll()
        {
          

            var orders = _context.Orders.Include(x => x.OrderItems).Include(x => x.AppUser).ToList();

            var getAllDto = orders.Select(x => new OrderGetAllDto
            {
                Id = x.Id,
                IsCreateAt = x.CreatedAt,
                Status = x.Status,
                TotalItem = x.OrderItems.Count,
                TotalAmount = x.TotalAmount,
                User = new UserInOrderGetAllDto
                {
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber = x.Phone
                }

            }).ToList();

            return Ok(getAllDto);

        }


        [HttpGet("orderitems/{id}")]
        public IActionResult GetOrderItems(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderItems = _context.OrderItems.Include(x=>x.Product).ThenInclude(x=>x.Images).Where(x=>x.OrderId ==id);

            var baseUrl = new UriBuilder(_httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Host, _httpContextAccessor.HttpContext.Request.Host.Port ?? -1);


            var getOrderItems = orderItems.Select(x => new OrderGetOrderItemDto
            {
                Id = x.ProductId,
                Name = x.Product.Name,
                SalePrice = x.Product.SalePrice,
                CostPrice = x.Product.CostPrice,
                StockStatus = x.Product.StockStatus,
                Image = baseUrl + "uploads/products/" + x.Product.Images.FirstOrDefault(x => x.ImageStatus == true).ImageName,
            }).ToList();

           

            return Ok(getOrderItems);

        }

        [HttpPut("accepted/{id}")]
        public IActionResult Accept(int id)
        {
            var order = _context.Orders.Include(_x => _x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();


            foreach (var item in order.OrderItems)
            {
                item.Product.SellerCount += item.Count;
            }
            
            order.Status = OrderStatus.Accepted;

            _context.SaveChanges();


            return NoContent();
        }

        [HttpPut("rejected/{id}")]
        public IActionResult Reject(int id)
        {
            var order = _context.Orders.Include(_x => _x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();

            order.Status = OrderStatus.Rejected;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
