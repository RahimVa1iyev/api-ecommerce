using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

        }
    }
}
