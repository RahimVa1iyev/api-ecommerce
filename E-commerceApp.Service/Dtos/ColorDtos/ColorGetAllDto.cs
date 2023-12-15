using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ColorDtos
{
    public class ColorGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductsCount { get; set; }

    }

    public class ColorGetAllDtoValidator : AbstractValidator<ColorGetAllDto>
    {
        public ColorGetAllDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
