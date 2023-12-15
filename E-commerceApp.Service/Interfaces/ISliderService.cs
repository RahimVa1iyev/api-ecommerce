using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.SliderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface ISliderService
    {

        List<SliderGetAllDto> GetAll();

        SliderGetDto Get(int id);

        GenerateCreateId Create(SliderPostDto postDto);

        void Edit(int id,SliderPutDto putDto);

        void Delete(int id);
    }
}
