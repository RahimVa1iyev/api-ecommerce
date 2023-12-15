using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetReviewDto
    {
        public string AppUserId { get; set; }

        public string AppUserUserName { get; set; }

        public int Rate { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
