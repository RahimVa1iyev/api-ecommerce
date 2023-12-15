using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.SizeDtos
{
    public class SizeGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductsCount { get; set; }

    }

    public class SizeGetAllDtoValidator : AbstractValidator<SizeGetAllDto>
    {
        public SizeGetAllDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(10).MinimumLength(1);
        }
    }
}
