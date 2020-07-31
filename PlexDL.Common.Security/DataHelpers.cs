using System;
using System.Text;

namespace PlexDL.Common
{
    public static class DataHelpers
    {
        public static byte[] StringToBytes(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public static string BytesToString(byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }

        public static string StringToBase64(string input)
        {
            return Convert.ToBase64String(StringToBytes(input));
        }

        public static string StringFromBase64(string input)
        {
            return BytesToString(Convert.FromBase64String(input));
        }

        public static string BytesToBase64(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public static byte[] BytesFromBase64(string input)
        {
            return Convert.FromBase64String(input);
        }
    }
}