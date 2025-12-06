using smart_waste_management.DataAccess.Interfaces;
using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Repositories
{
    public class BinRepository : IBinRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public BinRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public List<Bin> GetAllBins()
        {
            var bins = new List<Bin>();

            using (var reader = _dbHelper.ExecuteReader("sp_GetAllBins", null))
            {
                while (reader.Read())
                {
                    bins.Add(new Bin
                    {
                        BinID = Convert.ToInt32(reader["BinID"]),
                        Location = reader["Location"].ToString(),
                        Capacity = Convert.ToDecimal(reader["Capacity"]),
                        CurrentLevel = Convert.ToDecimal(reader["CurrentLevel"]),
                        Status = reader["Status"].ToString(),
                        LastEmptied = reader["LastEmptied"] == DBNull.Value ? null : (DateTime?)reader["LastEmptied"],
                        NextScheduledEmpty = reader["NextScheduledEmpty"] == DBNull.Value ? null : (DateTime?)reader["NextScheduledEmpty"]
                    });
                }
            }
            return bins;
        }

        public Bin GetBinById(int binId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBinStatus(int binId, decimal currentLevel, string status)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@BinID", binId),
                new SqlParameter("@CurrentLevel", currentLevel),
                new SqlParameter("@Status", status)
            };

            var result = _dbHelper.ExecuteNonQuery("sp_UpdateBinStatus", parameters);
            return result > 0;
        }

        public List<Bin> GetBinsByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}

