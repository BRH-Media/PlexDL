using System;

namespace PlexDL.Common.Structures.Plex
{
    [Serializable]
    public class PlexMusic : PlexObject
    {
        public string Artist { get; set; } = @"Unknown";
        public string Album { get; set; } = @"Unknown";
        public string SongTitle { get; set; } = @"Unknown";
    }
}