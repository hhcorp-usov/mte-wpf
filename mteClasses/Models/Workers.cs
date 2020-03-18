using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class Workers : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PostsId { get; set; }
        public int EnterprisesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual Posts Posts { get; set; }
    }

    public static class WorkersHelper
    {
        public static ObservableCollection<DataGridColumn> GetDataGridColumns()
        {
            ObservableCollection<DataGridColumn> res = new ObservableCollection<DataGridColumn>();
            res.Add(new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } });
            res.Add(new DataGridTextColumn() { Header = "FIRSTNAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("FirstName") } });
            res.Add(new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } });
            res.Add(new DataGridTextColumn() { Header = "LASTNAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("LastName") } });
            res.Add(new DataGridTextColumn() { Header = "POST", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Posts.Name") } });
            res.Add(new DataGridTextColumn() { Header = "ENTERPRISE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Enterprises.Name") } });
            return res;
        }

        public static IReadOnlyList<IDataList> GetDataGridItems(DatabaseContext _dbContext)
        {
            return _dbContext.Workers.OrderBy(o => o.Id).ToList();
        }

        public static int DeleteDataGridItem(DatabaseContext _dbContext, Workers SelectedItem)
        {
            Workers _item = _dbContext.Workers.Where(w => w.Id == SelectedItem.Id).SingleOrDefault();
            _dbContext.Workers.Remove(_item);
            return _dbContext.SaveChanges();
        }
    }
}
