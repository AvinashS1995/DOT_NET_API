using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace AIS_MyWebApi.Common
{
    public abstract class BaseRepository
    {

        public DbContext _dbContext { get; set; }
        public readonly ILogger<BaseRepository> _logger;

        protected BaseRepository(DbContext dbContext, ILogger<BaseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public MySqlCommand CreateCommand()
        {
            var cmd = _dbContext._mySqlConnection.CreateCommand();
            return cmd;
        }

        public void AddParameter(MySqlCommand command, string parameterName, object value)
        {
            command.Parameters.AddWithValue("@" + parameterName, value);
        }

        public async Task<DataTable> ExecuteDataTableAsync(MySqlCommand command)
        {
            DataTable dataTable = new DataTable();

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }
            finally
            {
                _dbContext.ConnectionClosed();
            }
            return dataTable;
        }

        public List<T> ConvertToLists<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            var collection = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    try
                    {
                        if (columnNames.Contains("c" + pro.Name.ToLower()))
                        {
                            pro.SetValue(objT, row["c" + pro.Name.ToLower()]);
                        }
                        else if (columnNames.Contains("n" + pro.Name.ToLower()))
                        {
                            var value = row["n" + pro.Name.ToLower()] == DBNull.Value ?
                                        Convert.ChangeType(0, pro.PropertyType) :
                                        Convert.ChangeType(row["n" + pro.Name.ToLower()], pro.PropertyType);
                            pro.SetValue(objT, value);
                        }
                        else if (columnNames.Contains("b" + pro.Name.ToLower()))
                        {
                            if (pro.PropertyType == typeof(bool))
                            {
                                pro.SetValue(objT, Convert.ToInt16(row["b" + pro.Name.ToLower()]) == 1);
                            }
                            else
                            {
                                pro.SetValue(objT, row["b" + pro.Name.ToLower()].ToString());
                            }
                        }
                        else if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            pro.SetValue(objT, row[pro.Name.ToLower()]);
                        }
                    }
                    catch (Exception ex)
                    { 
                        _logger.LogError(ex.Message, $"Error setting property {pro.Name}");
                    }
                }
                collection.Add(objT);
            }
            return collection;
        }
    }
}