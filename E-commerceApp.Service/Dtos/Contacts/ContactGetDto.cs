using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Contacts
{
    public class ContactGetDto
    {

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Note { get; set; }
    }
}
