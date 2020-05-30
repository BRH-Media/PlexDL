using PlexDL.Common.Globals;

namespace PlexDL.Common.Caching
{
    public static class CachingFileDir
    {
        public static string RootCacheDirectory { get; } = GlobalStaticVars.PlexDlAppData + @"\cache";
        public static string XmlDirectory { get; } = @"\xml";
        public static string ThumbDirectory { get; } = @"\thumb";
    }
}