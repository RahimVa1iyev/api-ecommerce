using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ColorDtos
{
    public class ColorGetDto
    {
        public string Name { get; set; }

        public int ProductsCount { get; set; }

    }

    public class ColorGetDtoValidator : AbstractValidator<ColorGetDto>
    {
        public ColorGetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
