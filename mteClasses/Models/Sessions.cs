using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mteModels.Models
{
    public static class CurrentSession
    {
        // database context
        public static DatabaseContext _dbContext;
        public static void DatabaseConnect() { _dbContext = new DatabaseContext(mteModels.Properties.Resources.DatabaseConnectionString); }

        public static Users CurrentUser;
        public static string GetCurrentUserView()
        {
            if (CurrentUser != null) return String.Format("{0} ({1})", CurrentUser.Name, CurrentUser.Posts.Name);
            else return "None";
        }

        public static IReadOnlyList<IDataList> GetGuidesDataForDataGrid(string GuidesName)
        {
            IReadOnlyList<IDataList> res = null;
            switch (GuidesName.ToLower().Trim())
            {
                case "enterprises":
                    res = _dbContext.Enterprises.OrderBy(o => o.Id).ToList();
                    break;

                case "posts":
                    res = _dbContext.Posts.OrderBy(o=>o.Id).ToList();
                    break;

                case "workers":
                    res = _dbContext.Workers.OrderBy(o => o.Id).ToList();
                    break;

                case "users":
                    res = _dbContext.Users.OrderBy(o => o.Id).ToList();
                    break;
            }
            return res;
        }

        public static List<Users> GetUsersList()
        {
            return _dbContext.Users.OrderBy(o => o.Name).ToList();
        }

        public static List<Enterprises> GetEnterprisesList()
        {
            return _dbContext.Enterprises.OrderBy(o => o.Name).ToList();
        }

        public static List<Posts> GetPostsList()
        {
            return _dbContext.Posts.OrderBy(o => o.Name).ToList();
        }
    }
}
