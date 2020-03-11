using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mte.Models
{
    public static class CurrentSession
    {
        // database context
        private static DatabaseContext _dbContext;
        public static void DatabaseConnect() { _dbContext = new DatabaseContext(mte.Properties.Resources.DatabaseConnectionString); }

        public static Users CurrentUser;
        public static string GetCurrentUserView()
        {
            if (CurrentUser != null) return String.Format("{0} ({1})", CurrentUser.Name, CurrentUser.Posts.Name);
            else return "None";
        }

        public static List<Users> GetUsersList()
        {
            return _dbContext.Users.OrderBy(o => o.Name).ToList();
        }

        public static List<Enterprises> GetEnterprisesList()
        {
            return _dbContext.Enterprises.OrderBy(o => o.Inn).ToList();
        }
    }
}
