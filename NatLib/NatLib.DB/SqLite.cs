using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace NatLib.DB
{
    public class SqLite
    {
        public SQLiteConnectionStringBuilder ConString { get; set; }

        public SQLiteConnection Connection(SQLiteConnectionStringBuilder conString = null)
        {
            var con = new SQLiteConnection();
            Func<string, string> config = ConfigurationManager.AppSettings.Get;

            if (conString == null)
                ConString = new SQLiteConnectionStringBuilder
                {
                    DataSource = config("SQLiteDataSource"),
                    Version = Convert.ToInt32(config("SQLiteVersion"))
                };
            else
                ConString = conString;

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
                var adapter = new SQLiteDataAdapter() { SelectCommand = com };
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
                    com.Parameters.Add(item.Key.SqlParamName(), (DbType) item.Value);

                var adapter = new SQLiteDataAdapter() { SelectCommand = com };
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

    }
}
