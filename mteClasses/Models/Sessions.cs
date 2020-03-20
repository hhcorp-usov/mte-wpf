using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public static int GuidesItemSave(IGuidesItem item)
        {
            return (item != null ? item.SaveItem(_dbContext) : 0);
        }

        public static int GuidesItemDelete(IGuidesItem item)
        {
            return (item != null ? item.DeleteItem(_dbContext) : 0);
        }

        // Items list

        public static IReadOnlyList<IGuidesItem> GetDataGridGuidesItems(MenuNavigatorItem MenuItem)
        {
            switch (MenuItem.Id)
            {
                case GuidesElements.Enterprises: return GetEnterprisesList();
                case GuidesElements.Posts: return GetPostsList();
                case GuidesElements.Workers: return GetWorkersList();
                case GuidesElements.Users: return GetUsersList();
                case GuidesElements.CarTypes: return GetCarTypesList();
                case GuidesElements.Cars: return GetCarsList();
                case GuidesElements.PointTypes: return GetPointsList();
                case GuidesElements.Points: return GetPointTypesList();
                case GuidesElements.Routes: return GetRoutesList();
                default: return null;
            }
        }

        // Datagrid header

        public static ObservableCollection<DataGridColumn> GetDataGridGuidesColumns(MenuNavigatorItem MenuItem)
        {
            switch (MenuItem.Id)
            {
                case GuidesElements.Enterprises:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } },
                        new DataGridTextColumn() { Header = "INN", Width = 100, Binding = new Binding() { Path = new PropertyPath("Inn") } }
                    };

                case GuidesElements.Posts:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } }
                    };

                case GuidesElements.Workers:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "FIRSTNAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("FirstName") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } },
                        new DataGridTextColumn() { Header = "LASTNAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("LastName") } },
                        new DataGridTextColumn() { Header = "POST", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Posts.Name") } },
                        new DataGridTextColumn() { Header = "ENTERPRISE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Enterprises.Name") } }
                    };

                case GuidesElements.Users:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "LOGIN", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Login") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } },
                        new DataGridTextColumn() { Header = "POST", Width = 100, Binding = new Binding() { Path = new PropertyPath("Posts.Name") } }
                    };

                case GuidesElements.CarTypes:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } }
                    };

                case GuidesElements.Cars:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "INOMER", Width = 100, Binding = new Binding() { Path = new PropertyPath("INomer") } },
                        new DataGridTextColumn() { Header = "SNOMER", Width = 100, Binding = new Binding() { Path = new PropertyPath("SNomer") } },
                        new DataGridTextColumn() { Header = "CARTYPE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("CarTypes.Name") } },
                        new DataGridTextColumn() { Header = "ENTERPRISE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Enterprises.Name") } }
                    };

                case GuidesElements.PointTypes:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } },
                        new DataGridTextColumn() { Header = "SHORTNAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("ShortName") } }
                    };

                case GuidesElements.Points:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "POINTYPE", Width = 100, Binding = new Binding() { Path = new PropertyPath("PointTypes.ShortName") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } }
                    };

                case GuidesElements.Routes:
                    return new ObservableCollection<DataGridColumn>
                    {
                        new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } },
                        new DataGridTextColumn() { Header = "NOMER", Width = 100, Binding = new Binding() { Path = new PropertyPath("Nomer") } },
                        new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } }
                    };

                default: return null;
            }
        }

        // Lists

        public static List<Enterprises> GetEnterprisesList()
        {
            return _dbContext.Enterprises.ToList();
        }

        public static List<Workers> GetWorkersList()
        {
            return _dbContext.Workers.ToList();
        }

        public static List<Cars> GetCarsList()
        {
            return _dbContext.Cars.ToList();
        }

        public static List<Points> GetPointsList()
        {
            return _dbContext.Points.ToList();
        }

        public static List<Users> GetUsersList()
        {
            return _dbContext.Users.ToList();
        }

        public static List<Posts> GetPostsList()
        {
            return _dbContext.Posts.ToList();
        }

        public static List<CarTypes> GetCarTypesList()
        {
            return _dbContext.CarTypes.ToList();
        }

        public static List<PointTypes> GetPointTypesList()
        {
            return _dbContext.PointTypes.ToList();
        }

        public static List<Routes> GetRoutesList()
        {
            return _dbContext.Routes.ToList();
        }

    }
}
