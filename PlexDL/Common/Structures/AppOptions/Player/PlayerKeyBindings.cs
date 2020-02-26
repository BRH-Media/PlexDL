using System.Windows.Forms;

namespace PlexDL.Common.Structures.AppOptions.Player
{
    public class PlayerKeyBindings
    {
        public Keys PlayPause { get; set; } = Keys.Space;
        public Keys SkipForward { get; set; } = Keys.Right;
        public Keys SkipBackward { get; set; } = Keys.Left;
        public Keys NextTitle { get; set; } = Keys.Up;
        public Keys PrevTitle { get; set; } = Keys.Down;
    }
}