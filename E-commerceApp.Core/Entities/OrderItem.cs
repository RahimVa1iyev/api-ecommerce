using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Count { get; set; }

        public decimal UnitSalePrice { get; set; }

        public decimal UnitCostPrice { get; set; }

        public decimal UnitDiscountedPrice { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
