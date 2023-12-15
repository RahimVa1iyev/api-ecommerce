using E_commerceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.OrderDtos
{
    public class OrderGetAllDto
    {

        public int Id { get; set; }

        public DateTime IsCreateAt { get; set; }

        public OrderStatus Status { get; set; }

        public int TotalItem { get; set; }

        public decimal TotalAmount { get; set; }

        public UserInOrderGetAllDto User { get; set; }
    }

    public class UserInOrderGetAllDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
