using System;
using System.Windows.Forms;

namespace PlexDL.Common.Structures.AppOptions.Player
{
    [Serializable]
    public class PlayerKeyBindings
    {
        public Keys PlayPause { get; set; } = Keys.Space;
        public Keys SkipForward { get; set; } = Keys.Right;
        public Keys SkipBackward { get; set; } = Keys.Left;
        public Keys NextTitle { get; set; } = Keys.Up;
        public Keys PrevTitle { get; set; } = Keys.Down;
        public Keys FullscreenToggle { get; set; } = Keys.F;
        public Keys FramerateToggle { get; set; } = Keys.G;

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}