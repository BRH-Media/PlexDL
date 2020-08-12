using System;
using LogDel.Enums;

namespace LogDel
{
    public static class Vars
    {
        public static LogSecurity Protected { get; set; } = LogSecurity.Protected;
        public static string LogDirectory { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl\\logs";
    }
}