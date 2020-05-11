using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.IO;

namespace PlexDL.Common.Caching
{
    public static class Helpers
    {
        public static void CacheStructureBuilder()
        {
            try
            {
                if (!Directory.Exists(@"cache\" + MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb"))
                    Directory.CreateDirectory(@"cache\" + MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\thumb");
                if (!Directory.Exists(@"cache\" + MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml"))
                    Directory.CreateDirectory(@"cache\" + MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress) + @"\xml");
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
            }
        }
    }
}