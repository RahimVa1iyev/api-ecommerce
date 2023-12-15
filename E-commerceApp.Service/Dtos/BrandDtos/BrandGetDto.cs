using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.BrandDtos
{
    public class BrandGetDto
    {
        public string Name { get; set; }

        public int ProductsCount { get; set; }
    }

    public class BrandGetDtoValidator : AbstractValidator<BrandGetDto>
    {
        public BrandGetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(30);
        }
    }
}
