using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class Points : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PointTypesId { get; set; }

        public virtual PointTypes PointTypes { get; set; }
    }

    public static class PointsHelper
    {
        public static ObservableCollection<DataGridColumn> GetDataGridColumns()
        {
            ObservableCollection<DataGridColumn> res = new ObservableCollection<DataGridColumn>();
            res.Add(new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } });
            res.Add(new DataGridTextColumn() { Header = "POINTYPE", Width = 100, Binding = new Binding() { Path = new PropertyPath("PointTypes.ShortName") } });
            res.Add(new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } });
            return res;
        }

        public static IReadOnlyList<IDataList> GetDataGridItems(DatabaseContext _dbContext)
        {
            return _dbContext.Points.OrderBy(o => o.Id).ToList();
        }

        public static int DeleteDataGridItem(DatabaseContext _dbContext, Points SelectedItem)
        {
            Points _item = _dbContext.Points.Where(w => w.Id == SelectedItem.Id).SingleOrDefault();
            _dbContext.Points.Remove(_item);
            return _dbContext.SaveChanges();
        }
    }
}
