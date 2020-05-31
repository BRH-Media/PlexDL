using System;

namespace PlexDL.Common.Caching
{
    public static class CachingFileDir
    {
        public static string RootCacheDirectory { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl\\caching";
        public static string XmlDirectory { get; } = @"\xml";
        public static string ThumbDirectory { get; } = @"\thumb";
    }
}