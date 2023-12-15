using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.OrderDtos
{
    public class OrderCheckOutItemDto
    {
        public string ProductName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
