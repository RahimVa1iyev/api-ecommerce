using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.AccountDtos
{
    public class AccountAdminGetDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public IList<string> Role { get; set; }
    }
}
