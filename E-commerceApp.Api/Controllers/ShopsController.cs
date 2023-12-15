using AutoMapper;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ShopDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly IMapper _mapper;

        public ShopsController(WatchesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult AddBasket(ShopCreateDto createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var basketItems = _context.BasketItems.Where(x => x.AppUserId == userId).ToList();

            var basketItem = basketItems.FirstOrDefault(x => x.ProductId == createDto.Id);

            if (basketItem == null)
            {
                basketItem = new()
                {
                    Count = 1,
                    ProductId = createDto.Id,
                    AppUserId = userId
                };
                _context.BasketItems.Add(basketItem);

            }
            else
            {
                basketItem.Count++;
            }
            if (createDto.Count != 0)
            {
                basketItem.Count = createDto.Count;

            }


            _context.SaveChanges();

            return StatusCode(201, new { count = basketItem.Count });

        }

        [HttpGet("all")]
        public IActionResult GetAllProduct()
        {
            BasketDto dto = new BasketDto();



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.Images.Where(x => x.ImageStatus == true)).Include(x => x.Product).ThenInclude(x => x.ProductSizes).ThenInclude(x => x.Size).Include(x => x.Product).ThenInclude(x => x.ProductColors).ThenInclude(x => x.Color).Where(x => x.AppUserId == userId).ToList();

            foreach (var bi in items)
            {

                BasketItemDto itemDto = new BasketItemDto()
                {
                    Count = bi.Count,
                    Product = bi.Product
                };

                dto.Items.Add(itemDto);
                dto.TotalAmount += (itemDto.Product.DiscountedPrice > 0 ? itemDto.Product.DiscountedPrice : itemDto.Product.SalePrice) * itemDto.Count;

            }

            return Ok(dto);



        }

        [Authorize(Roles = "Member")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;

            var basketItem = _context.BasketItems.FirstOrDefault(x => x.AppUserId == userId && x.ProductId == id);


            if (basketItem == null)
                return NotFound("Item not found");
            else
            {
                _context.BasketItems.Remove(basketItem);
                _context.SaveChanges();
            }

            return NoContent();
        }


        [HttpGet("")]
        public ActionResult<PaginatedListDto<ShopPaginatedDto>> GetFilter([FromQuery] ShopGetFilterDto filterDto)
        {
            var productQuery = _context.Products.Include(x => x.Images.Where(x => x.ImageStatus != null)).AsQueryable();

            if (filterDto.MinPrice != 0 && filterDto.MaxPrice != 0)
                productQuery = productQuery.Where(x => (int)x.SalePrice >= (int)filterDto.MinPrice && (int)x.SalePrice <= (int)filterDto.MaxPrice);

            if (filterDto.CategoryIds.Count > 0)
                productQuery = productQuery.Where(x => filterDto.CategoryIds.Contains(x.CategoryId));

            if (filterDto.BrandIds.Count > 0)
                productQuery = productQuery.Where(x => filterDto.BrandIds.Contains(x.BrandId));

            if (filterDto.SideIds.Count > 0)
                productQuery = productQuery.Where(x => x.ProductSizes.Any(ps => filterDto.SideIds.Contains(ps.SizeId)));

            if (filterDto.SideIds.Count > 0)
                productQuery = productQuery.Where(x => x.ProductColors.Any(ps => filterDto.ColorIds.Contains(ps.ColorId)));


            switch (filterDto.Sort)
            {
                case "a-z":
                    productQuery = productQuery.OrderBy(x => x.Name);
                    break;
                case "z-a":
                    productQuery = productQuery.OrderByDescending(x => x.Name);
                    break;
                case "low-high":
                    productQuery = productQuery.OrderByDescending(x => x.SalePrice);
                    break;
                case "high-low":
                    productQuery = productQuery.OrderBy(x => x.SalePrice);
                    break;
                default:
                    break;
            }

            var dto = _mapper.Map<List<ShopPaginatedDto>>(productQuery.Skip((filterDto.Page - 1) * filterDto.PerPage).Take(filterDto.PerPage)).ToList();

            return new PaginatedListDto<ShopPaginatedDto>(dto, filterDto.Page, filterDto.PerPage, productQuery.Count());


        }
    }
}
