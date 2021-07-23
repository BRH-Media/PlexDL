using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Security.Hashing;
using System.Drawing;
using System.IO;

namespace PlexDL.Common.Caching.Handlers
{
    public static class ThumbCaching
    {
        public static string ThumbCachePath(string sourceUrl)
        {
            var accountHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = MD5Helper.CalculateMd5Hash($"{ObjectProvider.Settings.ConnectionInfo.PlexAddress}" +
                                                        $":{ObjectProvider.Settings.ConnectionInfo.PlexPort}");
            var fileName = MD5Helper.CalculateMd5Hash(sourceUrl) + CachingFileExt.ThumbExt;
            var cachePath =
                $"{CachingFileDir.RootCacheDirectory}\\{accountHash}\\{serverHash}\\{CachingFileDir.ThumbRelativeDirectory}";
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            var fqPath = $"{cachePath}\\{fileName}";
            return fqPath;
        }

        public static bool ThumbInCache(string sourceUrl)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                return File.Exists(fqPath);
            }

            return false;
        }

        public static void ThumbToCache(Bitmap thumb, string sourceUrl)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                thumb.Save(fqPath);
            }
        }

        public static Bitmap ThumbFromCache(string sourceUrl)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                return (Bitmap)Image.FromFile(fqPath);
            }

            return null;
        }
    }
}