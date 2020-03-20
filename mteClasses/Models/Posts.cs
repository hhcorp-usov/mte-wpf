﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace mteModels.Models
{
    public class Posts : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            Posts _item = _dbContext.Posts.Find(this.Id);
            _dbContext.Posts.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            if (this.Id > 0)
            {
                Posts _item = _dbContext.Posts.Find(this.Id);
                _item.Name = this.Name;
                _dbContext.Entry(_item).State = EntityState.Modified;
            }
            else _dbContext.Posts.Add(this);
            return _dbContext.SaveChanges();
        }
    }
}
