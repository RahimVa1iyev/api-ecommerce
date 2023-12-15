using AutoMapper;
using ClosedXML.Excel;
using E_commerceApp.Core.Entities;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ProductDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace E_commerceApp.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly WatchesDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, WatchesDbContext context, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {
            return Ok(_productService.Get(id));

        }

        [HttpPost("all")]
        public ActionResult<List<ProductGetAllDto>> GetAll(ProductGetPaginatedDto paginatedDto)
        {
            return Ok(_productService.GetAll(paginatedDto));
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] ProductPostDto postDto)
        {
            var id = _productService.Create(postDto);

            return StatusCode(201, id);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromForm] ProductPutDto putDto)
        {
            _productService.Edit(id, putDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return NoContent();
        }

        [HttpPost("review")]
        public IActionResult Review(ProductCreateReviewDto reviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var rw = _context.ProductReviews.FirstOrDefault(x => x.AppUserId == userId && x.ProductId == reviewDto.ProductId);

            if (rw != null)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Review", "Your Review is already available");

            }

            ProductReview review = new ProductReview()
            {
                AppUserId = userId,
                ProductId = reviewDto.ProductId,
                Rate = reviewDto.Rate,
                Text = reviewDto.Text,
                CreatedAt = DateTime.UtcNow.AddHours(4),
            };

            var product = _context.Products.Include(x=>x.ProductReviews).FirstOrDefault(x => x.Id == review.ProductId);
            if (product == null)
                return NotFound($"Product not found by Id {review.ProductId}");

            product.ProductReviews.Add(review);
            product.Rate = (byte)Math.Ceiling(product.ProductReviews.Average(x => x.Rate));


            _context.SaveChanges();

            return StatusCode(201, new { id = review.Id });


        }


        [HttpGet("review/{id}")]
        public ActionResult<List<ProductGetReviewDto>> GetReview(int id)
        {
            var reviews = _context.ProductReviews.Include(x => x.AppUser).Include(x => x.Product).Where(x => x.ProductId == id);


            List<ProductGetReviewDto> reviewDto = reviews.Select(x => new ProductGetReviewDto
            {
                AppUserId = x.AppUserId,
                AppUserUserName = x.AppUser.UserName,
                Rate = x.Rate,
                Text = x.Text,
                CreatedAt = x.CreatedAt
            }).ToList();

            return Ok(reviewDto);

        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(x => x.Images)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Include(x => x.ProductColors)
                .ThenInclude(x => x.Color)
                .Include(x => x.ProductSizes)
                .ThenInclude(x => x.Size).FirstOrDefault(x => x.Id == id);
            product.ViewCount += 1;
            var relatedProducts = _context.Products.Include(x => x.Images.Where(x => x.ImageStatus != null)).Where(x => x.BrandId == product.BrandId && x.Id != product.Id).ToList();

            ProductDetailDto detailDto = new()
            {
                Product = _mapper.Map<ExistProductInProductDetailDto>(product),
                RelatedProducts = _mapper.Map<List<RelatedProductInProductDetailDto>>(relatedProducts)
            };

            _context.SaveChanges();

            return Ok(detailDto);
        }

        [HttpGet("discounted")]
        public ActionResult<List<ProductGetDicountedDto>> GetDiscountedProduct()
        {
            return Ok(_productService.GetDiscountedPr());
        }

        [HttpGet("new")]
        public ActionResult<List<ProductGetNewDto>> GetNewestProduct()
        {
            return Ok(_productService.GetNewestPr());
        }

        [HttpGet("featured")]
        public ActionResult<List<ProductGetFeaturedDto>> GetFeaturedProduct()
        {
            return Ok(_productService.GetFeaturedPr());
        }

        [HttpGet("mostview")]
        public ActionResult<List<ProductGetMostViewDto>> GetMostViewProduct()
        {
            return Ok(_productService.GetMostViewPr());
        }

        [HttpGet("modal/{id}")]
        public ActionResult<ProductGetModalDto> GetModal(int id)
        {
            return Ok(_productService.GetModalPr(id));
        }

        [HttpGet("compare/{id}")]
        public ActionResult<ProductGetCompareModalDto> GetComparePr(int id)
        {
            return Ok(_productService.GetCompareModalPr(id));
        }

        [HttpGet("best-seller")]
        public ActionResult<ProductGetBestSellerDto> GetBestSeller()
        {
            return Ok(_productService.GetBestSellerPr());
        }


        [HttpGet("shop")]
        public ActionResult<List<ProductGetShopPrDto>> GetShop()
        {
            return Ok(_productService.GetShopPr());
        }

        [HttpGet("export-excel")]
        public async Task<FileResult> ExportProductInExcell()
        {
            var products = _context.Products.Include(x=>x.Brand).Include(x=>x.Category).Include(x=>x.ProductColors).ThenInclude(x=>x.Color).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).ToList();
            var filename = "product.xlsx";

            return _generateExcell(filename, products);
        }

        private FileResult _generateExcell(string fileName, IEnumerable<Product> products)
        {
            DataTable dataTable = new DataTable("Product");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("SalePrice", typeof(decimal)),
                new DataColumn("DiscountedPrice", typeof(decimal)),
                new DataColumn("CostPrice", typeof(decimal)),
                new DataColumn("Brand", typeof(string)),
                new DataColumn("Category", typeof(string)),
                new DataColumn("StockStatus", typeof(bool)),
                new DataColumn("Colors", typeof(string)),
                new DataColumn("Sizes", typeof(string))




            });

            foreach (var pr in products)
            {
                var colorNames = string.Join(", ", pr.ProductColors.Select(x => x.Color.Name));
                var sizeNames = string.Join(", ", pr.ProductSizes.Select(x => x.Size.Name));


                dataTable.Rows.Add(pr.Id,pr.Name , pr.SalePrice, pr.DiscountedPrice,pr.CostPrice,pr.Brand.Name,pr.Category.Name,pr.StockStatus,colorNames,sizeNames );

            }

            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }
    }
}
