using System.Security.Cryptography;
using System.Text;

namespace PlexDL.Common
{
    public static class MD5Helper
    {
        public static string CalculateMd5Hash(string input)
        {
            string value = "";
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            string rtrn = sb.ToString();

            if (!string.IsNullOrEmpty(rtrn))
                value = rtrn;

            return value;
        }
    }
}