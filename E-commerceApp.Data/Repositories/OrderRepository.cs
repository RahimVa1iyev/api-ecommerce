using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Data.Repositories
{
    public class OrderRepository : Repository<Order> , IOrderRepository
    {
        public OrderRepository(WatchesDbContext context) : base(context)
        {

        }
    }
}
