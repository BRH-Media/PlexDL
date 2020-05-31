using System;

namespace LogDel
{
    public static class Vars
    {
        public static string LogDirectory { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl\\logs";
    }
}