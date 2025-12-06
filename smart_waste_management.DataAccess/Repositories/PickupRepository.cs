using smart_waste_management.DataAccess.Interfaces;
using smart_waste_management.DataAccess.Repositories;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Repositories
{
    public class PickupRepository : IPickupRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public PickupRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public int CreatePickupRequest(PickupRequest request)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", request.UserID),
                new SqlParameter("@BinID", request.BinID),
                new SqlParameter("@RequestedPickupDate", request.RequestedPickupDate),
                new SqlParameter("@Notes", request.Notes ?? (object)DBNull.Value)
            };

            var result = _dbHelper.ExecuteScalar("sp_CreatePickupRequest", parameters);
            return Convert.ToInt32(result);
        }

        public List<PickupRequest> GetUserPickupRequests(int userId)
        {
            var requests = new List<PickupRequest>();
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            using (var reader = _dbHelper.ExecuteReader("sp_GetUserPickupRequests", parameters))
            {
                while (reader.Read())
                {
                    requests.Add(new PickupRequest
                    {
                        RequestID = Convert.ToInt32(reader["RequestID"]),
                        BinID = Convert.ToInt32(reader["BinID"]),
                        Location = reader["Location"].ToString(),
                        RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                        RequestedPickupDate = Convert.ToDateTime(reader["RequestedPickupDate"]),
                        Status = reader["Status"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        AdminNotes = reader["AdminNotes"] == DBNull.Value ? null : reader["AdminNotes"].ToString()
                    });
                }
            }
            return requests;
        }

        public List<PickupRequest> GetAllPickupRequests()
        {
            var requests = new List<PickupRequest>();

            using (var reader = _dbHelper.ExecuteReader("sp_GetAllPickupRequests", null))
            {
                while (reader.Read())
                {
                    requests.Add(new PickupRequest
                    {
                        RequestID = Convert.ToInt32(reader["RequestID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        BinID = Convert.ToInt32(reader["BinID"]),
                        UserName = reader["Username"].ToString(),
                        Location = reader["Location"].ToString(),
                        RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                        RequestedPickupDate = Convert.ToDateTime(reader["RequestedPickupDate"]),
                        Status = reader["Status"].ToString(),
                        Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                    });
                }
            }
            return requests;
        }

        public bool UpdateRequestStatus(int requestId, string status, string adminNotes)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestId),
                new SqlParameter("@Status", status),
                new SqlParameter("@AdminNotes", adminNotes ?? (object)DBNull.Value)
            };

            var result = _dbHelper.ExecuteNonQuery("sp_UpdateRequestStatus", parameters);
            return result > 0;
        }
    }
}
