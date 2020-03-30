using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Caching
{
    public class CacheMode
    {
        [DisplayName("Server Caching")]
        [Description(
            "Instead of grabbing servers from Plex.tv every time, PlexDL can cache server lists for you (this does not apply to relays). You can disable this if your servers frequently change locations.")]
        public bool EnableServerCaching { get; set; } = true;

        [DisplayName("Image Caching")]
        [Description(
            "Instead of grabbing images from various servers every time (which can be very performance intensive), PlexDL can cache these images for you.")]
        public bool EnableThumbCaching { get; set; } = true;

        [DisplayName("API Caching")]
        [Description(
            "API Caching enables PlexDL to cache all API requests to a PMS server. This means content lists, metadata, library sections and other important information doesn't have to be downloaded again.")]
        public bool EnableXmlCaching { get; set; } = true;

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}