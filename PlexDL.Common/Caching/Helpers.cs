using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Security;
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
                if (!Directory.Exists(CachingFileDir.RootCacheDirectory + @"\" +
                                      Md5Helper.CalculateMd5Hash(
                                          ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) +
                                      @"\thumb"))
                    Directory.CreateDirectory(CachingFileDir.RootCacheDirectory + @"\" +
                                              Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo
                                                  .PlexAccountToken) + @"\" +
                                              Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo
                                                  .PlexAddress) + @"\thumb");
                if (!Directory.Exists(CachingFileDir.RootCacheDirectory + @"\" +
                                      Md5Helper.CalculateMd5Hash(
                                          ObjectProvider.Settings.ConnectionInfo.PlexAccountToken) + @"\" +
                                      Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress) +
                                      @"\xml"))
                    Directory.CreateDirectory(CachingFileDir.RootCacheDirectory + @"\" +
                                              Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo
                                                  .PlexAccountToken) + @"\" +
                                              Md5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo
                                                  .PlexAddress) + @"\xml");
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
            }
        }
    }
}