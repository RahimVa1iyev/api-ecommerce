using E_commerceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ShopDtos
{
    public class BasketItemDto
    {
        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
