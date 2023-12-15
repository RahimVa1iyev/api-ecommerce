using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetModalDto
    {
        public int Id { get; set; }

        public BrandInProductGetModalDto Brand { get; set; }

        public CategoryInProductGetModalDto Category { get; set; }

        public List<ColorInProductGetModalDto> Colors { get; set; }

        public List<SizeInProductGetModalDto> Sizes { get; set; }

        public GenderInProductGetModalDto Gender { get; set; }

        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public string Desc { get; set; }



        public List<ImageInProductGetModalDto> Images { get; set; }

    }

    public class BrandInProductGetModalDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryInProductGetModalDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class ColorInProductGetModalDto
    {
        public int ColorId { get; set; }

        public string Name { get; set; }
    }

    public class SizeInProductGetModalDto
    {
        public int SizeId { get; set; }

        public string Name { get; set; }
    }

    public class GenderInProductGetModalDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ImageInProductGetModalDto
    {
        public bool ImageStatus { get; set; }

        public string ImageName { get; set; }
    }
}
