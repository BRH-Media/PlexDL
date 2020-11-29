using PlexDL.Common.Logging;
using System;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace PlexDL.Common.Caching
{
    public class CachingMetrics
    {
        public int SERVER_LISTS { get; set; }
        public long SERVER_LISTS_SIZE { get; set; }
        public int API_DOCUMENTS { get; set; }
        public long API_DOCUMENTS_SIZE { get; set; }
        public int THUMBNAIL_IMAGES { get; set; }
        public long THUMBNAIL_IMAGES_SIZE { get; set; }
        public int TOTAL_CACHED { get; set; }
        public long TOTAL_CACHED_SIZE { get; set; }

        //format the byte number of the total size to a human-readable string
        public string TotalCacheSize() => Methods.FormatBytes(TOTAL_CACHED_SIZE);

        //format the byte number of all image sizes to a human-readable string
        public string ThumbSize() => Methods.FormatBytes(THUMBNAIL_IMAGES_SIZE);

        //format the byte number of all server list sizes to a human-readable string
        public string ServerListsSize() => Methods.FormatBytes(SERVER_LISTS_SIZE);

        //format the byte number of all XML sizes to a human-readable string
        public string ApiXmlSize() => Methods.FormatBytes(API_DOCUMENTS_SIZE);

        /// <summary>
        /// Constructs a CachingMetrics object from the directory itself
        /// </summary>
        /// <returns></returns>
        public static CachingMetrics FromLatest()
        {
            //get all cached files in arrays
            var servers = ServerLists();
            var apiXml = ApiXml();
            var thumbs = Thumbs();
            var total = AllCached();

            //construct the metrics object and return the result
            return new CachingMetrics
            {
                //number of servers
                SERVER_LISTS = servers.Length,

                //size of all server lists
                SERVER_LISTS_SIZE = SizeOfFiles(servers),

                //number of XML files
                API_DOCUMENTS = apiXml.Length,

                //size of all XML files
                API_DOCUMENTS_SIZE = SizeOfFiles(apiXml),

                //number of images
                THUMBNAIL_IMAGES = thumbs.Length,

                //size of all images
                THUMBNAIL_IMAGES_SIZE = SizeOfFiles(thumbs),

                //number of cached files in total
                TOTAL_CACHED = total.Length,

                //size of all cached files in total
                TOTAL_CACHED_SIZE = SizeOfFiles(total)
            };
        }

        /// <summary>
        /// Calculates the total byte-size of all files in the provided array
        /// </summary>
        /// <param name="files">Array of cached file paths</param>
        /// <returns></returns>
        public static long SizeOfFiles(string[] files)
        {
            //total size of all files in the array
            long size = 0;

            try
            {
                //for each file in the array, grab its size and add it to the counter
                size +=
                    (from f in files where File.Exists(f) select new FileInfo(f) into fi select fi.Length).Sum();
            }
            catch (Exception ex)
            {
                //log it and then continue as normal
                LoggingHelpers.RecordException(ex.Message, "CacheSizeCalcError");
            }

            //return the final size
            return size;
        }

        public static string[] AllCached()
        {
            //array of all cached files
            string[] files = { };

            //check if the caching directory exists
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))

                //it does, so use the directory indexer to list all cached files
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, "*", SearchOption.AllDirectories);

            //return the cached files array
            return files;
        }

        public static string[] ServerLists()
        {
            //array of all cached server lists
            string[] files = { };

            //check if the caching directory exists
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))

                //it does, so use the directory indexer to list all cached server lists
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, $"*{CachingFileExt.ServerListExt}",
                    SearchOption.AllDirectories);

            //return the cached files array
            return files;
        }

        public static string[] Thumbs()
        {
            //array of all cached images
            string[] files = { };

            //check if the caching directory exists
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))

                //it does, so use the directory indexer to list all cached images
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, $"*{CachingFileExt.ThumbExt}",
                    SearchOption.AllDirectories);

            //return the cached files array
            return files;
        }

        public static string[] ApiXml()
        {
            //array of all cached XML files
            string[] files = { };

            //check if the caching directory exists
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))

                //it does, so use the directory indexer to list all cached XML files
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, $"*{CachingFileExt.ApiXmlExt}",
                    SearchOption.AllDirectories);

            //return the cached files array
            return files;
        }
    }
}