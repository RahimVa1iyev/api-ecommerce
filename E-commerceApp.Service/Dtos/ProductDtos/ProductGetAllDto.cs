using E_commerceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public GenderInProductGetAllDto Gender { get; set; }

        public decimal DiscountedPrice { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public BrandInProductGetAllDto Brand { get; set; }

        public CategoryInProductGetAllDto Category { get; set; }

        public List<SizeInProductGetAllDto> Sizes { get; set; } = new List<SizeInProductGetAllDto>();

        public List<ColorInProductGetAllDto> Colors { get; set; } = new List<ColorInProductGetAllDto>();

        public List<ImageinProductGetAllDto> Images { get; set; }
    }


    public class BrandInProductGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class CategoryInProductGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SizeInProductGetAllDto
    {
        public int SizeId { get; set; }

        public string Name { get; set; }
    }

    public class ColorInProductGetAllDto
    {
        public int ColorId { get; set; }

        public string Name { get; set; }
    }
    public class ImageinProductGetAllDto
    {
        public int Id { get; set; }

        public bool? ImageStatus { get; set; }

        public string ImageName { get; set; }
    }
    public class GenderInProductGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
