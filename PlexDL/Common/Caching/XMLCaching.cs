using PlexDL.UI;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PlexDL.Common.Caching
{
    public static class XMLCaching
    {
        public static bool XMLInCache(string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableXMLCaching)
            {
                string fqPath = XMLCachePath(sourceUrl);
                return File.Exists(fqPath);
            }
            else
                return false;
        }

        public static string XMLCachePath(string sourceUrl)
        {
            string fileName = Helpers.CalculateMD5Hash(sourceUrl);
            string accountHash = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAccountToken);
            string serverHash = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAddress);
            string cachePath = @"cache\" + accountHash + @"\" + serverHash + @"\xml";
            string fqPath = cachePath + @"\" + fileName;
            return fqPath;
        }

        public static void XMLToCache(XmlDocument doc, string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableXMLCaching)
            {
                string fqPath = XMLCachePath(sourceUrl);
                doc.Save(fqPath);
            }
        }

        public static XmlDocument XMLFromCache(string sourceUrl)
        {
            if (Home.settings.CacheSettings.Mode.EnableXMLCaching)
            {
                string fqPath = XMLCachePath(sourceUrl);
                XmlDocument doc = new XmlDocument();
                doc.Load(fqPath);
                return doc;
            }
            else
                return new XmlDocument();
        }
    }
}