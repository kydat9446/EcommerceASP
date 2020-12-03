using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Models
{
    public class SearchProduct
    {
        public List<Product> Products { get; set; }
        public SelectList ListProduct { get; set; }
        public string TypeProduct { get; set; }
        public string SearchString { get; set; }
    }
}
