using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.SliderDtos;
using E_commerceApp.Service.Exceptions;
using E_commerceApp.Service.Helpers;
using E_commerceApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;

        public SliderService(ISliderRepository sliderRepository , IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }

        public GenerateCreateId Create(SliderPostDto postDto)
        {
            Slider slider = _mapper.Map<Slider>(postDto);

            foreach (var item in _sliderRepository.GetQueryable(x=>true).Where(x => x.Order >= postDto.Order).ToList())
            {
                item.Order++;
            }

            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
            if (postDto.ImageFile != null)
                slider.Image = FileManager.Save(postDto.ImageFile, rootPath, "uploads/sliders");
            else
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Image", "Image is a required filed");

            _sliderRepository.Add(slider);
            _sliderRepository.IsCommit();

            return new GenerateCreateId { Id = slider.Id };
        }

        public void Delete(int id)
        {
            var slider = _sliderRepository.Get(x => x.Id == id);
            if (slider == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Slider not found by Id {id}");

            string removableImage = slider.Image;
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

            _sliderRepository.Remove(slider);
            _sliderRepository.IsCommit();

            FileManager.Delete(rootPath, "uploads/sliders", removableImage);


        }

        public void Edit(int id, SliderPutDto putDto)
        {
            var slider = _sliderRepository.Get(x => x.Id == id);
            if (slider == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Slider not found by Id {id}");

            foreach (var item in _sliderRepository.GetQueryable(x=>true).Where(x => x.Order <= putDto.Order).ToList())
            {
                if (putDto.Order >= item.Order)
                {
                    item.Order--;

                }
            }

            slider.ButtonText = putDto.ButtonText;
            slider.ButtonUrl = putDto.ButtonUrl;
            slider.Description = putDto.Description;
            slider.SecondTitle = putDto.SecondTitle;
            slider.Title = putDto.Title;
            slider.BgColor = putDto.BgColor;

            string removableImage = null;
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

            if (putDto.ImageFile!=null)
            {
                removableImage = slider.Image;
                slider.Image = FileManager.Save(putDto.ImageFile, rootPath, "uploads/sliders");
            }

            _sliderRepository.IsCommit();

            if(removableImage !=null)
                FileManager.Delete(rootPath,"uploads/sliders",removableImage);
        }

        public SliderGetDto Get(int id)
        {
            var product = _sliderRepository.Get(x => x.Id == id);
            if (product == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Slider not found by Id {id}");

            var getDto = _mapper.Map<SliderGetDto>(product);

            return getDto;
        }

        public List<SliderGetAllDto> GetAll()
        {
            var sliders = _sliderRepository.GetQueryable(x => true).ToList();

            var getAllDto = _mapper.Map<List<SliderGetAllDto>>(sliders);

            return getAllDto;

        }

        
    }
}
