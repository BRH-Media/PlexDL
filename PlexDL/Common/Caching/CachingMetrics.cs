using PlexDL.Common.Logging;
using System;
using System.IO;

namespace PlexDL.Common.Caching
{
    public class CachingMetrics
    {
        public int SERVER_LISTS { get; set; } = 0;
        public long SERVER_LISTS_SIZE { get; set; } = 0;
        public int API_DOCUMENTS { get; set; } = 0;
        public long API_DOCUMENTS_SIZE { get; set; } = 0;
        public int THUMBNAIL_IMAGES { get; set; } = 0;
        public long THUMBNAIL_IMAGES_SIZE { get; set; } = 0;
        public int TOTAL_CACHED { get; set; } = 0;
        public long TOTAL_CACHED_SIZE { get; set; } = 0;

        public string TotalCacheSize()
        {
            return Methods.FormatBytes(TOTAL_CACHED_SIZE);
        }

        public string ThumbSize()
        {
            return Methods.FormatBytes(THUMBNAIL_IMAGES_SIZE);
        }

        public string ServerListsSize()
        {
            return Methods.FormatBytes(SERVER_LISTS_SIZE);
        }

        public string ApiXmlSize()
        {
            return Methods.FormatBytes(API_DOCUMENTS_SIZE);
        }

        public static CachingMetrics FromLatest()
        {
            string[] servers = ServerLists();
            string[] apixml = ApiXml();
            string[] thumbs = Thumbs();
            string[] total = AllCached();
            return new CachingMetrics()
            {
                SERVER_LISTS = servers.Length,
                SERVER_LISTS_SIZE = SizeOfFiles(servers),
                API_DOCUMENTS = apixml.Length,
                API_DOCUMENTS_SIZE = SizeOfFiles(apixml),
                THUMBNAIL_IMAGES = thumbs.Length,
                THUMBNAIL_IMAGES_SIZE = SizeOfFiles(thumbs),
                TOTAL_CACHED = total.Length,
                TOTAL_CACHED_SIZE = SizeOfFiles(total)
            };
        }

        public static long SizeOfFiles(string[] files)
        {
            long size = 0;
            try
            {
                foreach (string f in files)
                {
                    if (File.Exists(f))
                    {
                        FileInfo fi = new FileInfo(f);
                        size += fi.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                //log it and then continue as normal
                LoggingHelpers.RecordException(ex.Message, "CacheSizeCalcError");
            }
            return size;
        }

        public static string[] AllCached()
        {
            string[] files = new string[] { };
            if (Directory.Exists(@"cache"))
            {
                files = Directory.GetFiles(@"cache", "*", SearchOption.AllDirectories);
                if (files == null)
                    files = new string[] { };
            }
            return files;
        }

        public static string[] ServerLists()
        {
            string[] files = new string[] { };
            if (Directory.Exists(@"cache"))
            {
                files = Directory.GetFiles(@"cache", "*" + CachingFileExt.ServerListExt, SearchOption.AllDirectories);
                if (files == null)
                    files = new string[] { };
            }
            return files;
        }

        public static string[] Thumbs()
        {
            string[] files = new string[] { };
            if (Directory.Exists(@"cache"))
            {
                files = Directory.GetFiles(@"cache", "*" + CachingFileExt.ThumbExt, SearchOption.AllDirectories);
                if (files == null)
                    files = new string[] { };
            }
            return files;
        }

        public static string[] ApiXml()
        {
            string[] files = new string[] { };
            if (Directory.Exists(@"cache"))
            {
                files = Directory.GetFiles(@"cache", "*" + CachingFileExt.ApiXmlExt, SearchOption.AllDirectories);
                if (files == null)
                    files = new string[] { };
            }
            return files;
        }

        public static int NumberServerLists()
        {
            int servers = 0;
            if (Directory.Exists(@"cache"))
            {
                servers = ServerLists().Length;
            }
            return servers;
        }

        public static int Total()
        {
            int total = 0;
            if (Directory.Exists(@"cache"))
            {
                total = AllCached().Length;
            }
            return total;
        }

        public static int NumberApiXml()
        {
            int xml = 0;
            if (Directory.Exists(@"cache"))
            {
                xml = ApiXml().Length;
            }
            return xml;
        }

        public static int NumberThumbs()
        {
            int thumbs = 0;
            if (Directory.Exists(@"cache"))
            {
                thumbs = Thumbs().Length;
            }
            return thumbs;
        }
    }
}