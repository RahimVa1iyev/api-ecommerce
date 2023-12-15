using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Charts
{
    public class ChartGetOrderStatusCountDto
    {
        public int AcceptedCount { get; set; }

        public int RejectedCount { get; set; }

        public int PendingCount { get; set; }
    }
}
