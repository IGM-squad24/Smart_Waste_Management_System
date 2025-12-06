using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Interfaces
{
    public interface IPickupRepository
    {
        int CreatePickupRequest(PickupRequest request);
        List<PickupRequest> GetUserPickupRequests(int userId);
        List<PickupRequest> GetAllPickupRequests();
        bool UpdateRequestStatus(int requestId, string status, string adminNotes);
    }
}
