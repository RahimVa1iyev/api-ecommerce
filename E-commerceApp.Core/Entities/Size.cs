using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class Size
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductSize> ProductSizes { get; set; }

    }
}
