using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaEnded event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class EndedEventArgs : HideObjectEventArgs
    {
        internal StopReason _reason;
        internal bool _webcam;
        internal bool _mic;
        internal int _error;

        /// <summary>
        /// Specifies the reason that the media has stopped playing.
        /// </summary>
        public StopReason StopReason => _reason;

        /// <summary>
        /// The error code (LastErrorCode) of the player when the media stopped playing (for a description of the error use Player.GetErrorString(e.ErrorCode)).
        /// </summary>
        public int ErrorCode => _error;

        /// <summary>
        /// Gets a value indicating whether a webcam has stopped playing.
        /// </summary>
        public bool Webcam => _webcam;

        /// <summary>
        /// Gets a value indicating whether an audio input device has stopped playing.
        /// </summary>
        public bool AudioInput => _mic;
    }
}