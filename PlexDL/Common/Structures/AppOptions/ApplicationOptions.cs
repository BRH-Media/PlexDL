using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions.Caching;
using PlexDL.Common.Structures.AppOptions.Display;
using PlexDL.Common.Structures.AppOptions.Player;

namespace PlexDL.Common.Structures.AppOptions
{
    public class ApplicationOptions
    {
        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();
        public LoggingSettings Logging { get; set; } = new LoggingSettings();
        public GridViewDisplays DataDisplay { get; set; } = new GridViewDisplays();
        public PlayerSettings Player { get; set; } = new PlayerSettings();
        public GenericAppSettings Generic { get; set; } = new GenericAppSettings();
        public CachingSettings CacheSettings { get; set; } = new CachingSettings();
    }
}