using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.OrderDtos
{
    public class OrderCheckOutDto
    {
        public List<OrderCheckOutItemDto> Items { get; set; } = new List<OrderCheckOutItemDto>();

        public OrderCheckOutFormDto Order { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
