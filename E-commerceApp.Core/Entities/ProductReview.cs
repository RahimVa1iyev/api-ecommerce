using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string AppUserId { get; set; }

        public int Rate { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public AppUser AppUser { get; set; }

        public Product Product { get; set; }

    }
}
