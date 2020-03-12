using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mteModels.Models
{
    public class Workers : IDataList
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
    }
}
