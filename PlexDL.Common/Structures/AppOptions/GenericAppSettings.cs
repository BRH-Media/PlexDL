using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Common.Structures.AppOptions
{
    [Serializable]
    public class GenericAppSettings
    {
        [ReadOnly(true)]
        [DisplayName("PlexDL Version")]
        [Description("The version of PlexDL that you're currently running")]
        public string StoredAppVersion { get; set; } = Application.ProductVersion;

        [DisplayName("Autostream")]
        [Description(
            "When the user double-clicks on a selected episode, or a selected movie, it will launch the default Playback Engine. If the engine is set to MenuSelect, then VLC will be launched. When this is disabled," +
            "grids will resume showing the cell content instead of launching a player.")]
        public bool DoubleClickLaunch { get; set; } = true;

        [DisplayName("Autoupdate")]
        [Description("When enabled, PlexDL will check for new updates on startup. This requires 'Commit to Default'.")]
        public bool AutoUpdateEnabled { get; set; } = false;

        [DisplayName("Speed Throttle (Mbps)")]
        [Description(
            "PlexDL can throttle your downloads to a specified limit. When set to 0, the limit is removed, otherwise download speeds should not exceed the defined value.")]
        public decimal DownloadSpeedLimit { get; set; } = 0;

        [ReadOnly(true)]
        [DisplayName("Download Folder")]
        [Description(
            "The location where PlexDL will store your downloaded content. Change this value via the File menu.")]
        public string DownloadDirectory { get; set; } = "";

        [DisplayName("Adult Content Filtering")]
        [Description(
            "By default, PlexDL will pixelate potentially adult-orientated poster images, and show a warning when trying to stream the associated content")]
        public bool AdultContentProtection { get; set; } = true;

        [DisplayName("Connection Success Message")]
        [Description(
            "For debugging. This enables/disables a message informing the user that the connection was successful.")]
        public bool ShowConnectionSuccess { get; set; } = false;

        [ReadOnly(true)]
        [DisplayName("Layout Definition")]
        [Description("PlexDL follows internal rules for creating download layouts. This value should not be changed.")]
        public int DownloadLayoutDefinition { get; set; } = 0;

        [DisplayName("String Length")]
        [Description(
            "PlexDL limits string lengths so they display correctly. You can increase or decrease this value if things are not displaying as expected.")]
        public int DefaultStringLength { get; set; } = 64;

        [DisplayName("Search Column Priority")]
        [Description(
            "When searching, PlexDL will prioritise what column to autoselect in the Search Form. Matches are performed from index 0 onwards, so " +
            "the first value in this field to be matched will be selected, and other matches will be ignored.")]
        public List<string> SearchColumnPriority { get; set; } = new List<string>
        {
            "title",
            "Entry",
            "SessionID",
            "DateTime"
        };

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}