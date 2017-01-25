using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatLib.DB
{
    public static class Extension
    {
        public static string SqlParamName(this string param)
        {
            return param.IndexOf('@') != -1 ? param : "@" + param;
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
