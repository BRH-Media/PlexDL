using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Player
{
    [Serializable]
    public class PlayerSettings
    {
        [DisplayName("VLC Arguments")]
        [Description("When launching VLC Media Player, PlexDL will execute these arguments.")]
        public string VlcMediaPlayerArgs { get; set; } = @"%FILE% --meta-title=%TITLE%";

        [DisplayName("VLC Path")]
        [Description("The path to \"vlc.exe\"")]
        public string VlcMediaPlayerPath { get; set; } = @"C:\Program Files\VideoLAN\VLC\vlc.exe";

        [DisplayName("Skip Forward Step")]
        [Description("The number of seconds to skip forward by in PVS Player.")]
        public decimal SkipForwardInterval { get; set; } = 30;

        [DisplayName("Skip Backward Step")]
        [Description("The number of seconds to skip backward by in PVS Player.")]
        public decimal SkipBackwardInterval { get; set; } = 10;

        [DisplayName("Autoplay")]
        [Description(
            "When the current title is finished playing, PlexDL can automatically play the next one (if using PVS Player)")]
        public bool PlayNextTitleAutomatically { get; set; } = false;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReadOnly(true)]
        [DisplayName("Player Key Bindings")]
        [Description("These are the set shortcut keys for PVS Player.")]
        public PlayerKeyBindings KeyBindings { get; set; } = new PlayerKeyBindings();

        [DisplayName("Player Engine")]
        [Description("A value indicating what PlaybackMode PlexDL should use when streaming content. Current modes:\n" +
                     "[0] - PVS Player\n" +
                     "[1] - VLC Media Player\n" +
                     "[2] - Default Browser\n" +
                     "[3] - MenuSelector")]
        public PlaybackMode PlaybackEngine { get; set; } = PlaybackMode.MenuSelector;

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}