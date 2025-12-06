using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_waste_management.DataAccess.Repositories
{
    public class DatabaseHelper
    {
        private readonly string connectionString= "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WasteManagementDB;Integrated Security=True;Encrypt=False;";

        

        // Execute Non-Query (Insert, Update, Delete)
        public int ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Execute Scalar (Return single value)
        public object ExecuteScalar(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        // Execute Reader (Return DataReader)
        public SqlDataReader ExecuteReader(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
