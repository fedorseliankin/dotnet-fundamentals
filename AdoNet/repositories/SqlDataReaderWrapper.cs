using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.repositories
{
    public interface IDataReaderWrapper : IDisposable
    {
        bool Read();
        int GetInt32(int i);
        string GetString(int i);

        public decimal GetDecimal(int i);
    }

    public class SqlDataReaderWrapper : IDataReaderWrapper
    {
        private readonly SqlDataReader _reader;

        public SqlDataReaderWrapper(SqlDataReader reader)
        {
            _reader = reader;
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        public string GetString(int i)
        {
            return _reader.GetString(i);
        }

        public decimal GetDecimal(int i)
        {
            return _reader.GetDecimal(i);
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
