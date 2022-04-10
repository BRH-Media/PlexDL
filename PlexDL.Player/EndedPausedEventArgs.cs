using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaAudioTrackDeviceChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class EndedPausedEventArgs : HideObjectEventArgs
    {
        #region Fields (EndedPausedEventArgs)

        internal bool _continue;

        #endregion

        internal EndedPausedEventArgs() {}

        /// <summary>
        /// Gets or sets a value that indicates whether media playback should still end (continue)
        /// <br/>and the pause mode canceled after this event is handled (default: false).
        /// </summary>
        public bool Continue
        {
            get { return _continue; }
            set { _continue = value; }
        }
    }
}