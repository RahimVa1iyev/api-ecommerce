using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCheckOutDto> CheckOut();

        GenerateCreateId Create(OrderCreateDto orderCreateDto);

        List<OrderGetAllDto> GetAll();

    }
}
