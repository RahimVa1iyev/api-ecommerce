using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public int MailConfirmCode { get; set; }

        public int MailConfirmCodeForPassword { get; set; }

        public bool IsLogin { get; set; }

        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
