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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new smart_waste_management.DataAccess.Repositories.UserRepository();
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            return _userRepository.AuthenticateUser(username, password);
        }

        public int Register(User user)
        {
            // Basic validation
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                throw new System.Exception("Username and password are required");

            if (string.IsNullOrEmpty(user.Email))
                throw new System.Exception("Email is required");

            return _userRepository.RegisterUser(user);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }
    }
}