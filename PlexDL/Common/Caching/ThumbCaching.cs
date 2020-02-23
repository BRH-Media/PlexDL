using PlexDL.UI;
using System.Drawing;
using System.IO;

namespace PlexDL.Common.Caching
{
    public static class ThumbCaching
    {
        public static string ThumbCachePath(string sourceUrl)
        {
            string fileName = Helpers.CalculateMD5Hash(sourceUrl);
            string accountHash = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAccountToken);
            string serverHash = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAddress);
            string cachePath = @"cache\" + accountHash + @"\" + serverHash + @"\thumb";
            string fqPath = cachePath + @"\" + fileName;
            return fqPath;
        }

        public static bool ThumbInCache(string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableThumbCaching)
            {
                string fqPath = ThumbCachePath(sourceUrl);
                return File.Exists(fqPath);
            }
            else
                return false;
        }

        public static void ThumbToCache(Bitmap thumb, string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableThumbCaching)
            {
                string fqPath = ThumbCachePath(sourceUrl);
                thumb.Save(fqPath);
            }
        }

        public static Bitmap ThumbFromCache(string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableThumbCaching)
            {
                string fqPath = ThumbCachePath(sourceUrl);
                return (Bitmap)Bitmap.FromFile(fqPath);
            }
            else
                return null;
        }
    }
}