using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product product { get; set; }
    }
}
