using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.SizeDtos
{
    public class SizePutDto
    {
        public string Name { get; set; }

    }

    public class SizePutDtoValidator : AbstractValidator<SizePutDto>
    {
        public SizePutDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(10).MinimumLength(1);
        }
    }
}
