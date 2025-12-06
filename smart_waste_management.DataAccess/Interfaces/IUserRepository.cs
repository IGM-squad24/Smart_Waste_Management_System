using smart_waste_management.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User AuthenticateUser(string username, string password);
        int RegisterUser(User user);
        User GetUserById(int userId);
        List<User> GetAllUsers();
        bool UpdateUser(User user);
    }
}
