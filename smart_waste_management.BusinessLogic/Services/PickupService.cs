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
    public class PickupService : IPickupService
    {
        private readonly IPickupRepository _pickupRepository;

        public PickupService()
        {
            _pickupRepository = new smart_waste_management.DataAccess.Repositories.PickupRepository();
        }

        public int CreatePickupRequest(PickupRequest request)
        {
            // Validation
            if (request.UserID <= 0)
                throw new ArgumentException("Valid User ID is required");

            if (request.BinID <= 0)
                throw new ArgumentException("Valid Bin ID is required");

            if (request.RequestedPickupDate < DateTime.Now)
                throw new ArgumentException("Pickup date cannot be in the past");

            return _pickupRepository.CreatePickupRequest(request);
        }

        public List<PickupRequest> GetUserPickupRequests(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Valid User ID is required");

            return _pickupRepository.GetUserPickupRequests(userId);
        }

        public List<PickupRequest> GetAllPickupRequests()
        {
            return _pickupRepository.GetAllPickupRequests();
        }

        public bool UpdateRequestStatus(int requestId, string status, string adminNotes)
        {
            if (requestId <= 0)
                throw new ArgumentException("Valid Request ID is required");

            var validStatuses = new List<string> { "Pending", "Approved", "In Progress", "Completed", "Rejected" };
            if (!validStatuses.Contains(status))
                throw new ArgumentException("Invalid status");

            return _pickupRepository.UpdateRequestStatus(requestId, status, adminNotes);
        }
    }
}
