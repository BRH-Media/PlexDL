using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Caching
{
    public class CacheSettings
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Flags")]
        [Description("Contains flags for caching components that can be enabled/disabled")]
        public CacheMode Mode { get; set; } = new CacheMode();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Expiry")]
        [Description(
            "PlexDL can automatically download newer versions of cached files, if their creation date exceeds a fixed interval.")]
        public CacheExpiry Expiry { get; set; } = new CacheExpiry();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReadOnly(true)]
        [DisplayName("Data Paths")]
        [Description("Locations where cached data is stored. You cannot change these values.")]
        public CachePaths Paths { get; set; } = new CachePaths();

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}