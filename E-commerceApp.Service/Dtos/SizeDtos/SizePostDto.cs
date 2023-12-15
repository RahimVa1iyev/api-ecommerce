using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.SizeDtos
{
    public class SizePostDto
    {
        public string Name { get; set; }
    }
    public class SizePostDtoValidator : AbstractValidator<SizePostDto>
    {
        public SizePostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(10).MinimumLength(1);
        }
    }
}
