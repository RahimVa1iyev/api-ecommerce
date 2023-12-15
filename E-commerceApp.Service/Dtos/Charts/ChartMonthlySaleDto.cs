using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Charts
{
    public class ChartMonthlySaleDto
    {
        public List<string> Months { get; set; }

        public List<int> Prices { get; set; }

    }
}
