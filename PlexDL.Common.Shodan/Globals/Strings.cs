using System;

namespace PlexDL.Common.Shodan.Globals
{
    public static class Strings
    {
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ShodanApiKey { get; set; } = ApiKeyManager.StoredShodanApiKey();
        public static string ShodanApiKeyFileLocation { get; } = $@"{AppData}\.plexdl\.shodan";
    }
}