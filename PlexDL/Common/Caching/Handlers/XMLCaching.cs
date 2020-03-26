using System.IO;
using System.Xml;
using PlexDL.Common.Globals;

namespace PlexDL.Common.Caching.Handlers
{
    public static class XMLCaching
    {
        public static string XMLCachePath(string sourceUrl)
        {
            var fileName = Helpers.CalculateMd5Hash(sourceUrl);
            var accountHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = Helpers.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);
            var cachePath = @"cache\" + accountHash + @"\" + serverHash + @"\xml";
            var fqPath = cachePath + @"\" + fileName + CachingFileExt.ApiXmlExt;
            return fqPath;
        }

        public static bool XMLInCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XMLCachePath(sourceUrl);
                return File.Exists(fqPath);
            }
            else
            {
                return false;
            }
        }

        public static void XMLToCache(XmlDocument doc, string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XMLCachePath(sourceUrl);
                doc.Save(fqPath);
            }
        }

        public static XmlDocument XMLFromCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XMLCachePath(sourceUrl);
                var doc = new XmlDocument();
                doc.Load(fqPath);
                return doc;
            }
            else
            {
                return new XmlDocument();
            }
        }
    }
}