using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductGetPaginatedDto
    {
        public int PageScroll { get; set; }

        public int PerPage { get; set; }
    }
}
