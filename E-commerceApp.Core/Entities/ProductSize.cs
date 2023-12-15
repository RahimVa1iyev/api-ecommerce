﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Core.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }

        public Product Product { get; set; }

        public Size Size { get; set; }
    }
}
