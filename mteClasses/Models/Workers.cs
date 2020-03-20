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
    public class Workers : IGuidesItem
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

        public int DeleteItem(DatabaseContext _dbContext)
        {
            Workers _item = _dbContext.Workers.Find(this.Id);
            _dbContext.Workers.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                Workers _item = _dbContext.Workers.Find(this.Id);
                _item.Name = this.Name;
                _item.EnterprisesId = this.EnterprisesId;
                _item.PostsId = this.PostsId;
                _item.FirstName = this.FirstName;
                _item.LastName = this.LastName;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Workers.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
