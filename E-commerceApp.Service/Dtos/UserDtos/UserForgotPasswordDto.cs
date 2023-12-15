using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserForgotPasswordDto
    {
        public string Email { get; set; }
    }

    public class UserForgotPasswordDtoValidator : AbstractValidator<UserForgotPasswordDto>
    {
        public UserForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        }
    }
}
