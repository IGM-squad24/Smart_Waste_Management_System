using smart_waste_management.BusinessLogic.Interfaces;
using smart_waste_management.DataAccess.Interfaces;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.BusinessLogic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService()
        {
            _scheduleRepository = new smart_waste_management.DataAccess.Repositories.ScheduleRepository();
        }

        public List<Schedule> GetAllSchedules()
        {
            return _scheduleRepository.GetAllSchedules();
        }

        public List<Schedule> GetSchedulesByStaff(int staffId)
        {
            if (staffId <= 0)
                throw new System.ArgumentException("Valid Staff ID is required");

            return _scheduleRepository.GetSchedulesByStaff(staffId);
        }

        public bool UpdateScheduleStatus(int scheduleId, string status)
        {
            if (scheduleId <= 0)
                throw new System.ArgumentException("Valid Schedule ID is required");

            var validStatuses = new List<string> { "Scheduled", "In Progress", "Completed", "Cancelled" };
            if (!validStatuses.Contains(status))
                throw new System.ArgumentException("Invalid schedule status");

            return _scheduleRepository.UpdateScheduleStatus(scheduleId, status);
        }
    }
}