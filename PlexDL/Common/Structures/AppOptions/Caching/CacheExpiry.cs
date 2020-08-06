using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Caching
{
    [Serializable]
    public class CacheExpiry
    {
        [DisplayName("Interval")]
        [Description("How long (in days) from the creation date should cached data expire?")]
        public int Interval { get; set; } = 5; //5-day cache expiry

        [DisplayName("Expiry Enabled")]
        [Description("Is cache expiry enabled?")]
        public bool Enabled { get; set; } =
            true; //cache will expire (be replaced) if the Interval is reached or exceeded

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}