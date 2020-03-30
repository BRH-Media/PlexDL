using PlexDL.Common.Globals;
using System.IO;
using System.Xml;

namespace PlexDL.Common.Caching.Handlers
{
    public static class XmlCaching
    {
        public static string XmlCachePath(string sourceUrl)
        {
            var fileName = Helpers.CalculateMd5Hash(sourceUrl);
            var accountHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);
            var cachePath = @"cache\" + accountHash + @"\" + serverHash + @"\xml";
            var fqPath = cachePath + @"\" + fileName + CachingFileExt.ApiXmlExt;
            return fqPath;
        }

        public static bool XmlInCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XmlCachePath(sourceUrl);
                return File.Exists(fqPath);
            }

            return false;
        }

        public static void XmlToCache(XmlDocument doc, string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XmlCachePath(sourceUrl);
                doc.Save(fqPath);
            }
        }

        public static XmlDocument XmlFromCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XmlCachePath(sourceUrl);
                var doc = new XmlDocument();
                doc.Load(fqPath);
                return doc;
            }

            return new XmlDocument();
        }
    }
}