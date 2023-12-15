using AutoMapper;
using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Enums;
using E_commerceApp.Core.Repositories;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ProductDtos;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly WatchesDbContext _context;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        public string rootPath => Directory.GetCurrentDirectory() + "/wwwroot";


        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, ISizeRepository sizeRepository, IColorRepository colorRepository, IMapper mapper, WatchesDbContext context)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _context = context;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
        }
        public GenerateCreateId Create(ProductPostDto postDto)
        {
            if (_productRepository.IsExist(x => x.Name == postDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already been taken");

            if (!_brandRepository.IsExist(x => x.Id == postDto.BrandId))
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Brand not found by Id {postDto.BrandId}");

            if (!_categoryRepository.IsExist(x => x.Id == postDto.CategoryId))
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Category not found by Id {postDto.CategoryId}");

            foreach (var sizeId in postDto.SizeIds)
            {
                if (!_sizeRepository.IsExist(x => x.Id == sizeId))
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"Size not found by Id {sizeId}");
            }

            foreach (var colorId in postDto.ColorIds)
            {
                if (!_colorRepository.IsExist(x => x.Id == colorId))
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"Color not found by Id {colorId}");

            }

            var product = _mapper.Map<Product>(postDto);
            




            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";

            Image posterImg = new Image()
            {
                ImageStatus = true,
                ImageName = FileManager.Save(postDto.PosterFile, rootPath, "uploads/products")
            };
            product.Images.Add(posterImg);



            Image hoverImg = new Image()
            {
                ImageStatus = false,
                ImageName = FileManager.Save(postDto.HoverFile, rootPath, "uploads/products")
            };
            product.Images.Add(hoverImg);

            foreach (var imageFile in postDto.ImageFiles)
            {
                Image image = new Image()
                {
                    ImageStatus = null,
                    ImageName = FileManager.Save(imageFile, rootPath, "uploads/products")
                };

                product.Images.Add(image);
            };

            _productRepository.Add(product);
            _productRepository.IsCommit();

            return new GenerateCreateId { Id = product.Id };

        }

        public void Delete(int id)
        {
            var product = _productRepository.Get(x => x.Id == id, "Images");

            if (product == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Product not found by Id {id}");

            List<string> removableImages = new List<string>();

            foreach (var img in product.Images)
            {
                removableImages.Add(img.ImageName);
            }
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";



            _productRepository.Remove(product);
            _productRepository.IsCommit();

            FileManager.DeleteAll(rootPath, "uploads/products", removableImages);

        }

        public void Edit(int id, ProductPutDto putDto)
        {
            var existProduct = _productRepository.Get(x => x.Id == id, "Images", "ProductSizes", "ProductColors");

            if (existProduct == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Product not found by Id {id}");

            if (existProduct.Name != putDto.Name && _productRepository.IsExist(x => x.Name == putDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name has already been taken");

            if (!_brandRepository.IsExist(x => x.Id == putDto.BrandId))
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Brand not found by Id {putDto.BrandId}");

            if (!_categoryRepository.IsExist(x => x.Id == putDto.CategoryId))
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Category not found by Id {putDto.CategoryId}");

            if (putDto.SizeIds.Count>0)
            {
                existProduct.ProductSizes = new List<ProductSize>();

                foreach (var sizeId in putDto.SizeIds)
                {
                    if (!_sizeRepository.IsExist(x => x.Id == sizeId))
                        throw new RestException(System.Net.HttpStatusCode.NotFound, $"Size not found by Id {sizeId}");

                    existProduct.ProductSizes.Add(new ProductSize { SizeId = sizeId });

                }
            }


            if (putDto.ColorIds.Count>0)
            {
                existProduct.ProductColors = new List<ProductColor>();

                foreach (var colorId in putDto.ColorIds)
                {
                    if (!_colorRepository.IsExist(x => x.Id == colorId))
                        throw new RestException(System.Net.HttpStatusCode.NotFound, $"Color not found by Id {colorId}");

                    existProduct.ProductColors.Add(new ProductColor { ColorId = colorId });
                }
            }



            existProduct.BrandId = putDto.BrandId;
            existProduct.CategoryId = putDto.CategoryId;
            existProduct.Name = putDto.Name;
            existProduct.SalePrice = putDto.SalePrice;
            existProduct.CostPrice = putDto.CostPrice;
            existProduct.DiscountedPrice = putDto.DiscountedPrice;
            existProduct.Desc = putDto.Desc;
            existProduct.IsFeatured = putDto.IsFeatured;
            existProduct.IsNew = putDto.IsNew;
            existProduct.StockStatus = putDto.StockStatus;
            existProduct.Gender = (GenderStatus)putDto.Gender;



            List<string> removableImages = new List<string>();
            var rootPath = Directory.GetCurrentDirectory() + "/wwwroot";


            if (putDto.PosterFile != null)
            {
                Image poster = existProduct.Images.FirstOrDefault(x => x.ImageStatus == true);
                removableImages.Add(poster.ImageName);
                poster.ImageName = FileManager.Save(putDto.PosterFile, rootPath, "uploads/products");
            }

            if (putDto.HoverFile != null)
            {
                Image hover = existProduct.Images.FirstOrDefault(x => x.ImageStatus == false);
                removableImages.Add(hover.ImageName);
                hover.ImageName = FileManager.Save(putDto.HoverFile, rootPath, "uploads/products");
            }

            var removableImageNames = existProduct.Images.FindAll(x => x.ImageStatus == null && !putDto.ImageIds.Contains(x.Id));
            _context.Images.RemoveRange(removableImageNames);

            removableImages.AddRange(removableImageNames.Select(x => x.ImageName).ToList());

            if (putDto.ImageFiles != null)
            {
                foreach (var imagefile in putDto.ImageFiles)
                {
                    Image image = new Image()
                    {
                        ImageStatus = null,
                        ImageName = FileManager.Save(imagefile, rootPath, "uploads/products")
                    };
                    existProduct.Images.Add(image);

                }
            }

            _productRepository.IsCommit();

            if (removableImages.Count > 0)
            {
                FileManager.DeleteAll(rootPath, "uploads/products", removableImages);
            }


        }

        public ProductGetDto Get(int id)
        {
            var product = _productRepository.Get(x => x.Id == id, "Images", "Brand", "Category", "ProductColors.Color", "ProductSizes.Size");

            if (product == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Product not found by Id {id}");

            var getDto = _mapper.Map<ProductGetDto>(product);

            return getDto;
        }

        public List<ProductGetAllDto> GetAll(ProductGetPaginatedDto paginatedDto)
        {
            var products = _productRepository.GetQueryable(x => true, "Images", "Brand", "Category", "ProductColors.Color", "ProductSizes.Size").Skip((paginatedDto.PageScroll-1)* paginatedDto.PerPage).Take(paginatedDto.PerPage).ToList();

            var getallDto = _mapper.Map<List<ProductGetAllDto>>(products);

            return getallDto;
        }

        public List<ProductGetDicountedDto> GetDiscountedPr()
        {
            var products = _productRepository.GetQueryable(x =>
                x.DiscountedPrice > 0 &&
                x.Images.Any(img => img.ImageStatus.HasValue && (img.ImageStatus.Value || !img.ImageStatus.Value)),
                "Images"
            ).Take(6).ToList();

            var getDiscountedDto = _mapper.Map<List<ProductGetDicountedDto>>(products);

            return getDiscountedDto;
        }

        public List<ProductGetNewDto> GetNewestPr()
        {
            var products = _productRepository.GetQueryable(x =>
                x.IsNew == true &&
                x.Images.Any(img => img.ImageStatus.HasValue && (img.ImageStatus.Value || !img.ImageStatus.Value)),
                "Images"
            ).Take(12).ToList();

            var getNewDto = _mapper.Map<List<ProductGetNewDto>>(products);

            return getNewDto;
        }

        public List<ProductGetMostViewDto> GetMostViewPr()
        {
            var products = _productRepository.GetQueryable(x =>
               x.Images.Any(img => img.ImageStatus.HasValue && (img.ImageStatus.Value || !img.ImageStatus.Value)),
               "Images"
           ).OrderByDescending(x => x.ViewCount).Take(12).ToList();

            var getMostViewDto = _mapper.Map<List<ProductGetMostViewDto>>(products);

            return getMostViewDto;
        }

        public List<ProductGetFeaturedDto> GetFeaturedPr()
        {
            var products = _productRepository.GetQueryable(x =>
               x.IsFeatured == true &&
               x.Images.Any(img => img.ImageStatus.HasValue && (img.ImageStatus.Value || !img.ImageStatus.Value)),
               "Images"
           ).Take(9).ToList();

            var getFeaturedDto = _mapper.Map<List<ProductGetFeaturedDto>>(products);

            return getFeaturedDto;

        }

        public List<ProductGetBestSellerDto> GetBestSellerPr()
        {
            var products = _productRepository.GetQueryable(x =>
             x.Images.Any(img => img.ImageStatus.HasValue && (img.ImageStatus.Value || !img.ImageStatus.Value)),
             "Images"
         ).OrderByDescending(x => x.SellerCount).Take(12).ToList();

            var getbestSellerDto = _mapper.Map<List<ProductGetBestSellerDto>>(products);

            return getbestSellerDto;
        }

        public ProductGetModalDto GetModalPr(int id)
        {
            var product = _productRepository.Get(x => x.Id == id, "Category", "Brand", "Images", "ProductColors.Color", "ProductSizes.Size");
            if (product == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Product not found by Id {id}");

            var getModalDto = _mapper.Map<ProductGetModalDto>(product);

            return getModalDto;
        }

        public ProductGetCompareModalDto GetCompareModalPr(int id)
        {
            var product = _productRepository.Get(x => x.Id == id, "Images");
            if (product == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Product not found by Id {id}");

            var getCompareModalDto = _mapper.Map<ProductGetCompareModalDto>(product);

            return getCompareModalDto;
        }



        public List<ProductGetShopPrDto> GetShopPr()
        {
            var products = _productRepository.GetQueryable(x => true, "Brand", "Category", "ProductSizes.Size", "Images").ToList();

            var getDto = _mapper.Map<List<ProductGetShopPrDto>>(products);

            return getDto;

        }

      
    }
}
