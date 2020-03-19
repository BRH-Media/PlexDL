using PlexDL.Common.Logging;
using PlexDL.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PlexDL.Common.Caching
{
    public static class ServerCaching
    {
        public static string ServerCachePath(string accountToken)
        {
            var fileName = Helpers.CalculateMD5Hash(accountToken);
            var cachePath = @"cache\" + fileName;
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            var fqPath = cachePath + @"\" + fileName;
            return fqPath;
        }

        public static bool ServerInCache(string accountToken)
        {
            if (Home.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                var fqPath = ServerCachePath(accountToken);
                return File.Exists(fqPath);
            }
            else
            {
                return false;
            }
        }

        public static void ServerToCache(List<PlexAPI.Server> serverList, string accountToken)
        {
            try
            {
                if (Home.Settings.CacheSettings.Mode.EnableServerCaching)
                {
                    var fqPath = ServerCachePath(accountToken);
                    var serialiser = new XmlSerializer(typeof(List<PlexAPI.Server>));
                    TextWriter filestream = new StreamWriter(fqPath);
                    serialiser.Serialize(filestream, serverList);
                    filestream.Close();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ServerCacheWriteError");
                return;
            }
        }

        public static List<PlexAPI.Server> ServerFromCache(string accountToken)
        {
            if (Home.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                var fqPath = ServerCachePath(accountToken);
                var serialiser = new XmlSerializer(typeof(List<PlexAPI.Server>));
                TextReader filestream = new StreamReader(fqPath);
                var result = (List<PlexAPI.Server>)serialiser.Deserialize(filestream);
                filestream.Close();
                return result;
            }
            else
            {
                return new List<PlexAPI.Server>();
            }
        }
    }
}