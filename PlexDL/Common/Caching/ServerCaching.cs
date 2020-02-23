using PlexDL.UI;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PlexDL.Common.Caching
{
    public static class ServerCaching
    {
        public static string ServerCachePath(string accountToken)
        {
            string fileName = Helpers.CalculateMD5Hash(accountToken);
            string cachePath = @"cache\" + fileName;
            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);
            string fqPath = cachePath + @"\" + fileName;
            return fqPath;
        }

        public static bool ServerInCache(string accountToken)
        {
            if (Home.settings.CacheSettings.Mode.EnableServerCaching)
            {
                string fqPath = ServerCachePath(accountToken);
                return File.Exists(fqPath);
            }
            else
                return false;
        }

        public static void ServerToCache(List<PlexAPI.Server> serverList, string accountToken)
        {
            if (Home.settings.CacheSettings.Mode.EnableServerCaching)
            {
                string fqPath = ServerCachePath(accountToken);
                XmlSerializer serialiser = new XmlSerializer(typeof(List<PlexAPI.Server>));
                TextWriter filestream = new StreamWriter(fqPath);
                serialiser.Serialize(filestream, serverList);
                filestream.Close();
            }
        }

        public static List<PlexAPI.Server> ServerFromCache(string accountToken)
        {
            if (Home.settings.CacheSettings.Mode.EnableServerCaching)
            {
                string fqPath = ServerCachePath(accountToken);
                XmlSerializer serialiser = new XmlSerializer(typeof(List<PlexAPI.Server>));
                TextReader filestream = new StreamReader(fqPath);
                List<PlexAPI.Server> result = (List<PlexAPI.Server>)serialiser.Deserialize(filestream);
                filestream.Close();
                return result;
            }
            else
                return new List<PlexAPI.Server>();
        }
    }
}