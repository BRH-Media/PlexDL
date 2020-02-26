namespace PlexDL.Common.Structures.AppOptions
{
    public class GenericAppSettings
    {
        public int DefaultStringLength { get; set; } = 64;
        public bool ShowConnectionSuccess { get; set; } = false;
        public int DownloadLayoutDefinition { get; set; } = 0;
        public string DownloadDirectory { get; set; } = "";
        public int AnimationSpeed { get; set; } = 10;
    }
}