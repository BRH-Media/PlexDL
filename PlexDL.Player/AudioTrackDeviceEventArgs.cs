using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaAudioTrackDeviceChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AudioTrackDeviceEventArgs : HideObjectEventArgs
    {
        #region Fields (TrackDeviceEventArgs)

        internal int    _track;

        #endregion

        internal AudioTrackDeviceEventArgs() { }

        /// <summary>
        /// Gets the (zero-based) index of the audio track whose audio output device has been changed. 
        /// <br/>A value of -1 indicates that the audio output devices of all audio tracks may have been changed.
        /// </summary>
        public int Track
        {
            get { return _track; }
        }
    }
}