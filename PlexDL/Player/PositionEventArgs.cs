using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaPositionChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PositionEventArgs : HideObjectEventArgs
    {
        // ******************************** Fields (PositionEventArgs)

        #region Fields (PositionEventArgs)

        internal long _fromBegin;
        internal long _toEnd;
        internal long _fromStart;
        internal long _toStop;

        #endregion Fields (PositionEventArgs)

        /// <summary>
        /// Gets the length (duration) in ticks (for example, use TimeSpan.FromTicks) of the playing media from the natural beginning of the media to the current playback position.
        /// </summary>
        public long FromBegin => _fromBegin;

        /// <summary>
        /// Gets the length (duration) in ticks (for example, use TimeSpan.FromTicks) of the playing media from the current playback position to the natural end of the media.
        /// </summary>
        public long ToEnd => _toEnd;

        /// <summary>
        /// Gets the length (duration) in ticks (for example, use TimeSpan.FromTicks) of the playing media from Player.Media.StartTime to the current playback position.
        /// </summary>
        public long FromStart => _fromStart;

        /// <summary>
        /// Gets the length (duration) in ticks (for example, use TimeSpan.FromTicks) of the playing media from the current playback position to Player.Media.StopTime.
        /// </summary>
        public long ToStop => _toStop;
    }
}