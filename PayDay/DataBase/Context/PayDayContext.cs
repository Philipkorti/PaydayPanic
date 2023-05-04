using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Context
{
    public class PayDayContext : DbContext
    {
        public PayDayContext() : base("MyConnectionString") { }
        public DbSet<Items> Items { get; set; } = null;
        public DbSet<User> User { get; set; } = null;
        public DbSet<Casino> Casino { get; set; } = null;
        public DbSet<Highscore> Highscore { get; set; } = null;
        public DbSet<Shop> Shop { get; set; } = null;
        public DbSet<Statistics> Statistics { get; set; } = null;

        public DbSet<Ranks> Ranks { get; set; }
    }
}
