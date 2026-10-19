using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class ProductListDto
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Detail> ProductDescriptions { get; set; }
    }
}