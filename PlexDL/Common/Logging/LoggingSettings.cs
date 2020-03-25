using System.ComponentModel;

namespace PlexDL.Common.Logging
{
    public class LoggingSettings
    {
        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }

        [DisplayName("Transaction Logging")]
        [Description("If this is enabled, PlexDL will log every XML API request that is sent.")]
        public bool EnableXMLTransactionLogDel { get; set; } = true;

        [DisplayName("Exception Logging")]
        [Description("If this is enabled, PlexDL will attempt to log every implemented exception handler's response.")]
        public bool EnableExceptionLogDel { get; set; } = true;

        [DisplayName("Generic Logging")]
        [Description("If this is enabled, PlexDL will log everything to a file in addition to the \"Log\" section.")]
        public bool EnableGenericLogDel { get; set; } = true;
    }
}