using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.CategoryDtos
{
    public class CategoryGetDto
    {
        public string Name { get; set; }

        public int ProductsCount { get; set; }
    }
    public class CategoryGetDtoValidator : AbstractValidator<CategoryGetDto>
    {
        public CategoryGetDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
