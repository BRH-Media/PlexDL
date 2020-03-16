using System.Windows.Forms;

namespace PlexDL.Common.Structures.AppOptions
{
    public class GenericAppSettings
    {
        public string StoredAppVersion { get; set; } = Application.ProductVersion.ToString();
        public string DownloadDirectory { get; set; } = "";
        public bool AdultContentProtection { get; set; } = true;
        public bool ShowConnectionSuccess { get; set; } = false;
        public int DownloadLayoutDefinition { get; set; } = 0;
        public int DefaultStringLength { get; set; } = 64;
        public int AnimationSpeed { get; set; } = 10;
    }
}