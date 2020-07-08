using System.Collections.Generic;
using System.Windows.Forms;
using libbrhscgui.Components;
using PlexDL.AltoHTTP.Classes;
using PlexDL.Common.Enums;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;

namespace PlexDL.Common.Globals.Providers
{
    public static class ObjectProvider
    {
        public static Form DebugForm { get; set; } = null;
        public static DownloadQueue Engine { get; set; } = new DownloadQueue();
        public static List<DownloadInfo> Queue { get; set; }
        public static ApplicationOptions Settings { get; set; } = new ApplicationOptions();
        public static MyPlex PlexProvider { get; set; } = new MyPlex();
        public static UserInteraction LibUi { get; set; } = new UserInteraction();
        public static User User { get; set; } = new User();
        public static Server Svr { get; set; }
        public static PlexObject CurrentStream { get; set; }
        public static ContentType CurrentContentType { get; set; }
        public static List<Server> PlexServers { get; set; }
    }
}