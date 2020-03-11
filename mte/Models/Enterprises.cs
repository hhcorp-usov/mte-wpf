using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mte.Models
{
    public class Enterprises : IDataList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }
    }
}
