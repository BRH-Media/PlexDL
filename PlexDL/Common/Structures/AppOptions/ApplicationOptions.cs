using System.ComponentModel;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions.Caching;
using PlexDL.Common.Structures.AppOptions.Display;
using PlexDL.Common.Structures.AppOptions.Player;
using PlexDL.Common.TypeConverters;

namespace PlexDL.Common.Structures.AppOptions
{
    public class ApplicationOptions
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Connection Settings")]
        [Description("View Plex connection settings. You can't change these values.")]
        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LoggingSettings Logging { get; set; } = new LoggingSettings();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public GridViewDisplays DataDisplay { get; set; } = new GridViewDisplays();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Streaming Settings")]
        [Description("Settings related to PVS, VLC and streaming content in general.")]
        public PlayerSettings Player { get; set; } = new PlayerSettings();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Generic Application Settings")]
        [Description("Misc. settings relating to normal functioning")]
        public GenericAppSettings Generic { get; set; } = new GenericAppSettings();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Caching Settings")]
        [Description("Settings related to how PlexDL deals with cached data")]
        public CachingSettings CacheSettings { get; set; } = new CachingSettings();
    }
}