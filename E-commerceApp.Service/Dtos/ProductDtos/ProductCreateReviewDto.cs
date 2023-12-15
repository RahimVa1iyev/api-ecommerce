using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.ProductDtos
{
    public class ProductCreateReviewDto
    {
        public int ProductId { get; set; }

        public int Rate { get; set; }

        public string Text { get; set; }
    }

    public class ReviewCreateReviewDtoValidator : AbstractValidator<ProductCreateReviewDto>
    {
        public ReviewCreateReviewDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Rate).InclusiveBetween(1, 5);

        }
    }
}
