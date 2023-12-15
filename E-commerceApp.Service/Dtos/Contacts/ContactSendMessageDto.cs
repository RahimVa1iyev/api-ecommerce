using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Contacts
{
    public class ContactSendMessageDto
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

    }

    public class ContactSendMessageDtoValidator : AbstractValidator<ContactSendMessageDto>
    {
        public ContactSendMessageDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Text).NotEmpty().MaximumLength(500);

        }
    }
}
