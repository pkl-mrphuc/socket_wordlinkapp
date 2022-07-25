using System;
using System.Data;
using System.Data.SqlClient;

namespace WordLinkApp.DL
{
    public class BaseDL
    {
        public SqlConnection _sqlConnection;
        public string _connectionString;
        public BaseDL(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
