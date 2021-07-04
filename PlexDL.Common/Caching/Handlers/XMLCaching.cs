using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Net;
using PlexDL.Common.Security.Hashing;
using System;
using System.IO;
using System.Xml;

// ReSharper disable RedundantIfElseBlock

namespace PlexDL.Common.Caching.Handlers
{
    public static class XmlCaching
    {
        public static string XmlCachePath(string sourceUrl)
        {
            var accountHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            var serverHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress);
            var fileName = MD5Helper.CalculateMd5Hash(sourceUrl) + CachingFileExt.ApiXmlExt;
            var cachePath =
                $"{CachingFileDir.RootCacheDirectory}\\{accountHash}\\{serverHash}\\{CachingFileDir.XmlRelativeDirectory}";
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
                //ensure we're allowed to perform caching operations on XML files
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
                {
                    //ensure the URL requested is not already cached
                    if (!XmlInCache(sourceUrl))
                    {
                        //export the new cache file
                        var fqPath = XmlCachePath(sourceUrl);
                        doc.Save(fqPath);

                        //log status
                        LoggingHelpers.RecordCacheEvent("Successfully cached URL", sourceUrl);
                    }
                    else
                    {
                        //log status
                        LoggingHelpers.RecordCacheEvent("URL is already cached", sourceUrl);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "XmlCacheWrtError");
                LoggingHelpers.RecordCacheEvent("Couldn't create cached file (an error occurred)", sourceUrl);
            }

            //default
            return false;
        }

        public static bool XmlReplaceCache(XmlDocument doc, string sourceUrl)
        {
            try
            {
                //remove and then create a new cached XML file based on the supplied params
                //return the boolean result of both operations
                var deletionResult = XmlRemoveCache(sourceUrl);
                var replaceResult = XmlToCache(doc, sourceUrl);

                //final result
                return deletionResult && replaceResult;
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "XmlCacheRplError");
                LoggingHelpers.RecordCacheEvent("Couldn't replace existing cached file (an error occurred)", sourceUrl);
            }

            //default
            return false;
        }

        public static bool XmlRemoveCache(string sourceUrl)
        {
            try
            {
                //ensure we're allowed to perform caching operations on XML files
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)

                    //ensure the URL requested is already cached
                    if (XmlInCache(sourceUrl))
                    {
                        //delete the cached XML file
                        File.Delete(XmlCachePath(sourceUrl));

                        //make sure it no longer exists
                        if (!XmlInCache(sourceUrl))
                        {
                            //log event
                            LoggingHelpers.RecordCacheEvent("Removed URL from cache", sourceUrl);

                            //deletion succeeded
                            return true;
                        }
                        else

                            //log failure
                            LoggingHelpers.RecordCacheEvent("Cache deletion failed", sourceUrl);
                    }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "XmlCacheDelError");
                LoggingHelpers.RecordCacheEvent("Couldn't remove existing cached file (an error occurred)", sourceUrl);
            }

            //default
            return false;
        }

        public static XmlDocument XmlFromCache(string sourceUrl)
        {
            try
            {
                //ensure we're allowed to perform caching operations on XML files//ensure we're allowed to perform caching operations on XML files
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableXmlCaching)
                {
                    //ensure the URL requested is already cached
                    if (XmlInCache(sourceUrl))
                    {
                        //get the encoded cache path for this XML record
                        var fqPath = XmlCachePath(sourceUrl);

                        //check if expiring caches are enabled
                        if (ObjectProvider.Settings.CacheSettings.Expiry.Enabled)
                        {
                            //has this record expired?
                            if (!CachingExpiry.CheckCacheExpiry(fqPath,
                                ObjectProvider.Settings.CacheSettings.Expiry.Interval))
                            {
                                //log status
                                LoggingHelpers.RecordCacheEvent("Cached URL is not expired; loading from cached copy.",
                                    sourceUrl);

                                //create a new XML record and load from the cached copy
                                var doc = new XmlDocument();
                                doc.Load(fqPath);

                                //return cached copy
                                return doc;
                            }
                            else
                            {
                                //log status
                                LoggingHelpers.RecordCacheEvent(
                                    "Cached URL is out-of-date; attempting to get a new copy.",
                                    sourceUrl);

                                //force the download of a new copy
                                var newDoc = XmlGet.GetXmlTransaction(sourceUrl, true, false, false);

                                //replace the existing record with the new one
                                var replaceResult = XmlReplaceCache(newDoc, sourceUrl);

                                //logging procedure
                                LoggingHelpers.RecordCacheEvent(
                                    replaceResult ? "Cache replacement succeeded" : "Cache replacement failed",
                                    sourceUrl);

                                //return the newly-downloaded record
                                return newDoc;
                            }
                        }
                        else
                        {
                            //log status
                            LoggingHelpers.RecordCacheEvent("Loading from cached copy", sourceUrl);

                            //create a new XML record and load from the cached copy
                            var doc = new XmlDocument();
                            doc.Load(fqPath);

                            //return cached copy
                            return doc;
                        }
                    }
                    else

                        //log status
                        LoggingHelpers.RecordCacheEvent("URL isn't cached; couldn't load from specified file.",
                            sourceUrl);
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "XmlCacheGetError");
                LoggingHelpers.RecordCacheEvent("Couldn't retrieve existing cached file (an error occurred)", sourceUrl);
            }

            //default
            return new XmlDocument();
        }
    }
}