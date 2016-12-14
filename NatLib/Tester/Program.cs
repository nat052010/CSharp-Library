using System;
using System.Collections.Generic;
using NatLib.DB;
using NatLib.Debug;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var log = new Log();
            log.Write("Error1");
            log.Write("Error2");
            Console.WriteLine("Writing Error");
            Console.ReadLine();
            /*
                        var sql = new MsSqlServer();
                        var test = sql.SqlExecCommand("pTest", new Dictionary<string, object> { {"test1", 5}, {"test2", true} });
            */
        }
    }
}