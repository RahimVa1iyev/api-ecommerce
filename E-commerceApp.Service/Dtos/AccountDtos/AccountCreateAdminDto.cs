using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.AccountDtos
{
    public class AccountCreateAdminDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string  Role { get; set; }
    }

    public class AccountCreateAdminDtoValidator : AbstractValidator<AccountCreateAdminDto>
    {
        public AccountCreateAdminDtoValidator()
        {
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
            RuleFor(x => x.Role).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(25);
        }
    }
}
