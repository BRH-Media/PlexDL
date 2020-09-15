using System;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.Plex
{
    [Serializable]
    public class PlexTvShow : PlexObject
    {
        public string TvShowName { get; set; } = "";
        public string Season { get; set; } = "";
        public int EpisodeNumber { get; set; } = 0;
        public int SeasonNumber { get; set; } = 0;
        public int EpisodesInSeason { get; set; } = 0;

        [XmlIgnore]
        public string Notation
        {
            get
            {
                const int len = 2;
                var s = SeasonNumber.ToString().PadLeft(len, '0');
                var e = EpisodeNumber.ToString().PadLeft(len, '0');
                return $"S{s}E{e}";
            }
        }

        [XmlIgnore]
        public string TitleNotation
        {
            get
            {
                var n = Notation;
                var t = !string.IsNullOrEmpty(StreamInformation.ContentTitle) ? StreamInformation.ContentTitle : @"";
                var s = TvShowName;
                return $"{s} - {n} - {t}";
            }
        }
    }
}