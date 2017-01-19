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
            //Console.WriteLine("Writing Error".ToSqlCharacter());
            //Console.ReadLine();
            /*
                        var sql = new MsSqlServer();
                        var test = sql.SqlExecCommand("pTest", new Dictionary<string, object> { {"test1", 5}, {"test2", true} });
            */

            MutableStructHolder holder = new MutableStructHolder();
            // Affects the value of holder.Field
            holder.Field.SetValue(10);
            // Retrieves holder.Property as a copy and changes the copy
            holder.Property.SetValue(10);

            Console.WriteLine(holder.Field.Value);
            Console.WriteLine(holder.Property.Value);
        }
    }

    struct MutableStruct
    {
        public int Value { get; set; }

        public void SetValue(int newValue)
        {
            Value = newValue;
        }
    }

    class MutableStructHolder
    {
        public MutableStruct Field;
        public MutableStruct Property { get; set; }
    }
}