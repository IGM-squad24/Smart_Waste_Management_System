using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Interfaces
{
    public interface IBinRepository
    {
        List<Bin> GetAllBins();
        Bin GetBinById(int binId);
        bool UpdateBinStatus(int binId, decimal currentLevel, string status);
        List<Bin> GetBinsByStatus(string status);
    }
}
