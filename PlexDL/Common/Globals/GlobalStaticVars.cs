using libbrhscgui.Components;
using PlexDL.AltoHTTP.Classes;
using PlexDL.Common.Enums;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlexDL.Common.Globals
{
    public static class GlobalStaticVars
    {
        public static Form DebugForm { get; set; } = null;
        public static DownloadQueue Engine { get; set; } = new DownloadQueue();
        public static List<DownloadInfo> Queue { get; set; }
        public static ApplicationOptions Settings { get; set; } = new ApplicationOptions();
        public static MyPlex Plex { get; set; } = new MyPlex();
        public static UserInteraction LibUi { get; set; } = new UserInteraction();
        public static User User { get; set; } = new User();
        public static Server Svr { get; set; }
        public static PlexObject CurrentStream { get; set; }
        public static ContentType CurrentContentType { get; set; }
        public static List<Server> PlexServers { get; set; }
        public static string CurrentApiUri { get; set; } = "";
        public static string RepoUrl { get; } = "https://github.com/brhsoftco/plexdl";
        public static int SessionIdLength { get; } = 10;
        public static string CurrentSessionId { get; } = Methods.GenerateRandomNumber(SessionIdLength);

        public static string UserAppData { get; set; } =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string PlexDlAppData { get; set; } = $@"{UserAppData}\.plexdl";

        public static string GetToken()
        {
            return Svr != null ? Svr.accessToken : "";
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