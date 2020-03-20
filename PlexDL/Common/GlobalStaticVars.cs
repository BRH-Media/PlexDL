using libbrhscgui.Components;
using MetroSet_UI;
using PlexDL.AltoHTTP.Classes;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using PlexDL.PlexAPI;
using System.Collections.Generic;

namespace PlexDL.Common
{
    public static class GlobalStaticVars
    {
        public static StyleManager GlobalStyle { get; set; } = new StyleManager()
        {
            CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml",
            Style = MetroSet_UI.Design.Style.Light,
            ThemeAuthor = null,
            ThemeName = null
        };

        public static DownloadQueue Engine { get; set; }
        public static List<DownloadInfo> Queue { get; set; }
        public static ApplicationOptions Settings { get; set; } = new ApplicationOptions();
        public static MyPlex Plex { get; set; } = new MyPlex();

        public static UserInteraction LibUI { get; set; } = new UserInteraction();
        public static User User { get; set; } = new User();
        public static Server Svr { get; set; }
        public static PlexObject CurrentStream { get; set; }
        public static List<Server> PlexServers { get; set; }
    };
}