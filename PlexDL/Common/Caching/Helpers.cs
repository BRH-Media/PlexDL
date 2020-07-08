using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.IO;
using PlexDL.Common.Globals.Providers;

namespace PlexDL.Common.Caching
{
    public static class Helpers
    {
        public static void CacheStructureBuilder()
        {
            try
            {
                if (!Directory.Exists(CachingFileDir.RootCacheDirectory + @"\" + MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) + @"\thumb"))
                    Directory.CreateDirectory(CachingFileDir.RootCacheDirectory + @"\" + MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) + @"\thumb");
                if (!Directory.Exists(CachingFileDir.RootCacheDirectory + @"\" + MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) + @"\xml"))
                    Directory.CreateDirectory(CachingFileDir.RootCacheDirectory + @"\" + MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                              MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) + @"\xml");
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
            }
        }
    }
}