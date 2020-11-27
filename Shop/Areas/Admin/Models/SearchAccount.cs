using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Shop.Areas.Admin.Models
{
    public class SearchAccount
    {
        public List<Account> Accounts { get; set; }
        public SelectList ListAccount { get; set; }
        public string TypeAccount { get; set; }
        public string SearchString { get; set; }
    }
}
