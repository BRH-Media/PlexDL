using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using System;
using System.IO;
using System.Xml;

namespace PlexDL.Common.Caching.Handlers
{
    public static class XmlCaching
    {
        public static string XmlCachePath(string sourceUrl)
        {
            var accountHash = MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);
            var fileName = MD5Helper.CalculateMd5Hash(sourceUrl) + CachingFileExt.ApiXmlExt;
            var cachePath = $"{CachingFileDir.RootCacheDirectory}\\{accountHash}\\{serverHash}\\{CachingFileDir.XmlDirectory}";
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            var fqPath = $"{cachePath}\\{fileName}";
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

        public static bool XmlToCache(XmlDocument doc, string sourceUrl)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XmlCacheWrtError");
                LoggingHelpers.RecordCacheEvent("[Caching] Couldn't create cached file (an error occurred)", sourceUrl);
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
                LoggingHelpers.RecordCacheEvent("[Caching] Couldn't replace existing cached file (an error occurred)", sourceUrl);
                //replacing didn't succeed
                return false;
            }
        }

        public static bool XmlRemoveCache(string sourceUrl)
        {
            try
            {
                if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableXmlCaching)
                    if (XmlInCache(sourceUrl))
                    {
                        File.Delete(XmlCachePath(sourceUrl));
                        LoggingHelpers.RecordCacheEvent("[Caching] Removed URL from cache", sourceUrl);
                    }
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "XmlCacheDelError");
                LoggingHelpers.RecordCacheEvent("[Caching] Couldn't remove existing cached file (an error occurred)", sourceUrl);
                //deletion didn't succeed
                return false;
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
                            var doc = API.XmlGet.GetXmlTransaction(sourceUrl, "", true);
                            XmlReplaceCache(doc, sourceUrl);
                            return doc;
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