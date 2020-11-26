using PlexDL.Common.Security;

namespace PlexDL.PlexAPI.LoginHandler.Globals
{
    public static class PlexDefinitions
    {
        public static string ClientId { get; set; } = GuidHandler.GetGlobalGuid().ToString();
        public static string Version { get; set; } = @"Plex OAuth";
        public static string Model { get; set; } = @"Plex OAuth";
        public static string Product { get; set; } = @"PlexDL (Windows)";
    }
}