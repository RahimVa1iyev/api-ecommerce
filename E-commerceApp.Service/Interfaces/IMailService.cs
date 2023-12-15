using E_commerceApp.Service.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Interfaces
{
    public interface IMailService
    {

        Task SendEmailAsync(MailRequest mailRequest);
    }
}
