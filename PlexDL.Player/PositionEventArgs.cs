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

        #endregion

        /// <summary>
        /// Gets the playback position of the playing media, measured from the (natural) beginning of the media. Values in ticks (for example, use TimeSpan.FromTicks).
        /// </summary>
        public long FromBegin
        {
            get { return _fromBegin; }
        }

        /// <summary>
        /// Gets the playback position of the playing media, measured from the (natural) end of the media. Values in ticks (for example, use TimeSpan.FromTicks).
        /// </summary>
        public long ToEnd
        {
            get { return _toEnd; }
        }

        /// <summary>
        /// Gets the playback position of the playing media, measured from its (adjustable) start time. Values in ticks (for example, use TimeSpan.FromTicks).
        /// </summary>
        public long FromStart
        {
            get { return _fromStart; }
        }

        /// <summary>
        /// Gets the playback position of the playing media, measured from its (adjustable) stop time. Values in ticks (for example, use TimeSpan.FromTicks).
        /// </summary>
        public long ToStop
        {
            get { return _toStop; }
        }
    }
}