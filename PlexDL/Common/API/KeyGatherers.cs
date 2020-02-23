using PlexDL.Common.Logging;
using System.Xml;

namespace PlexDL.Common.API
{
    public class KeyGatherers
    {
        public static string getLibraryKey(System.Xml.XmlDocument doc)
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

        public static string getSectionKey(System.Xml.XmlDocument doc)
        {
            string key = "";

            LoggingHelpers.addToLog("Parsing XML Reply");
            using (XmlReader reader = new XmlNodeReader(doc))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        LoggingHelpers.addToLog("Checking for directories");

                        switch (reader.Name.ToString())
                        {
                            case "Directory":
                                if (reader.GetAttribute("title") == "Library Sections")
                                {
                                    string localKey = reader.GetAttribute("key");
                                    key = localKey;
                                    LoggingHelpers.addToLog("Found " + key);
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