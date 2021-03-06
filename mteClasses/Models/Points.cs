﻿using System;
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
    public class Points : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PointTypesId { get; set; }

        public virtual PointTypes PointTypes { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            Points _item = _dbContext.Points.Find(this.Id);
            _dbContext.Points.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                Points _item = _dbContext.Points.Find(this.Id);
                _item.Name = this.Name;
                _item.PointTypesId = this.PointTypesId;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Points.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
