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
        Users,
        CarTypes,
        Cars,
        PointTypes,
        Points,
        Routes
    }

    public class MenuNavigatorItem
    {
        public GuidesElements Id { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string DialogName { get; set; }
    }
}
