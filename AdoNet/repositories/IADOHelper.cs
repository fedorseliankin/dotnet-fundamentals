using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.repositories
{
    public interface IADOHelper
    {
        void ExecuteCommand(string query, List<SqlParameter> parameters);

        public T ExecuteSingleReader<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> convert);
        
        public List<T> ExecuteReader<T>(string query, List<SqlParameter> parameters, Func<SqlDataReader, T> convert);

        public void ExecuteTransaction(string procedureName, List<SqlParameter> parameters);

        public IDataReaderWrapper ExecuteReaderWithConnection(string command, List<SqlParameter> parameters);
    }
}
