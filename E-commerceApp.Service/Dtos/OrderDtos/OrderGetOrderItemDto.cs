using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.OrderDtos
{
    public class OrderGetOrderItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public bool StockStatus { get; set; }

        public string? Image { get; set; }

    }
}
