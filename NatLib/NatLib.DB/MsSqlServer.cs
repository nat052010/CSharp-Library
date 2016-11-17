using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NatLib.DB
{
    /// <summary>
    /// nat20161117 object for manipulating ms sql server database
    /// </summary>
    public class MsSqlServer : IDisposable
    {
        public SqlConnectionStringBuilder ConString { get; set; }

        public virtual SqlConnection Connection()
        {
            var con = new SqlConnection();
            Func<string, string> config = ConfigurationManager.AppSettings.Get;
            ConString = new SqlConnectionStringBuilder
            {
                DataSource = config("Server"),
                UserID = config("UserID"),
                Password = config("Password"),
                InitialCatalog = config("Database")
            };
            con.ConnectionString = ConString.ConnectionString;
            con.Open();
            return con;
        }

        public DataSet SqlExecCommand(string command)
        {
            var dataSet = new DataSet();
            using (var con = Connection())
            {
                var com = con.CreateCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = command;
                var adapter = new SqlDataAdapter { SelectCommand = com };
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public DataSet SqlExecCommand(string command, Dictionary<string, object> param)
        {
            var dataSet = new DataSet();
            using (var con = Connection())
            {
                var com = con.CreateCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = command;
                foreach (var item in param)
                    com.Parameters.Add(item.Key.SqlParamName(), item.Value);

                var adapter = new SqlDataAdapter { SelectCommand = com };
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public void SqlNoRetCommand(string command)
        {
            var dataTable = new DataTable();
            using (var con = Connection())
            {
                var comm = con.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = command;
                comm.ExecuteNonQuery();
            }
        }

        public object SqlScalarCommand(string command)
        {
            object result;
            using (var con = Connection())
            {
                var comm = con.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = command;
                result = comm.ExecuteScalar();
            }
            return result;
        }

        public virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
    }
}