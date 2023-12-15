using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.SizeDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository , IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }
        public GenerateCreateId Create(SizePostDto postDto)
        {
            if (_sizeRepository.IsExist(x => x.Name == postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Size", "Size has already been taken");

            var size = _mapper.Map<Size>(postDto);

            _sizeRepository.Add(size);
            _sizeRepository.IsCommit();

            return new GenerateCreateId { Id = size.Id };
        }

        public void Delete(int id)
        {
            var size = _sizeRepository.Get(x => x.Id == id);

            if(size==null)
            throw new RestException(System.Net.HttpStatusCode.NotFound, $"Size not found by Id {id} ");

            _sizeRepository.Remove(size);
            _sizeRepository.IsCommit();
        }

        public void Edit(int id, SizePutDto putDto)
        {
            var size = _sizeRepository.Get(x => x.Id == id);

            if (size == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Size not found by Id {id} ");

            if (size.Name != putDto.Name && _sizeRepository.IsExist(x => x.Name == putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Size", "Size has already been taken");

            size.Name = putDto.Name;

            _sizeRepository.IsCommit();
        }

        public SizeGetDto Get(int id)
        {
            var size = _sizeRepository.Get(x => x.Id == id);

            if (size == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Size not found by Id {id} ");

            var getDto = _mapper.Map<SizeGetDto>(size);

            return getDto;

        }

        public List<SizeGetAllDto> GetAll()
        {
            var sizes = _sizeRepository.GetQueryable(x => true ,"ProductSizes").ToList();

            var getAllDto = _mapper.Map<List<SizeGetAllDto>>(sizes);

            return getAllDto;
        }
    }
}
