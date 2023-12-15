using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ColorDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository , IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public GenerateCreateId Create(ColorPostDto postDto)
        {
            if (_colorRepository.IsExist(x => x.Name == postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Color", "Color has already taken");

            var color = _mapper.Map<Color>(postDto);

            _colorRepository.Add(color);
            _colorRepository.IsCommit();

            return new GenerateCreateId { Id = color.Id };
        }

        public void Delete(int id)
        {
            var color = _colorRepository.Get(x => x.Id == id);
            if (color == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Color not found by Id {id}");

            _colorRepository.Remove(color);
            _colorRepository.IsCommit();
        }

        public void Edit(int id, ColorPutDto putDto)
        {
            var color = _colorRepository.Get(x => x.Id == id);

            if (color == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Color not found by Id {id}");

            if (color.Name != putDto.Name && _colorRepository.IsExist(x => x.Name == putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Color", "Color has already been");

            color.Name = putDto.Name;

            _colorRepository.IsCommit();

        }

        public ColorGetDto Get(int id)
        {
            var color = _colorRepository.Get(x => x.Id == id);

            if (color == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Color not found by Id {id}");

            var getDto = _mapper.Map<ColorGetDto>(color);

            return getDto;
        }

        public List<ColorGetAllDto> GetAll()
        {
            var colors = _colorRepository.GetQueryable(x => true,"ProductColors").ToList();

            var getAllDto = _mapper.Map<List<ColorGetAllDto>>(colors);

         

            return getAllDto;
        }
    }
}
