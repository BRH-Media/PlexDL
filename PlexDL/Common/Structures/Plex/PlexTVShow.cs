using System;

namespace PlexDL.Common.Structures.Plex
{
    [Serializable]
    public class PlexTVShow : PlexObject
    {
        public string TVShowName { get; set; } = "";
        public string Season { get; set; } = "";
        public string Episode { get; set; } = "";
        public int EpisodesInSeason { get; set; } = 0;
    }
}