using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Security.Hashing;
using System;
using System.IO;

namespace PlexDL.Common.Caching
{
    /// <summary>
    /// Contains miscellaneous methods to assist with the operation of the caching system
    /// </summary>
    public static class CachingHelpers
    {
        /// <summary>
        /// This method ensures the directory structure required is created and ready for use
        /// </summary>
        public static void CacheStructureBuilder()
        {
            //root caching directory for the current user
            var rootUserDir = $@"{CachingFileDir.RootCacheDirectory}\" +
                           $@"{MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken)}\" +
                           $@"{MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress)}";

            //root directory where all images are stored
            var thumbDir = $@"{rootUserDir}\{CachingFileDir.ThumbRelativeDirectory}";

            //root directory where all XML files are stored
            var xmlDir = $@"{rootUserDir}\{CachingFileDir.XmlRelativeDirectory}";

            try
            {
                //ensure the images directory has been created
                if (!Directory.Exists(thumbDir))
                    Directory.CreateDirectory(thumbDir);

                //ensure the XML directory has been created
                if (!Directory.Exists(xmlDir))
                    Directory.CreateDirectory(xmlDir);
            }
            catch (Exception ex)
            {
                //log the error and exit
                LoggingHelpers.RecordException(ex.Message, "CacheDirBuildError");
            }
        }
    }
}