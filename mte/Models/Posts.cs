using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mte.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
