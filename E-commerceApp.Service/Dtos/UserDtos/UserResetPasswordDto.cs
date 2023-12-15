using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserResetPasswordDto
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserId { get; set; }
    }

    public class UserResetPasswordDtoValidator : AbstractValidator<UserResetPasswordDto>
    {
        public UserResetPasswordDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().MaximumLength(30);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MaximumLength(30);
            RuleFor(x => x.UserId).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Token).NotEmpty().MaximumLength(800);
        }
    }
}
