using System.Collections.Generic;
using libbrhscgui.Components;
using PlexDL.AltoHTTP.Classes;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;

namespace PlexDL.Common.Globals
{
    public static class GlobalStaticVars
    {
        public static DownloadQueue Engine { get; set; }
        public static List<DownloadInfo> Queue { get; set; }
        public static ApplicationOptions Settings { get; set; } = new ApplicationOptions();
        public static MyPlex Plex { get; set; } = new MyPlex();
        public static UserInteraction LibUI { get; set; } = new UserInteraction();
        public static User User { get; set; } = new User();
        public static Server Svr { get; set; }
        public static PlexObject CurrentStream { get; set; }
        public static List<Server> PlexServers { get; set; }
        public static string CurrentApiUri { get; set; } = "";

        public static string GetToken()
        {
            return Svr.accessToken;
        }

        public static string GetBaseUri(bool incToken)
        {
            if (incToken)
                return "http://" + Settings.ConnectionInfo.PlexAddress + ":" + Settings.ConnectionInfo.PlexPort +
                       "/?X-Plex-Token=";
            return "http://" + Settings.ConnectionInfo.PlexAddress + ":" + Settings.ConnectionInfo.PlexPort + "/";
        }
    }
}