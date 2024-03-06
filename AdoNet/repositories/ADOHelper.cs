using System.Data;
using System.Data.SqlClient;

namespace AdoNet.repositories
{
    public class ADOHelper : IADOHelper
    {
        private string ConnectionString;

        public ADOHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void ExecuteCommand(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        public T ExecuteSingleReader<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> convert)
        {
            T result = default;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result = convert(reader);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<T> ExecuteReader<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> convert)
        {
            List<T> results = new List<T>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(convert(reader));
                        }
                    }
                }
            }
            return results;
        }

        public void ExecuteTransaction(string procedureName, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(procedureName, connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddRange(parameters.ToArray());
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
