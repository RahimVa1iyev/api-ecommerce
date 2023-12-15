using E_commerceApp.Core.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public GenderInProductGetDto Gender { get; set; }

        public decimal DiscountedPrice { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public BrandInProductGetDto Brand { get; set; }
        
        public CategoryInProductGetDto Category { get; set; }

        public List<SizeInProductGetDto> Sizes { get; set; } = new List<SizeInProductGetDto>();

        public List<ColorInProductGetDto> Colors { get; set; } = new List<ColorInProductGetDto>();

        public List<ImageinProductGetDto> Images { get; set; }

    }


    public class BrandInProductGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class CategoryInProductGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SizeInProductGetDto
    {
        public int SizeId { get; set; }

        public string Name { get; set; }
    }

    public class ColorInProductGetDto
    {
        public int ColorId { get; set; }

        public string Name { get; set; }
    }
    public class ImageinProductGetDto
    {
        public int Id { get; set; }

        public bool? ImageStatus { get; set; }

        public string  ImageName { get; set; }
    }
    public class GenderInProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
