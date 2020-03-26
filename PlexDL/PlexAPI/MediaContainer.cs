using System;
using System.Collections.Generic;
using PlexDL.PlexAPI.DirectoryTypes;
using RestSharp;

namespace PlexDL.PlexAPI
{
    public class MediaContainer : PlexItem
    {
        public List<Directory> directories { get; set; }
        public List<Show> showDirectories { get; set; }

        public int size { get; set; }
        public bool allowSync { get; set; }
        public string identifier { get; set; }
        public int librarySectionID { get; set; }
        public string librarySectionUUID { get; set; }
        public string mediaTagPrefix { get; set; }
        public long mediaTagVersion { get; set; }
        public bool nocache { get; set; }
        public bool sortAsc { get; set; }
        public string title1 { get; set; }
        public string title2 { get; set; }
        public string viewGroup { get; set; }
        public int viewMode { get; set; }
        public string friendlyName { get; set; }
        public string machineIdentifier { get; set; }
        public string mediaAnalysisVersion { get; set; }
        public bool myPlex { get; set; }
        public string myPlexMappingState { get; set; }
        public string myPlexSigninState { get; set; }
        public string myPlexUsername { get; set; }
        public string platform { get; set; }
        public string platformVersion { get; set; }
        public bool requestParametersInCookie { get; set; }
        public bool sync { get; set; }
        public int transcroderActiveVideoSessions { get; set; }
        public bool transcoderAudio { get; set; }
        public bool transcoderVideo { get; set; }
        public string transcoderVideoBitrates { get; set; }
        public string transcoderVideoQualities { get; set; }
        public string transcoderVideoResolutions { get; set; }
        public string version { get; set; }
        public string content { get; set; }
        public bool mixedParents { get; set; }
        public int parentIndex { get; set; }
        public string parentTitle { get; set; }
        public int parentYear { get; set; }
        public string summary { get; set; }
        public string theme { get; set; }
        public string grandparentContentRating { get; set; }
        public string grandparentStudio { get; set; }
        public string grandparentTheme { get; set; }
        public string grandparentTitle { get; set; }

        public List<Video> videos { get; set; }
        public List<Track> tracks { get; set; }
        public List<Photo> photos { get; set; }

        public MediaContainer()
        {
        }

        public MediaContainer(User user, Server server, string uri) : base(user, server, uri)
        {
            Load();
        }

        public void Load()
        {
            var request = new RestRequest();
            request.Resource = uri;
            Console.WriteLine("Getting items from: " + uri);

            showDirectories = new List<Show>();

            var m = Execute<MediaContainer>(request, user);
            if (m.directories != null)
            {
                this.CopyFrom(m);

                // Loop through directories and add the user, server and uri
                for (var i = 0; i < directories.Count; i++)
                    ProcessDirectory(directories[i], i);
            }
        }

        protected void ProcessDirectory(Directory item, int i)
        {
            item.user = user;
            item.server = server;
            if (item.key.StartsWith("/"))
                item.uri = item.key;
            else
                item.uri = uri + "/" + item.key;

            switch (item.type)
            {
                case "show":
                    showDirectories.Add(new Show(item));
                    break;
            }

            directories[i] = item;
        }
    }
}