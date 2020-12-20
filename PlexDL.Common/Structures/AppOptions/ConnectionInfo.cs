using PlexDL.AltoHTTP.Common.Net;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures.AppOptions
{
    [Serializable]
    public class ConnectionInfo
    {
        [NonSerialized] [XmlIgnore] public bool RelaysOnly = false;

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

        [DisplayName("Global Request Timeout")]
        [Description(
            "This is the HTTP request timeout (in seconds). It applies across all PlexDL network services; " +
            "including updates, API requests and downloads. " +
            "A value of 0 denotes no timeout.")]
        [ReadOnly(true)]
        public int GlobalRequestTimeout
        {
            get => NetGlobals.Timeout;
            set => NetGlobals.Timeout = value;
        }

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}