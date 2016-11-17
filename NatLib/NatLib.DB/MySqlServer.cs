﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace NatLib.DB
{
    /// <summary>
    /// NAT20161117 object for manipulating mysql server database
    /// </summary>
    public class MySqlServer
    {
        public MySqlConnectionStringBuilder ConString { get; set; }

        public virtual MySqlConnection Connection()
        {
            var con = new MySqlConnection();
            Func<string, string> config = ConfigurationManager.AppSettings.Get;
            ConString = new MySqlConnectionStringBuilder()
            {
                Server = config("Server"),
                UserID = config("UserID"),
                Password = config("Password"),
                Database = config("Database"),
                Port = Convert.ToUInt32(config("Port"))
            };
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
                var adapter = new MySqlDataAdapter { SelectCommand = com };
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

                var adapter = new MySqlDataAdapter { SelectCommand = com };
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