using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ColorDtos
{
    public class ColorPostDto
    {
        public string Name { get; set; }
    }

    public class ColorPostDtoValidator : AbstractValidator<ColorPostDto>
    {
        public ColorPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
