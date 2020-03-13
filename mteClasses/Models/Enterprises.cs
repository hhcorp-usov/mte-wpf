using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mteModels.Models
{
    public class Enterprises : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }

        public int SaveChanges(IDialogParameters Parameters) 
        {
            if (Parameters.Count > 0)
            {
                Enterprises _item = Parameters.GetValue<Enterprises>("Item");
                
                this.Id = _item.Id;
                this.Inn = _item.Inn;
                this.Name = _item.Name;

                if (this.Id > 0) CurrentSession._dbContext.Entry(this).State = EntityState.Modified;
                else CurrentSession._dbContext.Enterprises.Add(this);
                
                return CurrentSession._dbContext.SaveChanges();
            }
            else
                return -1;
        }
    }
}
