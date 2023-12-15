using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.BrandDtos
{
    public class BrandGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductsCount { get; set; }
    }

    public class BrandGetAllDtoValidator : AbstractValidator<BrandGetAllDto>
    {
        public BrandGetAllDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(30);
        }
    }
}
