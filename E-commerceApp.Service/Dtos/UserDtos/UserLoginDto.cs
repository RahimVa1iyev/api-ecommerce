using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserLoginDto
    {
        public string UserName { get; set; }

        public string  Password { get; set; }
    }

    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.Password).NotEmpty()
           .MinimumLength(6);
        }
    }
}
