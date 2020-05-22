using RestSharp;
using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class Directory : PlexItem
    {
        public Directory()
        {
        }

        public Directory(PlexItem item) : base(item)
        {
        }

        public Directory(User user, Server server, string uri) : base(user, server, uri)
        {
        }

        public bool secondary { get; set; }
        public bool search { get; set; }
        public string prompt { get; set; }
        public string agent { get; set; }
        public string scanner { get; set; }
        public string language { get; set; }
        public string uuid { get; set; }
        public bool refreshing { get; set; }
        public bool filters { get; set; }
        public int ratingKey { get; set; }
        public string studio { get; set; }
        public string contentRating { get; set; }
        public string summary { get; set; }
        public float rating { get; set; }
        public int year { get; set; }
        public string theme { get; set; }
        public long duration { get; set; }
        public string originallyAvailableAt { get; set; }
        public int leafCount { get; set; }
        public string viewedLeafCount { get; set; }
        public List<Genre> genres { get; set; }
        public List<Location> locations { get; set; }

        /*public Directory (Directory d)
		{
			Utils.CopyFrom<Directory, Directory>(this, d);
		}*/

        public List<T> GetChildren<T>() where T : PlexItem
        {
            var request = new RestRequest();
            request.Resource = uri;

            var items = Execute<List<T>>(request, user);
            for (var i = 0; i < items.Count; i++)
            {
                items[i].user = user;
                items[i].server = server;
                if (items[i].key.StartsWith("/"))
                    items[i].uri = items[i].key;
                else
                    items[i].uri = uri + "/" + items[i].key;
            }

            return items;
        }
    }
}