using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.Entities.Models
{
    public class PickupRequest
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        public int BinID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequestedPickupDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string AdminNotes { get; set; }

        // Navigation properties (optional)
        public string UserName { get; set; }
        public string Location { get; set; }
        public string WasteType { get; set; }
        public double Weight { get; set; }
        public string Comments { get; set; }
    }
}
