using PlexDL.Common.Logging;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class KeyGatherers
    {
        public static string GetLibraryKey(System.Xml.XmlDocument doc)
        {
            string key = "";

            using (XmlReader reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag
                        switch (reader.Name.ToString())
                        {
                            case "Directory":
                                if (reader.GetAttribute("title") == "library")
                                {
                                    string localKey = reader.GetAttribute("key");
                                    key = localKey;
                                }
                                break;
                        }
                    }
                }
                return key;
            }
        }

        public static string GetSectionKey(System.Xml.XmlDocument doc)
        {
            string key = "";

            LoggingHelpers.AddToLog("Parsing XML Reply");
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        LoggingHelpers.AddToLog("Checking for directories");

                        switch (reader.Name.ToString())
                        {
                            case "Directory":
                                if (reader.GetAttribute("title") == "Library Sections")
                                {
                                    string localKey = reader.GetAttribute("key");
                                    key = localKey;
                                    LoggingHelpers.AddToLog("Found " + key);
                                }
                                break;
                        }
                    }
                }
                return key;
            }
        }
    }
}