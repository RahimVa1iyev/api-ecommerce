using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Data.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.OrderDtos;
using E_commerceApp.Service.Dtos.ShopDtos;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;

        public OrderService(IHttpContextAccessor httpContextAccessor,IBasketItemRepository basketItemRepository,UserManager<AppUser> userManager,IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketItemRepository = basketItemRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }



        public async  Task<OrderCheckOutDto> CheckOut()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            var baseUrl = new UriBuilder(_httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Host, _httpContextAccessor.HttpContext.Request.Host.Port ?? -1);


            OrderCheckOutDto dto = new OrderCheckOutDto();
            dto.Order = new OrderCheckOutFormDto();

          
                var basketItems = _basketItemRepository.GetQueryable(x => x.AppUserId == userId, "Product.Images").ToList();

                List<OrderCheckOutItemDto> itemDto = basketItems.Select(bi => new OrderCheckOutItemDto
                {
                    ProductName = bi.Product.Name,
                    Count = bi.Count,
                    Price = bi.Product.DiscountedPrice > 0 ? bi.Product.DiscountedPrice * bi.Count : bi.Product.SalePrice * bi.Count,
                    Image = baseUrl + "uploads/products/" + bi.Product.Images.FirstOrDefault(x=>x.ImageStatus==true).ImageName
                    
                }).ToList();

                dto.Items = itemDto;


                var user = await _userManager.FindByIdAsync(userId);

                dto.Order.FirstName = user.FirstName;
                dto.Order.LastName = user.LastName;
                dto.Order.Email = user.Email;
                dto.Order.PhoneNumber = user.PhoneNumber;

        
            dto.TotalAmount = dto.Items.Sum(x => x.Price);
            return dto;
        }

        public GenerateCreateId Create(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
        }

        public List<OrderGetAllDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
