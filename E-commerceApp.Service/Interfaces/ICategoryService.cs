using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface ICategoryService
    {
        CategoryGetDto Get(int id);

        List<CategoryGetAllDto> GetAll();

        GenerateCreateId Create(CategoryPostDto postDto);

        void Edit(int id, CategoryPutDto putDto);

        void Delete(int id);
    }
}
