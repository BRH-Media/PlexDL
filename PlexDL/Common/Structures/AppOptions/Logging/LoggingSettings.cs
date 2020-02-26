namespace PlexDL.Common.Structures.AppOptions.Logging
{
    public class LoggingSettings
    {
        public bool EnableXMLTransactionLogDel { get; set; } = true;
        public bool EnableExceptionLogDel { get; set; } = true;
        public bool EnableGenericLogDel { get; set; } = true;
    }
}