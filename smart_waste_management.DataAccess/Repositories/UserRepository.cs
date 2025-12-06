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
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public UserRepository()
        {
            _dbHelper = new DatabaseHelper();
        }

        public User AuthenticateUser(string username, string password)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            using (var reader = _dbHelper.ExecuteReader("sp_AuthenticateUser", parameters))
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Role = reader["Role"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
            return null;
        }

        public int RegisterUser(User user)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@FullName", user.FullName)
            };

            var result = _dbHelper.ExecuteScalar("sp_RegisterUser", parameters);
            return Convert.ToInt32(result);
        }

        public User GetUserById(int userId)
        {
            // Implementation for getting user by ID
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            // Implementation for getting all users
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            // Implementation for updating user
            throw new NotImplementedException();
        }
    }
}
