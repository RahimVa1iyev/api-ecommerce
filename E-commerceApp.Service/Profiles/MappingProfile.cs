using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Enums;
using E_commerceApp.Service.Dtos.BrandDtos;
using E_commerceApp.Service.Dtos.CategoryDtos;
using E_commerceApp.Service.Dtos.ColorDtos;
using E_commerceApp.Service.Dtos.Features;
using E_commerceApp.Service.Dtos.Gender;
using E_commerceApp.Service.Dtos.Infos;
using E_commerceApp.Service.Dtos.Offers;
using E_commerceApp.Service.Dtos.ProductDtos;
using E_commerceApp.Service.Dtos.ShopDtos;
using E_commerceApp.Service.Dtos.SizeDtos;
using E_commerceApp.Service.Dtos.SliderDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IHttpContextAccessor _httpContextAccessor)
        {

            var baseUrl = new UriBuilder(_httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host.Host, _httpContextAccessor.HttpContext.Request.Host.Port ?? -1);

            CreateMap<BrandPostDto, Brand>();
            CreateMap<Brand, BrandGetDto>();
            CreateMap<Brand, BrandGetAllDto>();
            CreateMap<Brand, BrandInProductGetDto>();
            CreateMap<Brand, BrandInProductGetAllDto>();
            CreateMap<Brand, BrandGetPaginatedDto>();
            CreateMap<Brand, BrandInProductDetailDto>();
            CreateMap<Brand, BrandInProductGetModalDto>();
            CreateMap<Brand, BrandInProductGetShopPrDto>();






            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryGetAllDto>();
            CreateMap<Category, CategoryInProductGetDto>();
            CreateMap<Category, CategoryInProductGetAllDto>();
            CreateMap<Category, CategoryInProductDetailDto>();
            CreateMap<Category, CategoryInProductGetModalDto>();
            CreateMap<Category, CategoryInProductGetShopPrDto>();




            CreateMap<SizePostDto, Size>();
            CreateMap<Size, SizeGetDto>();
            CreateMap<Size, SizeGetAllDto>()
                  .ForMember(dest => dest.ProductsCount, src => src.MapFrom(m => m.ProductSizes.Count));

            CreateMap<Size, SizeInProductGetDto>();
            CreateMap<Size, SizeInProductGetAllDto>();
            CreateMap<Size, SizeInProductDetailDto>();
            CreateMap<Size, SizeInProductGetModalDto>();
            CreateMap<Size, SizeInProductGetShopPrDto>();





            CreateMap<ColorPostDto, Color>();
            CreateMap<Color, ColorGetDto>();
            CreateMap<Color, ColorGetAllDto>()
                  .ForMember(dest => dest.ProductsCount, src => src.MapFrom(m => m.ProductColors.Count));



            CreateMap<Color, ColorInProductGetAllDto>();
            CreateMap<Color, ColorInProductGetDto>();
            CreateMap<Color, ColorInProductDetailDto>();
            CreateMap<Color, ColorInProductGetModalDto>();


            CreateMap<Image, ImageinProductGetDto>()
                .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageinProductGetAllDto>()
                .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductDetailDto>()
               .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetDicountedDto>()
                .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetNewDto>()
               .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetFeaturedDto>()
               .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetMostViewDto>()
               .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetBestSellerDto>()
               .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));

            CreateMap<Image, ImageInProductGetModalDto>()
            .ForMember(d => d.ImageName, s => s.MapFrom(m => string.IsNullOrWhiteSpace(m.ImageName) && m.ImageStatus != null ? null : (baseUrl + "uploads/products/" + m.ImageName)));



            CreateMap<ProductPostDto, Product>()
                 .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.SizeIds.Select(id => new ProductSize { SizeId = id }).ToList()))
                 .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src => src.ColorIds.Select(id => new ProductColor { ColorId = id }).ToList()))
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (GenderStatus)src.Gender));
           
            CreateMap<Product, ProductGetDto>()
                   .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(x => new SizeInProductGetDto { SizeId = x.SizeId, Name = x.Size.Name })))
                    .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(x => new ColorInProductGetDto { ColorId = x.ColorId, Name = x.Color.Name })))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => new GenderInProductGetDto { Id = (int)src.Gender, Name = src.Gender.ToString() }));
          
            CreateMap<Product, ProductGetAllDto>()
                   .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(x => new SizeInProductGetAllDto { SizeId = x.SizeId, Name = x.Size.Name })))
                   .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(x => new ColorInProductGetAllDto { ColorId = x.ColorId, Name = x.Color.Name })))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => new GenderInProductGetAllDto { Id = (int)src.Gender, Name = src.Gender.ToString() }));
          
            CreateMap<Product, ProductGetModalDto>()
                  .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(x => new SizeInProductGetModalDto { SizeId = x.SizeId, Name = x.Size.Name })))
                  .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(x => new ColorInProductGetModalDto { ColorId = x.ColorId, Name = x.Color.Name })))
                  .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => new GenderInProductGetModalDto { Id = (int)src.Gender, Name = src.Gender.ToString() }));

            CreateMap<Product, ExistProductInProductDetailDto>()
                      .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(x => new SizeInProductDetailDto { SizeId = x.SizeId, Name = x.Size.Name })))
                   .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(x => new ColorInProductDetailDto { ColorId = x.ColorId, Name = x.Color.Name })))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => new GenderInProductDetailDto { Id = (int)src.Gender, Name = src.Gender.ToString() }));

            CreateMap<Product, ShopPaginatedDto>()
                  .ForMember(dest => dest.ImageName, opt =>  opt.MapFrom(src => baseUrl + "uploads/products/" + src.Images.FirstOrDefault(image => image.ImageStatus==true).ImageName));

            CreateMap<Product, ProductGetShopPrDto>()
                 .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => baseUrl + "uploads/products/" + src.Images.FirstOrDefault(image => image.ImageStatus == true).ImageName))
                 .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(x => new SizeInProductGetShopPrDto { SizeId = x.SizeId, Name = x.Size.Name })))
                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => new GenderInProductGetShopPrDto { Id = (int)src.Gender, Name = src.Gender.ToString() }));



            CreateMap<Product, ProductGetDicountedDto>();
            CreateMap<Product, ProductGetNewDto>();
            CreateMap<Product, ProductGetMostViewDto>();
            CreateMap<Product, ProductGetFeaturedDto>();
            CreateMap<Product, ProductGetBestSellerDto>();
            CreateMap<Product, RelatedProductInProductDetailDto>();
            CreateMap<Product , ProductGetCompareModalDto>()
                  .ForMember(dest => dest.Image, opt =>  opt.MapFrom(src => baseUrl + "uploads/products/" + src.Images.FirstOrDefault(image => image.ImageStatus==true).ImageName));




            CreateMap<GenderStatus, GenderGetAllDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src))
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.ToString()));



            

            CreateMap<SliderPostDto, Slider>();
            CreateMap<Slider, SliderGetAllDto>()
                .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Image) ? null : (baseUrl + "uploads/sliders/" + src.Image)));
            CreateMap<Slider, SliderGetDto>()
                .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Image) ? null : (baseUrl + "uploads/sliders/" + src.Image)));

            CreateMap<Offer, OfferGetAllDto>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Icon) ? null : (baseUrl + "uploads/offers/" + src.Icon)));

            CreateMap<Feature, FeatureGetAllDto>()
              .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.ImageUri) ? null : (baseUrl + "uploads/features/" + src.ImageUri)));

            CreateMap<Info, InfoGetDto>();






        }
    }
}
