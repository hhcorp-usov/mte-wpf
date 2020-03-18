using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class Cars : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string INomer { get; set; }
        public string SNomer { get; set; }
        public int EnterprisesId { get; set; }
        public int CarTypesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual CarTypes CarTypes { get; set; }
    }

    public static class CarsHelper
    {
        public static ObservableCollection<DataGridColumn> GetDataGridColumns()
        {
            ObservableCollection<DataGridColumn> res = new ObservableCollection<DataGridColumn>();
            res.Add(new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } });
            res.Add(new DataGridTextColumn() { Header = "INOMER", Width = 100, Binding = new Binding() { Path = new PropertyPath("INomer") } });
            res.Add(new DataGridTextColumn() { Header = "SNOMER", Width = 100, Binding = new Binding() { Path = new PropertyPath("SNomer") } });
            res.Add(new DataGridTextColumn() { Header = "CARTYPE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("CarTypes.Name") } });
            res.Add(new DataGridTextColumn() { Header = "ENTERPRISE", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Enterprises.Name") } });
            return res;
        }

        public static IReadOnlyList<IDataList> GetDataGridItems(DatabaseContext _dbContext)
        {
            return _dbContext.Cars.OrderBy(o => o.Id).ToList();
        }

        public static int DeleteDataGridItem(DatabaseContext _dbContext, Cars SelectedItem)
        {
            Cars _item = _dbContext.Cars.Where(w => w.Id == SelectedItem.Id).SingleOrDefault();
            _dbContext.Cars.Remove(_item);
            return _dbContext.SaveChanges();
        }
    }
}
