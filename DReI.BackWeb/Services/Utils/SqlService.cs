using System;
using System.Data.SqlClient;
using System.Configuration;

namespace DReI.BackWeb.Services.Utils
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rafaelagovarEntities"].ConnectionString;
        }

        public T ExecuteScalar<T>(string query, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }
    }
}