using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Catid { get; set; }
        [ForeignKey("Catid")]
        public TypeProduct TypeP { get; set; }
    } 
}
