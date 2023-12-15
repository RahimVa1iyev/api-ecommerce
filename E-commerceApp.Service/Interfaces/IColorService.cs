using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ColorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IColorService 
    {
        ColorGetDto Get(int id);

        List<ColorGetAllDto> GetAll();

        GenerateCreateId Create(ColorPostDto postDto);

        void Edit(int id, ColorPutDto putDto);

        void Delete(int id);
    }
}
