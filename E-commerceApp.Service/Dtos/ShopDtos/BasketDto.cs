using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ShopDtos
{
    public class BasketDto
    {

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public decimal TotalAmount { get; set; }
    }
}
