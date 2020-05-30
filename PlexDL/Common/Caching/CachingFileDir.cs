using PlexDL.Common.Globals;

namespace PlexDL.Common.Caching
{
    public static class CachingFileDir
    {
        private static readonly string AccountHash = MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
        private static readonly string ServerHash = MD5Helper.CalculateMd5Hash(GlobalStaticVars.Settings.ConnectionInfo.PlexAddress);

        public static string RootCacheDirectory { get; } = GlobalStaticVars.PlexDlAppData + @"\cache";
        public static string RootSessionDirectory { get; set; } = $"{RootCacheDirectory}\\{AccountHash}\\{ServerHash}";
        public static string XmlDirectory { get; } = RootSessionDirectory + @"\xml";
        public static string ThumbDirectory { get; } = RootSessionDirectory + @"\thumb";
        public static string ServerListDirectory { get; } = RootSessionDirectory;
    }
}