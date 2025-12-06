using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Interfaces
{
    public interface IScheduleRepository
    {
        List<Schedule> GetAllSchedules();
        List<Schedule> GetSchedulesByStaff(int staffId);
        bool UpdateScheduleStatus(int scheduleId, string status);
    }
}
