using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.UserDtos
{
    public class UserVerifyDto
    {
        public string Email { get; set; }

        public int Code { get; set; }
    }
}
