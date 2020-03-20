using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class PointTypes : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            PointTypes _item = _dbContext.PointTypes.Find(this.Id);
            _dbContext.PointTypes.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                PointTypes _item = _dbContext.PointTypes.Find(this.Id);
                _item.Name = this.Name;
                _item.ShortName = this.ShortName;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.PointTypes.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
