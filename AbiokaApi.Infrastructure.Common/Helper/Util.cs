using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace AbiokaApi.Infrastructure.Common.Helper
{
    public static class Util
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string GetHashText(string text) {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static T EnumParse<T>(this string value) {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException($"{typeof(T).Name} is not an enum");

            var result = (T)Enum.Parse(typeof(T), value);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list == null || list.Count() == 0;

        public static bool IsNotNullAndEmpty<T>(this IEnumerable<T> list) => list != null && list.Count() > 0;

        public static bool IsNullOrEmpty(this Guid guid) => guid == null || guid == Guid.Empty;

        public static bool IsNotNullAndEmpty(this Guid guid) => guid != null && guid != Guid.Empty;

        public static string EncodeWithBase64(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        public static string DecodeBase64(this string encodedString) => Encoding.UTF8.GetString(Convert.FromBase64String(encodedString));
    }
}
