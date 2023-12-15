using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class UserContact
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
     
        public string FullName { get; set; }
      
        public string Phone { get; set; }
    
        public string Email { get; set; }
    
        public string Subject { get; set; }

        public string Text { get; set; }

        public AppUser AppUser { get; set; }

        public string Response { get; set; }

    }


}
