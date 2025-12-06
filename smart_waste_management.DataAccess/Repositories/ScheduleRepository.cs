using smart_waste_management.DataAccess.Interfaces;
using smart_waste_management.DataAccess.Repositories;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public ScheduleRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public List<Schedule> GetAllSchedules()
        {
            var schedules = new List<Schedule>();

            using (var reader = _dbHelper.ExecuteReader("sp_GetAllSchedules", null))
            {
                while (reader.Read())
                {
                    schedules.Add(new Schedule
                    {
                        ScheduleID = Convert.ToInt32(reader["ScheduleID"]),
                        Location= reader["Location"].ToString(),
                        AssignedStaff = reader["AssignedStaff"] == DBNull.Value ? "Unassigned" : reader["AssignedStaff"].ToString(),
                        ScheduledDate = Convert.ToDateTime(reader["ScheduledDate"]),
                        CompletedDate = reader["CompletedDate"] == DBNull.Value ? null : (DateTime?)reader["CompletedDate"],
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return schedules;
        }

        public List<Schedule> GetSchedulesByStaff(int staffId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateScheduleStatus(int scheduleId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
