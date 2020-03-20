using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class Enterprises : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                var _item = _dbContext.Enterprises.Find(this.Id);
                _item.Inn = this.Inn;
                _item.Name = this.Name;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Enterprises.Add(this);
            return _dbContext.SaveChanges();
        }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            var _item = _dbContext.Enterprises.Find(this.Id);
            _dbContext.Enterprises.Remove(_item);
            return _dbContext.SaveChanges();
        }
    }
}
