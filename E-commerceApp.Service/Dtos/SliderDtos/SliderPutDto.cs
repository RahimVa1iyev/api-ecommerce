using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.SliderDtos
{
    public class SliderPutDto
    {

        public string Title { get; set; }

        public string SecondTitle { get; set; }

        public string Description { get; set; }

        public string ButtonUrl { get; set; }

        public string ButtonText { get; set; }

        public int Order { get; set; }

        public string BgColor { get; set; }

        public IFormFile ImageFile { get; set; }
    }

    public class SliderPutDtoValidator : AbstractValidator<SliderPutDto>
    {
        public SliderPutDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.SecondTitle).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10).MaximumLength(200);
            RuleFor(x => x.ButtonUrl).NotEmpty().MaximumLength(50);
            RuleFor(x=>x.BgColor).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.ButtonText).NotEmpty().MaximumLength(50);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile != null)
                {
                    if (x.ImageFile.Length > 2 * 1024 * 1024)
                    {
                        context.AddFailure("ImageFile", "ImageFile file must be less or equal that 2MB");

                    }
                    if (x.ImageFile.ContentType != "image/jpeg" && x.ImageFile.ContentType != "image/png")
                    {
                        context.AddFailure("ImageFile", "ImageFile must be png , jpg or jpeg file");

                    }
                }
            });
        }
    }
}
