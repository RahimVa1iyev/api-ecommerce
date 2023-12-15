using E_commerceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public GenderStatus  Gender { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public int Rate { get; set; }

        public bool StockStatus { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        public int SellerCount { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

        public List<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

        public List<Image> Images { get; set; } = new List<Image>();

        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        public List<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        public Brand Brand { get; set; }

        public Category Category { get; set; }



    }
}
