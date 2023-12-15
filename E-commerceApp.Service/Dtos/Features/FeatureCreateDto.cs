using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Dtos.Features
{
    public class FeatureCreateDto
    {

        public string FTitle { get; set; }

        public string STitle { get; set; }

        public string TTitle { get; set; }

        public string Desc { get; set; }

        public IFormFile ImageUri { get; set; }
    }
}
