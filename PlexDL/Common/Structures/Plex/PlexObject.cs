using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.Plex
{
    [XmlInclude(typeof(PlexMovie))]
    [XmlInclude(typeof(PlexTVShow))]
    [XmlInclude(typeof(PlexMusic))]
    [Serializable]
    public class PlexObject
    {
        public DownloadInfo StreamInformation { get; set; } = new DownloadInfo();
        public int StreamIndex { get; set; } = 0;
        public Resolution StreamResolution { get; set; } = new Resolution();
        public string ContentGenre { get; set; } = "";
        public List<PlexActor> Actors { get; set; } = new List<PlexActor>();
        public string Synopsis { get; set; } = "";
    }
}