using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
  
        public class ProductGetBestSellerDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal SalePrice { get; set; }

            public decimal DiscountedPrice { get; set; }

            public int Rate { get; set; }

            public bool StockStatus { get; set; }

            public int SellerCount { get; set; }

        public List<ImageInProductGetBestSellerDto> Images { get; set; }
        }

        public class ImageInProductGetBestSellerDto
        {
            public bool? ImageStatus { get; set; }

            public string ImageName { get; set; }
        }
    
}
