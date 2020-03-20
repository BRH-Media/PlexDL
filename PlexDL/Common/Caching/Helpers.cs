using PlexDL.Common.Logging;
using PlexDL.UI;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PlexDL.Common.Caching
{
    public static class Helpers
    {
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

        public static void CacheStructureBuilder()
        {
            try
            {
                if (!System.IO.Directory.Exists(@"cache\" + CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                                CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb"))
                    System.IO.Directory.CreateDirectory(@"cache\" + CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                                        CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb");
                if (!System.IO.Directory.Exists(@"cache\" + CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                                CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml"))
                    System.IO.Directory.CreateDirectory(@"cache\" + CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                                        CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml");
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
                return;
            }
        }
    }
}