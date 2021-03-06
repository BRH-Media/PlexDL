﻿using System;
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
        #region Fields (EndedEventArgs)

        internal StopReason         _reason;
        internal int                _error;
        internal string             _mediaName;
        internal MediaSourceType    _sourceType;

        #endregion

        /// <summary>
        /// Gets a value that indicates the reason why the media has stopped playing.
        /// </summary>
        public StopReason StopReason
        {
            get { return _reason; }
        }

        /// <summary>
        /// Gets the error code that indicates why the media has stopped playing (for a (localized) description of the error use Player.GetErrorString(e.ErrorCode)).
        /// </summary>
        public int ErrorCode
        {
            get { return _error; }
        }

        /// <summary>
        /// Gets a value that indicates the source type of the media that has stopped playing.
        /// </summary>
        public MediaSourceType MediaSourceType
        {
            get { return _sourceType; }
        }

        /// <summary>
        /// Gets the name of the media that has stopped playing.
        /// </summary>
        public string MediaName
        {
            get { return _mediaName; }
        }
    }
}