using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mteModels.Models
{
    public enum WaybillsWorkersStatus
    {
        Driver = 1,
        Conductor = 2,
        Trainee = 3
    }

    public enum WayBillsStatus 
    { 
        Planned = 1
    }

    public class WayBills
    {
        [Key]
        public int Id { get; set; }
        public int Status { get; set; }
        public string Nomer { get; set; }
        public int CarsId { get; set; }

        public List<WayBillsCrews> WayBillsCrews { get; set; }

        public WayBills()
        {
            
        }
    }

    public class WayBillsCrews 
    {
        [Key]
        public int Id { get; set; }
        public int WaybillsId { get; set; }
        public int WorkersId { get; set; }
        public WaybillsWorkersStatus WorkersStatus { get; set; }

    }
}
