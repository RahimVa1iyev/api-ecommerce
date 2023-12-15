using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImageName { get; set; }

        public bool? ImageStatus { get; set; }

        public Product Product { get; set; }
    }
}
