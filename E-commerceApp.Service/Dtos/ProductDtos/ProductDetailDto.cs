using E_commerceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductDetailDto
    {
        public ExistProductInProductDetailDto Product { get; set; }

        public List<RelatedProductInProductDetailDto> RelatedProducts { get; set; }
    }

    public class ExistProductInProductDetailDto
    {
        public int Id { get; set; }

        public BrandInProductDetailDto  Brand  { get; set; }

        public CategoryInProductDetailDto Category { get; set; }

        public List<SizeInProductDetailDto> Sizes { get; set; } = new List<SizeInProductDetailDto>();

        public List<ColorInProductDetailDto> Colors { get; set; } = new List<ColorInProductDetailDto>();

        public List<ImageInProductDetailDto> Images { get; set; }

        public GenderInProductDetailDto Gender { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public int Rate { get; set; }

        public bool StockStatus { get; set; }

        public int ViewCount { get; set; }

    }

    public class RelatedProductInProductDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public int Rate { get; set; }

        public List<ImageInProductDetailDto> Images { get; set; }

    }

    public class BrandInProductDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryInProductDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SizeInProductDetailDto
    {
        public int SizeId { get; set; }

        public string Name { get; set; }
    }

    public class ColorInProductDetailDto
    {
        public int ColorId { get; set; }

        public string Name { get; set; }
    }

    public class ImageInProductDetailDto
    {
        public bool? ImageStatus { get; set; }

        public string ImageName { get; set; }
    }
    public class GenderInProductDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}
