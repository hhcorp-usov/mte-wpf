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

                case GuidesElements.CarTypes:
                    res = CarTypesHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.Cars:
                    res = CarsHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.PointTypes:
                    res = PointTypesHelper.GetDataGridItems(_dbContext);
                    break;

                case GuidesElements.Points:
                    res = PointsHelper.GetDataGridItems(_dbContext);
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

                case GuidesElements.CarTypes:
                    res = CarTypesHelper.GetDataGridColumns();
                    break;

                case GuidesElements.Cars:
                    res = CarsHelper.GetDataGridColumns();
                    break;

                case GuidesElements.PointTypes:
                    res = PointTypesHelper.GetDataGridColumns();
                    break;

                case GuidesElements.Points:
                    res = PointsHelper.GetDataGridColumns();
                    break;
            }
            return res;
        }

        public static int DeleteDataGridGuidesItem(GuidesElements GuidesName, object SelectedItem)
        {
            int res = 0;
            switch (GuidesName)
            {
                case GuidesElements.Enterprises:
                    res = EnterprisesHelper.DeleteDataGridItem(_dbContext, (Enterprises)SelectedItem);
                    break;

                case GuidesElements.Posts:
                    res = PostsHelper.DeleteDataGridItem(_dbContext, (Posts)SelectedItem);
                    break;

                case GuidesElements.Workers:
                    res = WorkersHelper.DeleteDataGridItem(_dbContext, (Workers)SelectedItem);
                    break;

                case GuidesElements.Users:
                    res = UsersHelper.DeleteDataGridItem(_dbContext, (Users)SelectedItem);
                    break;

                case GuidesElements.CarTypes:
                    res = CarTypesHelper.DeleteDataGridItem(_dbContext, (CarTypes)SelectedItem);
                    break;

                case GuidesElements.Cars:
                    res = CarsHelper.DeleteDataGridItem(_dbContext, (Cars)SelectedItem);
                    break;

                case GuidesElements.PointTypes:
                    res = PointTypesHelper.DeleteDataGridItem(_dbContext, (PointTypes)SelectedItem);
                    break;

                case GuidesElements.Points:
                    res = PointsHelper.DeleteDataGridItem(_dbContext, (Points)SelectedItem);
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

        public static List<CarTypes> GetCarTypesList()
        {
            return _dbContext.CarTypes.OrderBy(o => o.Name).ToList();
        }

        public static List<PointTypes> GetPointTypesList()
        {
            return _dbContext.PointTypes.OrderBy(o => o.Name).ToList();
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

        public static int CarTypesSaveChanges(CarTypes item)
        {
            if (item.Id > 0)
            {
                CarTypes _item = _dbContext.CarTypes.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Name = item.Name;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.CarTypes.Add(item);
            return _dbContext.SaveChanges();
        }

        public static int CarsSaveChanges(Cars item)
        {
            if (item.Id > 0)
            {
                Cars _item = _dbContext.Cars.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.INomer = item.INomer;
                _item.EnterprisesId = item.EnterprisesId;
                _item.CarTypesId = item.CarTypesId;
                _item.SNomer= item.SNomer;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Cars.Add(item);
            return _dbContext.SaveChanges();
        }

        public static int PointTypesSaveChanges(PointTypes item)
        {
            if (item.Id > 0)
            {
                PointTypes _item = _dbContext.PointTypes.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Name = item.Name;
                _item.ShortName = item.ShortName;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.PointTypes.Add(item);
            return _dbContext.SaveChanges();
        }

        public static int PointsSaveChanges(Points item)
        {
            if (item.Id > 0)
            {
                Points _item = _dbContext.Points.Where(w => w.Id == item.Id).SingleOrDefault();
                _item.Name = item.Name;
                _item.PointTypesId = item.PointTypesId;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Points.Add(item);
            return _dbContext.SaveChanges();
        }

    }
}
