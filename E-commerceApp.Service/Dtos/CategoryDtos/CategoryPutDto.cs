using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.CategoryDtos
{
    public class CategoryPutDto
    {
        public string Name { get; set; }
    }
    public class CategoryPutDtoValidator : AbstractValidator<CategoryPutDto>
    {
        public CategoryPutDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
        }
    }
}
