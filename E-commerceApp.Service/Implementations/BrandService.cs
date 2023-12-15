using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.BrandDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public GenerateCreateId Create(BrandPostDto postDto)    
        {
            if (_brandRepository.IsExist(x=>x.Name==postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already taken");
            var brand = _mapper.Map<Brand>(postDto);

            _brandRepository.Add(brand);
            _brandRepository.IsCommit();

            return new GenerateCreateId { Id = brand.Id };
            
        }

        public void Delete(int id)
        {
            var brand = _brandRepository.Get(x => x.Id == id);
            if (brand == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Brand not found by Id {id}");

            _brandRepository.Remove(brand);
            _brandRepository.IsCommit();
          
           
        }

        public void Edit(int id, BrandPutDto putDto)
        {
            var brand = _brandRepository.Get(x => x.Id == id);
            if (brand == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Brand not found by Id {id}");

            if (putDto.Name!=brand.Name && _brandRepository.IsExist(x=>x.Name==putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already taken");

            brand.Name = putDto.Name;
            _brandRepository.IsCommit();


        }

        public BrandGetDto Get(int id)
        {
            var brand = _brandRepository.Get(x => x.Id == id,"Products");
            
            if(brand==null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Brand not found by Id {id}");

            var getDto = _mapper.Map<BrandGetDto>(brand);

            return getDto;


        }

        public List<BrandGetAllDto> GetAll()
        {
            var brands = _brandRepository.GetQueryable(x => true,"Products").ToList();

            var getAllDto = _mapper.Map<List<BrandGetAllDto>>(brands);

            return getAllDto;
        }

        public PaginatedListDto<BrandGetPaginatedDto> GetPaginated(int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            var query = _brandRepository.GetQueryable(x => true);
            var list = _mapper.Map<List<BrandGetPaginatedDto>>(query.Skip((page - 1) * 2).Take(2)).ToList();


            return new PaginatedListDto<BrandGetPaginatedDto>(list, page, 2, query.Count());
        }
    }
}
