using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Text { get; set; }
    }

    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Text).NotEmpty().MaximumLength(500);
        }
    }
}


