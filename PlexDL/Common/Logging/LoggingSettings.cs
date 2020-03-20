using System.ComponentModel;

namespace PlexDL.Common.Logging
{
    public class LoggingSettings
    {
        public bool EnableXMLTransactionLogDel { get; set; } = true;
        public bool EnableExceptionLogDel { get; set; } = true;
        public bool EnableGenericLogDel { get; set; } = true;
    }
}