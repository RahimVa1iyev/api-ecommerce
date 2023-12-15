using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetShopPrDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public string Desc { get; set; }

        public int Rate { get; set; }

        public bool StockStatus { get; set; }

        public CategoryInProductGetShopPrDto Category { get; set; }

        public GenderInProductGetShopPrDto Gender { get; set; }

        public BrandInProductGetShopPrDto Brand { get; set; }

        public List<SizeInProductGetShopPrDto> Sizes { get; set; }

        public string ImageName { get; set; }
    }

    public class BrandInProductGetShopPrDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class CategoryInProductGetShopPrDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SizeInProductGetShopPrDto
    {
        public int SizeId { get; set; }

        public string Name { get; set; }
    }

    public class GenderInProductGetShopPrDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
