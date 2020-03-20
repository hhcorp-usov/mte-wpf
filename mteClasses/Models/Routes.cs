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
    public class Routes : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nomer { get; set; }
        public string BackName { get; set; }
        public string LineName { get; set; }
        public int EnterprisesId { get; set; }

        public virtual Enterprises Enterprises { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            var _item = _dbContext.Routes.Find(this.Id);
            _dbContext.Routes.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                var _item = _dbContext.Routes.Find(this.Id);
                _item.Name = this.Name;
                _item.Nomer= this.Nomer;
                _item.BackName = this.BackName;
                _item.LineName = this.LineName;
                _item.EnterprisesId = this.EnterprisesId;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Routes.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
