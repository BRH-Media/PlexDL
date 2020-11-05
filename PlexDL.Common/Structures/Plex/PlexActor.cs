using System.Drawing;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.Plex
{
    public class PlexActor
    {
        public string ThumbnailUri { get; set; } = "";

        [XmlIgnore]
        public Image Thumbnail { get; set; } = null;

        public string ActorName { get; set; } = "";
        public string ActorRole { get; set; } = "";
    }
}