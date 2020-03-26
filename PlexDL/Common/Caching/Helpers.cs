using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;

namespace PlexDL.Common.Caching
{
    public static class Helpers
    {
        public static string CalculateMd5Hash(string input)
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
                if (!Directory.Exists(@"cache\" + CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb"))
                    Directory.CreateDirectory(@"cache\" + CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb");
                if (!Directory.Exists(@"cache\" + CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml"))
                    Directory.CreateDirectory(@"cache\" + CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml");
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
            }
        }
    }
}