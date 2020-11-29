using System.Security.Cryptography;
using System.Text;

// ReSharper disable InconsistentNaming

namespace PlexDL.Common.Security.Hashing
{
    /// <summary>
    /// Provides methods for handling MD5 hashes
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// Hashes a string and converts it to a hex string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMd5Hash(string input)
        {
            //convert the string to its byte representation
            var inputBytes = Encoding.ASCII.GetBytes(input);

            //hash the string's bytes
            var hash = CalculateMd5Hash(inputBytes);

            //convert byte array to hex string
            var final = Md5ToHex(hash);

            //return the result only if the hex string is valid;
            //otherwise, simply return an empty string
            return !string.IsNullOrEmpty(final)
                ? final
                : @"";
        }

        /// <summary>
        /// Hashes a byte array and converts it to a hex string
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public static byte[] CalculateMd5Hash(byte[] inputBytes)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(inputBytes);

            return hash;
        }

        /// <summary>
        /// Converts a hashed byte array to a hex string
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string Md5ToHex(byte[] hash)
        {
            //stores the relevant strings for hex conversion
            var sb = new StringBuilder();

            //for each byte in the hash, append it as hex
            foreach (var t in hash)
                sb.Append(t.ToString("X2"));

            //return the final hex string
            return sb.ToString();
        }
    }
}