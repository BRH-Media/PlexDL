using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.PlexAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Directory = System.IO.Directory;

namespace PlexDL.Common.Caching.Handlers
{
    public static class ServerCaching
    {
        public static string ServerCachePath(string accountToken)
        {
            var accountHash = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            var fileName = accountHash + CachingFileExt.ServerListExt;
            var cachePath = $"{CachingFileDir.RootCacheDirectory}\\{accountHash}";
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            var fqPath = $"{cachePath}\\{fileName}";
            return fqPath;
        }

        public static bool ServerInCache(string accountToken)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                var fqPath = ServerCachePath(accountToken);
                return File.Exists(fqPath);
            }

            return false;
        }

        public static void ServerToCache(List<Server> serverList, string accountToken)
        {
            try
            {
                if (ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
                {
                    var fqPath = ServerCachePath(accountToken);
                    var serialiser = new XmlSerializer(typeof(List<Server>));
                    var filestream = new StreamWriter(fqPath);
                    serialiser.Serialize(filestream, serverList);
                    filestream.Close();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ServerCacheWriteError");
            }
        }

        public static List<Server> ServerFromCache(string accountToken)
        {
            if (ObjectProvider.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                var fqPath = ServerCachePath(accountToken);
                var serialiser = new XmlSerializer(typeof(List<Server>));
                var filestream = new StreamReader(fqPath);
                var result = (List<Server>)serialiser.Deserialize(filestream);
                filestream.Close();
                return result;
            }

            return new List<Server>();
        }
    }
}