using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Models
{
    [Keyless]
    public class Comment
    {
        public int Pid { get; set; }
        public int Aid { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        [ForeignKey("Pid")]
        public virtual Product pid { get; set; }
        [ForeignKey("Aid")]
        public virtual Account aid { get; set; }
    }
}
