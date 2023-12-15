using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface ISizeService 
    {
        List<SizeGetAllDto> GetAll();

        SizeGetDto Get(int id);

        GenerateCreateId Create(SizePostDto postDto);

        void Edit(int id, SizePutDto putDto);

        void Delete(int id);
    }
}
