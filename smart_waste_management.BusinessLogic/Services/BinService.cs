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
    public class BinService : IBinService
    {
        private readonly IBinRepository _binRepository;

        public BinService()
        {
            _binRepository = new smart_waste_management.DataAccess.Repositories.BinRepository();
        }

        public List<Bin> GetAllBins()
        {
            return _binRepository.GetAllBins();
        }

        public Bin GetBinById(int binId)
        {
            return _binRepository.GetBinById(binId);
        }

        public bool UpdateBinStatus(int binId, decimal currentLevel, string status)
        {
            // Validate status
            var validStatuses = new List<string> { "Empty", "Low", "Medium", "Full", "Critical" };
            if (!validStatuses.Contains(status))
                throw new System.Exception("Invalid bin status");

            // Validate current level
            if (currentLevel < 0 || currentLevel > 100)
                throw new System.Exception("Current level must be between 0 and 100");

            return _binRepository.UpdateBinStatus(binId, currentLevel, status);
        }

        public List<Bin> GetBinsByStatus(string status)
        {
            return _binRepository.GetBinsByStatus(status);
        }
    }
}
