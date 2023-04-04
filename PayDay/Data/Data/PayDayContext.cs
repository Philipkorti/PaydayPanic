using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class PayDayContext :DbContext
    {
        public DbSet<Gold> Golds { get; set; } = null;
        public DbSet<Items> Items { get; set; } = null;
        public DbSet<User> Users { get; set; } = null;
    }
}
