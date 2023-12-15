using E_commerceApp.Service.Commons;
using E_commerceApp.Service.Dtos.ProductDtos;
using E_commerceApp.Service.Dtos.ShopDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IProductService
    {
        ProductGetDto Get(int id);

        List<ProductGetAllDto> GetAll(ProductGetPaginatedDto paginatedDto);

        List<ProductGetDicountedDto> GetDiscountedPr();

        List<ProductGetNewDto> GetNewestPr();

        List<ProductGetMostViewDto> GetMostViewPr();

        List<ProductGetFeaturedDto> GetFeaturedPr();

        List<ProductGetBestSellerDto> GetBestSellerPr();

        ProductGetModalDto GetModalPr(int id);

        ProductGetCompareModalDto GetCompareModalPr(int id);

        List<ProductGetShopPrDto> GetShopPr();

        GenerateCreateId Create(ProductPostDto postDto);

        void Edit(int id, ProductPutDto putDto);

        void Delete(int id);
    }
}
