using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.CategoryDtos
{
    public class CategoryGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductsCount { get; set; }

    }

    public class CategoryGetAllDtoValidator : AbstractValidator<CategoryGetAllDto>
    {
        public CategoryGetAllDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
