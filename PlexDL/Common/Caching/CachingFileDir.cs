using System;

namespace PlexDL.Common.Caching
{
    public static class CachingFileDir
    {
        private static string UserAppData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string RootCacheDirectory { get; set; } = UserAppData + @"\.plexdl\caching";
        public static string XmlDirectory { get; set; } = @"\xml";
        public static string ThumbDirectory { get; set; } = @"\thumb";
    }
}