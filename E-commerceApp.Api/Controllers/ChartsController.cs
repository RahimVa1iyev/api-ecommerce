using E_commerceApp.Core.Enums;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly WatchesDbContext _context;

        public ChartsController(WatchesDbContext context)
        {
            _context = context;
        }

        [HttpGet("montlysales")]
        public IActionResult MontlySale()
        {
            List<string> list = new List<string>();
            List<int> priceList = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                DateTime startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-i);
                DateTime endDate = startDate.AddMonths(1);


                list.Add(startDate.ToString("MMM"));
                priceList.Add((int)_context.Orders
                    .Where(x => x.Status == OrderStatus.Accepted && x.CreatedAt >= startDate && x.CreatedAt < endDate)
                    .Sum(x => x.TotalAmount));
            }

            ChartMonthlySaleDto dto = new ChartMonthlySaleDto
            {
                Months = list,
                Prices = priceList,
            };

            return Ok(dto);

        }

        [HttpGet("count")]
        public IActionResult GetCount()
        {
            ChartGetOrderStatusCountDto dto = new ChartGetOrderStatusCountDto
            {
                AcceptedCount = _context.Orders.Where(x => x.Status == OrderStatus.Accepted).Count(),
                RejectedCount = _context.Orders.Where(x => x.Status == OrderStatus.Rejected).Count(),
                PendingCount = _context.Orders.Where(x => x.Status == OrderStatus.Pending).Count(),

            };

            return Ok(dto);
        }

        [HttpGet("income")]
        public IActionResult GetIncome()
        {

            DateTime today = DateTime.UtcNow.Date;
            DateTime yesterday = today.AddDays(-1);

            DateTime now = DateTime.UtcNow;
            DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            DateTime firstDayOfLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime lastDayOfLastMonth = new DateTime(now.Year, now.Month, 1).AddDays(-1);

            DateTime firstDayOfYear = new DateTime(now.Year, 1, 1);
            DateTime lastDayOfYear = new DateTime(now.Year, 12, 31);

            DateTime firstDayOfLastYear = new DateTime(now.Year - 1, 1, 1);
            DateTime lastDayOfLastYear = new DateTime(now.Year - 1, 12, 31);


            decimal todayRevenue = 0;
            decimal yesterdayRevenue = 0;

            decimal monthlyRevenue = 0;
            decimal lastMonthRevenue = 0;

            decimal yearlyRevenue = 0;
            decimal lastYearRevenue = 0;

            var orderQuery = _context.Orders.Include(x => x.OrderItems).Where(x => x.Status == OrderStatus.Accepted).AsQueryable();


            var todayOrders = orderQuery.Where(x => x.CreatedAt >= today && x.CreatedAt < today.AddDays(1)).ToList();
            var yesterdayOrders = orderQuery.Where(x => x.CreatedAt >= yesterday && x.CreatedAt < yesterday.AddDays(1)).ToList();

            foreach (var order in todayOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    todayRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }

            foreach (var order in yesterdayOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    yesterdayRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }


            var lastMonthOrders = orderQuery.Where(x => x.CreatedAt >= firstDayOfLastMonth && x.CreatedAt <= lastDayOfLastMonth);
            var thismonthOrders = orderQuery.Where(x => x.CreatedAt >= firstDayOfMonth && x.CreatedAt <= lastDayOfMonth);

            foreach (var order in thismonthOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    monthlyRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }

            foreach (var order in lastMonthOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    lastMonthRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }


            var lastYearOrders = orderQuery.Where(x => x.CreatedAt >= firstDayOfLastYear && x.CreatedAt <= lastDayOfLastYear);
            var thisyearOrders = orderQuery.Where(x => x.CreatedAt >= firstDayOfYear && x.CreatedAt <= lastDayOfYear);

            foreach (var order in lastYearOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    lastYearRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }

            foreach (var order in thisyearOrders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    decimal itemPrice = orderItem.UnitDiscountedPrice > 0 ? orderItem.UnitDiscountedPrice : orderItem.UnitSalePrice;

                    decimal itemCost = orderItem.UnitCostPrice;

                    yearlyRevenue += (itemPrice - itemCost) * orderItem.Count;
                }
            }

            decimal dailyRevenueDiff = todayRevenue - yesterdayRevenue;
            decimal dailyInterestRate = (yesterdayRevenue !=0) ? dailyRevenueDiff/yesterdayRevenue * 100 : 0;

            decimal monthlyRevenueDiff = monthlyRevenue - lastMonthRevenue;
            decimal monthlyInterestRate = (lastMonthRevenue != 0) ? monthlyRevenueDiff / lastMonthRevenue * 100 : 0;

            decimal yearlyRevenueDiff = yearlyRevenue - lastYearRevenue;
            decimal yearlyInterestRate = (lastYearRevenue != 0) ? yearlyRevenueDiff / lastYearRevenue * 100 : 0;

            ChartGetIncomeDto chartGetIncomeDto = new ChartGetIncomeDto()
            {
                Daily = new DailyRevenueInChartGetIncomeDto()
                {
                    Income = Math.Round(todayRevenue,2),
                    InterestRate = Math.Round(dailyInterestRate,2)
                },
                Monthly = new MonthlyRevenueInChartGetIncomeDto()
                {
                    Income =Math.Round(monthlyRevenue,2),
                    InterestRate =Math.Round(monthlyInterestRate,2),
                },
                Yearly = new YearlyRevenueInChartGetIncomeDto()
                {
                    Income = Math.Round(yearlyRevenue,2),
                    InterestRate =Math.Round(yearlyInterestRate,2),
                }
            };

            return Ok(chartGetIncomeDto);


        }


    }
}
