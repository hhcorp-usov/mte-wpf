using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public static class SessionsHelper
    {
        // database context
        private static DatabaseContext _dbContext;
        public static void DatabaseConnect() { _dbContext = new DatabaseContext(mteModels.Properties.Resources.DatabaseConnectionString); }

        public static Users CurrentUser;
        public static string GetCurrentUserView()
        {
            if (CurrentUser != null) return String.Format("{0} ({1})", CurrentUser.Name, CurrentUser.Posts.Name);
            else return "None";
        }

        public static IReadOnlyList<IDataList> GetDataGridGuidesItems(GuidesElements GuidesName)
        {
            IReadOnlyList<IDataList> res = null;
            switch (GuidesName)
            {
                case GuidesElements.Enterprises:
                    res = EnterprisesHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.Posts:
                    res = PostsHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.Workers:
                    res = WorkersHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.Users:
                    res = UsersHelper.GetDataGridItems(_dbContext);
                    break;
            }
            return res;
        }

        public static ObservableCollection<DataGridColumn> GetDataGridGuidesColumns(GuidesElements GuidesName)
        {
            ObservableCollection<DataGridColumn> res = new ObservableCollection<DataGridColumn>();
            switch (GuidesName)
            {
                case GuidesElements.Enterprises:
                    res = EnterprisesHelper.GetDataGridColumns();
                    break;

                case GuidesElements.Posts:
                    res = PostsHelper.GetDataGridColumns();
                    break;

                case GuidesElements.Workers:
                    res = WorkersHelper.GetDataGridColumns(); 
                    break;

                case GuidesElements.Users:
                    res = UsersHelper.GetDataGridColumns(); 
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

        public static int EnterprisesSaveChanges(Enterprises item)
        {
            if (item.Id > 0)
            {
                Enterprises _item = _dbContext.Enterprises.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Inn = item.Inn;
                _item.Name = item.Name;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Enterprises.Add(item);
            return _dbContext.SaveChanges();
        }

        public static int PostsSaveChanges(Posts item)
        {
            if (item.Id > 0)
            {
                Posts _item = _dbContext.Posts.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Name = item.Name;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Posts.Add(item);
            return _dbContext.SaveChanges();
        }

        public static int WorkersSaveChanges(Workers item)
        {
            if (item.Id > 0)
            {
                Workers _item = _dbContext.Workers.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Name = item.Name;
                _item.EnterprisesId = item.EnterprisesId;
                _item.PostsId = item.PostsId;
                _item.FirstName = item.FirstName;
                _item.LastName = item.LastName;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Workers.Add(item);
            return _dbContext.SaveChanges();
        }
    }
}
