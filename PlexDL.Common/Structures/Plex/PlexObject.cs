﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.Plex
{
    [XmlInclude(typeof(PlexMovie))]
    [XmlInclude(typeof(PlexTvShow))]
    [XmlInclude(typeof(PlexMusic))]
    [Serializable]
    public class PlexObject
    {
        [XmlIgnore]
        public XmlDocument RawMetadata { get; set; }

        public StreamInfo StreamInformation { get; set; } = new StreamInfo();
        public string ApiUri { get; set; } = @""; //e.g. /library/metadata/<UNIQUE_NUMERIC_ID>
        public int StreamIndex { get; set; } = 0;
        public Resolution StreamResolution { get; set; } = new Resolution();
        public string ContentGenre { get; set; } = "";
        public List<PlexActor> Actors { get; set; } = new List<PlexActor>();
        public string Synopsis { get; set; } = "";
    }
}