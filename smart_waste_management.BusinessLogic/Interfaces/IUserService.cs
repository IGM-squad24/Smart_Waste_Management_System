using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        int Register(User user);
        User GetUserById(int userId);
        bool UpdateUser(User user);
    }
}
