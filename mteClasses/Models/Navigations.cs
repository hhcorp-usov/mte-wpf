using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mteModels.Models
{
    public enum GuidesElements 
    { 
        Enterprises,
        Posts,
        Workers,
        Users
    }

    public class MenuNavigatorItem
    {
        public GuidesElements Id { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
    }
}
