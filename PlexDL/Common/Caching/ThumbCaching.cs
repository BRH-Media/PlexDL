using PlexDL.Common.Globals;
using System.Drawing;
using System.IO;

namespace PlexDL.Common.Caching
{
    public static class ThumbCaching
    {
        public static string ThumbCachePath(string sourceUrl)
        {
            var fileName = Helpers.CalculateMD5Hash(sourceUrl);
            var accountHash = Helpers.CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = Helpers.CalculateMD5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);
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
            else
            {
                return false;
            }
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
            else
            {
                return null;
            }
        }
    }
}