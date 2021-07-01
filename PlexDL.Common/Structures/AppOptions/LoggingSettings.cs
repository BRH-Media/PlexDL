using LogDel;
using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions
{
    [Serializable]
    public class LoggingSettings
    {
        [DisplayName("Transaction Logging")]
        [Description("If this is enabled, PlexDL will log every XML API request that is sent.")]
        public bool EnableXmlTransactionLogDel { get; set; } = true;

        [DisplayName("Exception Logging")]
        [Description("If this is enabled, PlexDL will attempt to log every implemented exception handler's response.")]
        public bool EnableExceptionLogDel { get; set; } = true;

        [DisplayName("Generic Logging")]
        [Description("If this is enabled, PlexDL will log every generic event to a file.")]
        public bool EnableGenericLogDel { get; set; } = true;

        [DisplayName("Cache Logging")]
        [Description("If this is enabled, PlexDL will log every cache event to a file.")]
        public bool EnableCacheLogDel { get; set; } = true;

        [DisplayName("Base64 Encoding")]
        [Description("If this is enabled, PlexDL will encode all entries in Base64 to preserve formatting.")]
        public bool EnableBase64Encoding
        {
            get => LogDelGlobals.LogBase64Enabled;
            set => LogDelGlobals.LogBase64Enabled = value;
        }

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}