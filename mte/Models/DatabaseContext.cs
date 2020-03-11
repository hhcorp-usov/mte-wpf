using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mte.Models
{
    public interface IDataList
    {
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Enterprises> Enterprises { get; set; }
        public DbSet<Posts> Posts { get; set; }

        public DatabaseContext(string DatabaseConnectionString) : base(DatabaseConnectionString)
        {
        }
    }
}
