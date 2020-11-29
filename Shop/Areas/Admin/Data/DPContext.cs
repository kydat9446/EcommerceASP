using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Areas.Admin.Data
{
    public class DPContext : DbContext
    {

        public DPContext(DbContextOptions<DPContext> options)
            : base(options)
        {
        }
        public DbSet<Product> product { get; set; }
        public DbSet<TypeProduct> typeProduct { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<TypeAccount> typeAccount { get; set; }
        public DbSet<Comment> comment { get; set; }
    }
}
