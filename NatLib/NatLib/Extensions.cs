using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Hosting;


namespace NatLib
{
    public static class Extensions
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

        public static string SqlParamName(this string param)
        {
            return param.IndexOf('@') != -1 ? param : "@" + param;
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

        public static string ToSqlCharacter(this string value)  
        {
            var result = value?.Replace("'", "''");

            return result;
        }

        public static string ToCleanSql(this string sql)
        {
            if (sql == null) return null;

            var length = sql.IndexOf(';') == -1 ? sql.Length : sql.IndexOf(';');
            return sql.Substring(0, length);
        }

    }
}