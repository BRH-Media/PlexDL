using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures
{
    [Serializable]
    public class ConnectionInfo
    {
        [NonSerialized] public bool RelaysOnly = false;

        [DisplayName("Port")]
        [Description("The port of the Plex server")]
        [ReadOnly(true)]
        public int PlexPort { get; set; } = 32400;

        [DisplayName("Address")]
        [Description("The IP address/hostname of the Plex server")]
        [ReadOnly(true)]
        public string PlexAddress { get; set; } = "0.0.0.0";

        [DisplayName("Token")]
        [Description("Your unique 20-character Plex account token")]
        [ReadOnly(true)]
        public string PlexAccountToken { get; set; } = "";

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}