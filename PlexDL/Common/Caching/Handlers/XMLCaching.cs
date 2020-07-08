using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.IO;
using System.Xml;
using PlexDL.Common.Globals.Providers;

namespace PlexDL.Common.Caching.Handlers
{
    public static class XmlCaching
    {
        public static string XmlCachePath(string sourceUrl)
        {
            var accountHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress);
            var fileName = MD5Helper.CalculateMd5Hash(sourceUrl) + CachingFileExt.ApiXmlExt;
            var cachePath = $"{CachingFileDir.RootCacheDirectory}\\{accountHash}\\{serverHash}\\{CachingFileDir.XmlDirectory}";
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            var fqPath = $"{cachePath}\\{fileName}";
            return fqPath;
        }

        public static bool XmlInCache(string sourceUrl)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                var fqPath = XmlCachePath(sourceUrl);
                return File.Exists(fqPath);
            }

            return false;
        }

        public static bool XmlToCache(XmlDocument doc, string sourceUrl)
        {
            try
            {
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
                {
                    if (!XmlInCache(sourceUrl))
                    {
                        var fqPath = XmlCachePath(sourceUrl);
                        doc.Save(fqPath);
                        LoggingHelpers.RecordCacheEvent("Successfully cached URL", sourceUrl);
                    }
                    else
                        LoggingHelpers.RecordCacheEvent("URL is already cached", sourceUrl);
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XmlCacheWrtError");
                LoggingHelpers.RecordCacheEvent("Couldn't create cached file (an error occurred)", sourceUrl);
                return false;
            }
        }

        public static bool XmlReplaceCache(XmlDocument doc, string sourceUrl)
        {
            try
            {
                if (XmlRemoveCache(sourceUrl))
                {
                    if (XmlToCache(doc, sourceUrl))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XmlCacheRplError");
                LoggingHelpers.RecordCacheEvent("Couldn't replace existing cached file (an error occurred)", sourceUrl);
                //replacing didn't succeed
                return false;
            }
        }

        public static bool XmlRemoveCache(string sourceUrl)
        {
            try
            {
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
                    if (XmlInCache(sourceUrl))
                    {
                        File.Delete(XmlCachePath(sourceUrl));
                        LoggingHelpers.RecordCacheEvent("Removed URL from cache", sourceUrl);
                    }
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XmlCacheDelError");
                LoggingHelpers.RecordCacheEvent("Couldn't remove existing cached file (an error occurred)", sourceUrl);
                //deletion didn't succeed
                return false;
            }
        }

        public static XmlDocument XmlFromCache(string sourceUrl)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
            {
                if (XmlInCache(sourceUrl))
                {
                    var fqPath = XmlCachePath(sourceUrl);
                    if (ObjectProvider.Settings.CacheSettings.Expiry.Enabled)
                    {
                        if (!CachingExpiryHelpers.CheckCacheExpiry(fqPath, ObjectProvider.Settings.CacheSettings.Expiry.Interval))
                        {
                            LoggingHelpers.RecordCacheEvent("Cached URL is not expired; loading from cached copy.", sourceUrl);
                            var doc = new XmlDocument();
                            doc.Load(fqPath);
                            return doc;
                        }
                        else
                        {
                            LoggingHelpers.RecordCacheEvent("Cached URL is out-of-date; attempting to get a new copy.", sourceUrl);
                            var doc = API.XmlGet.GetXmlTransaction(sourceUrl, "", true);
                            XmlReplaceCache(doc, sourceUrl);
                            return doc;
                        }
                    }
                    else
                    {
                        LoggingHelpers.RecordCacheEvent("Loading from cached copy", sourceUrl);
                        var doc = new XmlDocument();
                        doc.Load(fqPath);
                        return doc;
                    }
                }
                else
                    LoggingHelpers.RecordCacheEvent("URL isn't cached; couldn't load from specified file.", sourceUrl);
            }

            return new XmlDocument();
        }
    }
}