using PlexDL.Common.Globals;
using System.Drawing;
using System.IO;

namespace PlexDL.Common.Caching.Handlers
{
    public static class ThumbCaching
    {
        public static string ThumbCachePath(string sourceUrl)
        {
            var fileName = MD5Helper.CalculateMd5Hash(sourceUrl);
            var cachePath = CachingFileDir.ThumbDirectory;
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
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