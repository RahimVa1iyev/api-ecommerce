using E_commerceApp.Core.Entities;
using E_commerceApp.Core.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductPostDto
    {

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public byte Gender { get; set; }

        public string Desc { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public bool StockStatus { get; set; }

        public IFormFile PosterFile { get; set; }

        public IFormFile HoverFile { get; set; }

        public List<IFormFile> ImageFiles { get; set; }

        public List<int> SizeIds { get; set; }

        public List<int> ColorIds { get; set; }


    }


    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(70);
            RuleFor(x => x.Desc).NotEmpty().MinimumLength(40).MaximumLength(500);
            RuleFor(x => x.SalePrice);
           

            RuleFor(x => x.CostPrice);
            RuleFor(x => x.DiscountedPrice);

            RuleFor(x => x.HoverFile).NotNull().WithMessage("Hover image is a required field");
            RuleFor(x => x.PosterFile).NotNull().WithMessage("Poster image is a required field");
            RuleFor(x => x.ImageFiles).NotNull().WithMessage("Image files are  required field");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.PosterFile != null)
                {
                    if (x.PosterFile.Length > 2 * 1024 * 1024)
                    {
                        context.AddFailure("ImageFile", "ImageFile file must be less or equal that 2MB");

                    }
                    if (x.PosterFile.ContentType != "image/jpeg" && x.PosterFile.ContentType != "image/png")
                    {
                        context.AddFailure("ImageFile", "ImageFile must be png , jpg or jpeg file");

                    }
                }
            });


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.HoverFile != null)
                {
                    if (x.HoverFile.Length > 2 * 1024 * 1024)
                    {
                        context.AddFailure("ImageFile", "ImageFile file must be less or equal that 2MB");

                    }
                    if (x.HoverFile.ContentType != "image/jpeg" && x.HoverFile.ContentType != "image/png")
                    {
                        context.AddFailure("ImageFile", "ImageFile must be png , jpg or jpeg file");

                    }
                }
            });

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFiles != null)
                {
                    foreach (var formFile in x.ImageFiles)
                    {
                        if (formFile.Length > 2 * 1024 * 1024)
                        {
                            context.AddFailure("ImageFile", "ImageFile file must be less or equal that 2MB");

                        }
                        if (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png")
                        {
                            context.AddFailure("ImageFile", "ImageFile must be png , jpg or jpeg file");

                        }
                    }
                }
            });
        }
    }
}
