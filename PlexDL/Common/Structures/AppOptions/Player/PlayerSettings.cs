namespace PlexDL.Common.Structures.AppOptions.Player
{
    public class PlayerSettings
    {
        public string VLCMediaPlayerArgs { get; set; } = @"%FILE% --meta-title=%TITLE%";
        public string VLCMediaPlayerPath { get; set; } = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
        public decimal SkipForwardInterval { get; set; } = 30;
        public decimal SkipBackwardInterval { get; set; } = 10;
        public bool PlayNextTitleAutomatically { get; set; } = false;
        public PlayerKeyBindings KeyBindings { get; set; } = new PlayerKeyBindings();
        public int PlaybackEngine { get; set; } = PlaybackMode.PVSPlayer;
        public bool ShowFSMessage { get; set; } = true;
    }
}