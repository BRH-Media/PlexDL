using LogDel.Enums;
using System;

namespace LogDel
{
    public static class LogDelGlobals
    {
        public static LogSecurity Protected { get; set; } = LogSecurity.Unprotected;

        /// <summary>
        /// The directory where log files are read from/written to
        /// </summary>
        public static string LogDirectory { get; set; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.plexdl\logs";

        /// <summary>
        /// The entry prefix to denote a base64-encoded log entry
        /// </summary>
        public static string LogBase64Prefix { get; } = @"b64:";

        /// <summary>
        /// Whether or not to encode entries in Base64
        /// </summary>
        public static bool LogBase64Enabled { get; set; } = true;
    }
}