using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mteModels.Models
{
    public interface IDataList
    {
    }

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

        public DatabaseContext(string DatabaseConnectionString) : base(DatabaseConnectionString)
        {
        }
    }
}
