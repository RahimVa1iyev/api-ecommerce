using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.CategoryDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public GenerateCreateId Create(CategoryPostDto postDto)
        {
            
            if (_categoryRepository.IsExist(x => x.Name == postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already taken");

            var category = _mapper.Map<Category>(postDto);

            _categoryRepository.Add(category);
            _categoryRepository.IsCommit();

            return new GenerateCreateId { Id = category.Id };

        }

        public void Delete(int id)
        {
            var category = _categoryRepository.Get(x => x.Id == id);
            if (category == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Category not found by Id {id}");

            _categoryRepository.Remove(category);
            _categoryRepository.IsCommit();


        }

        public void Edit(int id, CategoryPutDto putDto)
        {
            var category = _categoryRepository.Get(x => x.Id == id);

            if (category == null)
               throw new RestException(System.Net.HttpStatusCode.NotFound,$"Category not found by Id {id}");

            if (category.Name != putDto.Name && _categoryRepository.IsExist(x => x.Name == putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already been");

            category.Name = putDto.Name;

            _categoryRepository.IsCommit();

           
        }

        public CategoryGetDto Get(int id)
        {
            var category = _categoryRepository.Get(x => x.Id == id,"Products");

            if (category == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Category not found by Id {id}");

            var getDto = _mapper.Map<CategoryGetDto>(category);

            return getDto;

        }

        public List<CategoryGetAllDto> GetAll()
        {
            var categories = _categoryRepository.GetQueryable(x => true,"Products").ToList();

            var getAllDto = _mapper.Map<List<CategoryGetAllDto>>(categories);

            return getAllDto;
        }
    }
}
