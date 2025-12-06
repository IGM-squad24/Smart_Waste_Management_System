using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.Entities.Models
{
    public class Bin
    {
        public int BinID { get; set; }
        public string Location { get; set; }
        public decimal Capacity { get; set; }
        public decimal CurrentLevel { get; set; }
        public string Status { get; set; }
        public DateTime? LastEmptied { get; set; }
        public DateTime? NextScheduledEmpty { get; set; }
    }
}
