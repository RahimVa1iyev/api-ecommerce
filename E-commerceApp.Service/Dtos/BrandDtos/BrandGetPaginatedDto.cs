using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.BrandDtos
{
    public class BrandGetPaginatedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductsCount { get; set; }
    }
}
