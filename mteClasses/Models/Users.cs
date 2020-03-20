using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mteModels.Models
{
    public class Users : IGuidesItem
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int PostsId { get; set; }

        public virtual Posts Posts { get; set; }

        public int DeleteItem(DatabaseContext _dbContext)
        {
            Users _item = _dbContext.Users.Find(this.Id);
            _dbContext.Users.Remove(_item);
            return _dbContext.SaveChanges();
        }

        public int SaveItem(DatabaseContext _dbContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
