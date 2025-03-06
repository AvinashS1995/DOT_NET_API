using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace AIS_MyWebApi.Common
{
    public class DbContext : IDisposable
    {
        public readonly MySqlConnection _mySqlConnection;

        public DbContext(string connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
            _mySqlConnection.Open();
        }

        public void ConnectionClosed()
        {
            if(_mySqlConnection.State != ConnectionState.Closed)
            _mySqlConnection.Close();
        }
        public void Dispose()
        {
            if(_mySqlConnection.State != ConnectionState.Closed)
            _mySqlConnection.Close();
        }
    }
}