using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Controls;

namespace mteModels.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Enterprises> Enterprises { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Workers> Workers { get; set; }
        public DbSet<CarTypes> CarTypes { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<PointTypes> PointTypes { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<Routes> Routes { get; set; }

        public DatabaseContext(string DatabaseConnectionString) : base(DatabaseConnectionString)
        {
        }
    }
}
