using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;


namespace NatLib
{
    public static class Extension
    {
        public static string ToLetter(this int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        public static string Fuse<T>(this IEnumerable<T> list, string delimeter = ", ")
        {
            return string.Join(delimeter, list);
        }

        public static void Log(this string error)
        {
            var location = HostingEnvironment.IsHosted ? HttpContext.Current.Server.MapPath("~/Error") : Path.Combine(Directory.GetCurrentDirectory(), "Error");

            if (!Directory.Exists(location))
                Directory.CreateDirectory(location);

            var fileName = "Err_" + DateTime.Today.ToShortDateString().Replace("/", "-") + ".txt";
            var path = Path.Combine(location, fileName);

            using (var file = new StreamWriter(path, true))
            {
                file.WriteLine(DateTime.Now.ToShortTimeString() + ", " + error);
            }
        }

        public static List<object> Items(this DataRowCollection dRows)
        {
            var drow = (from DataRow item in dRows select item.ItemArray.ToList()).Cast<object>().ToList();
            return drow;
        }

        public static List<object> Items(this DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            var items = new List<object>();

            foreach (DataRow item in dt.Rows)
            {
                var value = new Dictionary<string, object>();
                foreach (var column in columnNames)
                {
                    value.Add(column, item[column]);
                }
                items.Add(value);
            }

            return items;
        }

        public static List<List<object>> Items(this DataTableCollection dts)
        {
            var items = new List<List<object>>();
            foreach (DataTable dt in dts)
            {
                items.Add(dt.Items());
            }

            return items;
        }


    }
}