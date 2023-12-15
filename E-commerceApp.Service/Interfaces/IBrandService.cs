using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.BrandDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IBrandService
    {
        List<BrandGetAllDto> GetAll();

        BrandGetDto Get(int id);

        PaginatedListDto<BrandGetPaginatedDto> GetPaginated(int page);

        GenerateCreateId Create(BrandPostDto postDto);

        void Edit(int id,BrandPutDto putDto);

        void Delete(int id);

    }
}
