using E_commerceApp.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class UserProfileDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
           
        }
    }
}
