using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetCompareModalDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public bool StockStatus { get; set; }

        public  string Image { get; set; }
    }

  
}
