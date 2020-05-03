using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
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
                if (!XmlInCache(sourceUrl))
                {
                    var fqPath = XmlCachePath(sourceUrl);
                    doc.Save(fqPath);
                    LoggingHelpers.RecordCacheEvent("[Caching] Successfully cached URL", sourceUrl);
                }
                else
                    LoggingHelpers.RecordCacheEvent("[Caching] URL is already cached", sourceUrl);
            }
        }

        public static XmlDocument XmlFromCache(string sourceUrl)
        {
            if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                if (XmlInCache(sourceUrl))
                {
                    var fqPath = XmlCachePath(sourceUrl);
                    if (GlobalStaticVars.Settings.CacheSettings.Expiry.Enabled)
                    {
                        if (!CachingExpiryHelpers.CheckCacheExpiry(fqPath, GlobalStaticVars.Settings.CacheSettings.Expiry.Interval))
                        {
                            LoggingHelpers.RecordCacheEvent("[Caching] Cached URL is not expired; loading from cached copy.", sourceUrl);
                            var doc = new XmlDocument();
                            doc.Load(fqPath);
                            return doc;
                        }
                        else
                        {
                            LoggingHelpers.RecordCacheEvent("[Caching] Cached URL is out-of-date; attempting to get a new copy.", sourceUrl);
                            return API.XmlGet.GetXmlTransaction(sourceUrl, "", true);
                        }
                    }
                    else
                    {
                        LoggingHelpers.RecordCacheEvent("[Caching] Loading from cached copy", sourceUrl);
                        var doc = new XmlDocument();
                        doc.Load(fqPath);
                        return doc;
                    }
                }
                else
                    LoggingHelpers.RecordCacheEvent("[Caching] URL isn't cached; couldn't load from specified file.", sourceUrl);
            }

            return new XmlDocument();
        }
    }
}