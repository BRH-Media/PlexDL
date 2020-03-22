using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Common.Structures.AppOptions
{
    public class GenericAppSettings
    {
        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }

        [ReadOnly(true)]
        [DisplayName("PlexDL Version")]
        [Description("The version of PlexDL that you're currently running")]
        public string StoredAppVersion { get; set; } = Application.ProductVersion;

        [ReadOnly(true)]
        [DisplayName("Download Folder")]
        [Description("The location where PlexDL will store your downloaded content. Change this value via the File menu.")]
        public string DownloadDirectory { get; set; } = "";

        [DisplayName("Adult Content Filtering")]
        [Description("By default, PlexDL will pixelate potentially adult-orientated poster images, and show a warning when trying to stream the associated content")]
        public bool AdultContentProtection { get; set; } = true;

        [DisplayName("Connection Success Message")]
        [Description("For debugging. This enables/disables a message informing the user that the connection was successful.")]
        public bool ShowConnectionSuccess { get; set; } = false;

        [ReadOnly(true)]
        [DisplayName("Layout Definition")]
        [Description("PlexDL follows internal rules for creating download layouts. This value should not be changed.")]
        public int DownloadLayoutDefinition { get; set; } = 0;

        [DisplayName("String Length")]
        [Description("PlexDL limits string lengths so they display correctly. You can increase or decrease this value if things are not displaying as expected.")]
        public int DefaultStringLength { get; set; } = 64;

        [ReadOnly(true)]
        [DisplayName("Animation Speed")]
        [Description("This is a deprecated value")]
        public int AnimationSpeed { get; set; } = 10;
    }
}