using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class Slider
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Title { get; set; }

        public string SecondTitle { get; set; }

        public string Description { get; set; }

        public string ButtonUrl { get; set; }

        public string ButtonText { get; set; }

        public string BgColor { get; set; }

        public string Image { get; set; }
    }
}
