using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ColorDtos
{
    public class ColorPutDto
    {
        public string Name { get; set; }
    }

    public class ColorPutDtoValidator : AbstractValidator<ColorPutDto>
    {
        public ColorPutDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
