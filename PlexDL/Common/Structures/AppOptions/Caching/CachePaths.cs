using PlexDL.Common.Caching;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Caching
{
    public class CachePaths
    {
        [ReadOnly(true)]
        [DisplayName("Server Lists")]
        [Description("Location where PlexDL will store the hashed server list (if Server Caching is enabled) for each token")]
        public string ServerCachePath { get; set; } = $"{CachingFileDir.RootCacheDirectory}\\%TOKEN%";

        [ReadOnly(true)]
        [DisplayName("Images")]
        [Description("Location will PlexDL will store hashed image files (if Image Caching is enabled)")]
        public string ThumbCachePath { get; set; } = $"{CachingFileDir.RootCacheDirectory}\\%TOKEN%\\%SERVER%\thumb";

        [ReadOnly(true)]
        [DisplayName("API Files")]
        [Description("Location where PlexDL will store hashed API files (if API Caching is enabled)")]
        public string XmlCachePath { get; set; } = $"{CachingFileDir.RootCacheDirectory}\\%TOKEN%\\%SERVER%\\xml";

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}