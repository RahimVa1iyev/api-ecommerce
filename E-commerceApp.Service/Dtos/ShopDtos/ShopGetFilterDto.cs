using E_commerceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ShopDtos
{
    public class ShopGetFilterDto
    {
        public int Page { get; set; } = 1;

        public int PerPage { get; set; } = 4;

        public GenderStatus Gender { get; set; } = GenderStatus.Men;

        public List<int> CategoryIds { get; set; } = new List<int>();

        public List<int> BrandIds { get; set; } = new List<int>();

        public List<int> SideIds { get; set; } = new List<int>();

        public List<int> ColorIds { get; set; } = new List<int>();

        public decimal MinPrice { get; set; } = 0;

        public decimal MaxPrice { get; set; } = 0;

        public string Sort { get; set; } = "a-z";

    }
}
