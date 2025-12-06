using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.Entities.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int BinID { get; set; }
        public int AssignedStaffID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Status { get; set; }

        // Navigation properties (optional)
        public string Location { get; set; }
        public string AssignedStaff { get; set; }
    }
}
