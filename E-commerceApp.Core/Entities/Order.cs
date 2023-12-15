using E_commerceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }

        public DateTime CreatedAt { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
     
        public string Address { get; set; }

        public string Text { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public AppUser AppUser { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
