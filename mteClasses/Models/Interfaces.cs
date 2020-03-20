using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mteModels.Models
{
    public interface IGuidesItem
    {
        int SaveItem(DatabaseContext _dbContext);
        int DeleteItem(DatabaseContext _dbContext);
    }
}