using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaSubtitleChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SubtitleEventArgs : HideObjectEventArgs
    {
        internal int    _index;
        internal string _subtitle;

        /// <summary>
        /// Gets the index of the current subtitle in the list of the player's active subtitles.
        /// </summary>
        public int Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Gets the text of the player's current subtitle (or string.Empty).
        /// </summary>
        public string Subtitle
        {
            get { return _subtitle; }
        }
    }
}