using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.Plex
{
    [XmlInclude(typeof(PlexMovie))]
    [XmlInclude(typeof(PlexTVShow))]
    [Serializable]
    public class PlexObject
    {
        public DownloadInfo StreamInformation { get; set; } = new DownloadInfo();
        public int StreamIndex { get; set; } = 0;
        public Resolution StreamResolution { get; set; } = new Resolution();
        public string StreamPosterUri { get; set; } = "";
        public string ContentGenre { get; set; } = "";
        public long ContentDuration { get; set; } = 0;
        public List<PlexActor> Actors { get; set; } = new List<PlexActor>();
    }
}