using System.Drawing;
using System.IO;
using PlexDL.Common.Globals;

namespace PlexDL.Common.Caching.Handlers
{
    public static class ThumbCaching
    {
        public static string ThumbCachePath(string sourceUrl)
        {
            var fileName = Helpers.CalculateMd5Hash(sourceUrl);
            var accountHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);
            var cachePath = @"cache\" + accountHash + @"\" + serverHash + @"\thumb";
            var fqPath = cachePath + @"\" + fileName + CachingFileExt.ThumbExt;
            return fqPath;
        }

        public static bool ThumbInCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                return File.Exists(fqPath);
            }

            return false;
        }

        public static void ThumbToCache(Bitmap thumb, string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                thumb.Save(fqPath);
            }
        }

        public static Bitmap ThumbFromCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableThumbCaching)
            {
                var fqPath = ThumbCachePath(sourceUrl);
                return (Bitmap)Image.FromFile(fqPath);
            }

            return null;
        }
    }
}