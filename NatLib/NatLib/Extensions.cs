using System.Collections.Generic;

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
    }
}