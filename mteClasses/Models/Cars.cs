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
    public class Cars : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string INomer { get; set; }
        public string SNomer { get; set; }
        public int EnterprisesId { get; set; }
        public int CarTypesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }
        public virtual CarTypes CarTypes { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            var _item = _dbContext.Cars.Find(this.Id);
            _dbContext.Cars.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                var _item = _dbContext.Cars.Find(this.Id);
                _item.INomer = this.INomer;
                _item.EnterprisesId = this.EnterprisesId;
                _item.CarTypesId = this.CarTypesId;
                _item.SNomer = this.SNomer;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Cars.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
