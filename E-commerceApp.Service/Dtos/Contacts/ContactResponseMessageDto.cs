using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Contacts
{
    public class ContactResponseMessageDto
    {
        public int Id { get; set; }

        public string Response { get; set; }
    }

    public class ContactResponseMessageDtoValidator : AbstractValidator<ContactResponseMessageDto>
    {
        public ContactResponseMessageDtoValidator()
        {
            RuleFor(x => x.Response).NotEmpty().MaximumLength(700);
        }
    }
}
