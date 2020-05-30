using System;

namespace PlexDL.Common.Structures.Plex
{
    [Serializable]
    public class PlexTVShow : PlexObject
    {
        public string TvShowName { get; set; } = "";
        public string Season { get; set; } = "";
        public int EpisodeNumber { get; set; } = 0;
        public int EpisodesInSeason { get; set; } = 0;
    }
}