using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Charts
{
    public class ChartGetIncomeDto
    {
        public DailyRevenueInChartGetIncomeDto Daily { get; set; }

        public MonthlyRevenueInChartGetIncomeDto Monthly { get; set; }

        public YearlyRevenueInChartGetIncomeDto Yearly { get; set; }

    }

    public class DailyRevenueInChartGetIncomeDto
    {
        public decimal Income { get; set; }

        public decimal InterestRate { get; set; }
    }
    public class MonthlyRevenueInChartGetIncomeDto
    {
        public decimal Income { get; set; }

        public decimal InterestRate { get; set; }
    }
    public class YearlyRevenueInChartGetIncomeDto
    {
        public decimal Income { get; set; }

        public decimal InterestRate { get; set; }
    }
}
