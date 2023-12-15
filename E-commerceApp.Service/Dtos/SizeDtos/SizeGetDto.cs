using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.SizeDtos
{
    public class SizeGetDto
    {
        public string Name { get; set; }

        public int ProductsCount { get; set; }
    }

    public class SizeGetDtoValidator : AbstractValidator<SizeGetDto>
    {
        public SizeGetDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(10).MinimumLength(1);
        }
    }
}
