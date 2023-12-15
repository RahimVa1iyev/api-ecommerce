using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Offers
{
    public class OfferCreateDto
    {

        public string Title { get; set; }

        public string Desc { get; set; }

        public IFormFile Icon { get; set; }
    }
}
