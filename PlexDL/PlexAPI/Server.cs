using System;
using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    [Serializable]
    public class Server : PlexRest
    {
        public Server()
        {
        }

        public string accessToken { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int port { get; set; }
        public string version { get; set; }
        public string host { get; set; }
        public string localAddresses { get; set; }
        public string machineIdentifier { get; set; }
        public bool owned { get; set; }
        public User user { get; set; }

        public string createdAt
        {
            get { return createDate.ToString(); }
            set { createDate = Utils.GetDateTimeFromTimestamp(value); }
        }

        public string updatedAt
        {
            get { return lastUpdated.ToString(); }
            set { lastUpdated = Utils.GetDateTimeFromTimestamp(value); }
        }

        public DateTime createDate { get; set; }
        public DateTime lastUpdated { get; set; }

        protected override string GetBaseUrl()
        {
            return "http://" + address + ":" + port;
        }

        public List<Directory> GetLibrarySections()
        {
            MediaContainer m = new MediaContainer(user, this, "/library/sections");
            return m.directories;
        }

        public List<Directory> Test()
        {
            Directory d = new Directory(user, this, "/library/sections");
            List<Directory> dl = d.GetChildren<Directory>();
            return dl;
        }
    }
}