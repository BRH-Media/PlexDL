using System;

namespace PlexDL.Common.Structures
{
    public class ConnectionInfo
    {
        public int PlexPort { get; set; } = 32400;
        public string PlexAddress { get; set; } = "";
        public string PlexAccountToken { get; set; } = "";

        [NonSerialized]
        public bool RelaysOnly = false;

        [NonSerialized]
        public bool DirectOnly = false;
    }
}