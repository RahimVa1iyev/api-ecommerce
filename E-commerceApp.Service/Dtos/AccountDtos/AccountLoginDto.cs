using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.AccountDtos
{
    public class AccountLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class AccountLoginDtoValidator : AbstractValidator<AccountLoginDto>
    {
        public AccountLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(20).MinimumLength(2);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(25).MinimumLength(8);
        }
    }
}
