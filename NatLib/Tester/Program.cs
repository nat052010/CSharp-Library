using System;
using System.Collections.Generic;
using NatLib;
using NatLib.DB;
using NatLib.Debug;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //"Error Sample".Log();
            Console.WriteLine("Writing Error".ToSqlCharacter());
            Console.ReadLine();
            /*
                        var sql = new MsSqlServer();
                        var test = sql.SqlExecCommand("pTest", new Dictionary<string, object> { {"test1", 5}, {"test2", true} });
            */
        }
    }
}