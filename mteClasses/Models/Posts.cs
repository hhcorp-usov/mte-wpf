﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace mteModels.Models
{
    public class Posts : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class PostsHelper
    {
        public static ObservableCollection<DataGridColumn> GetDataGridColumns()
        {
            ObservableCollection<DataGridColumn> res = new ObservableCollection<DataGridColumn>();
            res.Add(new DataGridTextColumn() { Header = "ID", Width = 50, Binding = new Binding() { Path = new PropertyPath("Id") } });
            res.Add(new DataGridTextColumn() { Header = "NAME", Width = new DataGridLength(100, DataGridLengthUnitType.Star), Binding = new Binding() { Path = new PropertyPath("Name") } });
            return res;
        }

        public static IReadOnlyList<IDataList> GetDataGridItems(DatabaseContext _dbContext)
        {
            return _dbContext.Posts.OrderBy(o => o.Id).ToList();
        }
    }
}
