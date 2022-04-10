using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaAudioVolumeChanged, .MediaAudioBalanceChanged and .MediaAudioMuteChanged events.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AudioEventArgs : HideObjectEventArgs
    {
        #region Fields (AudioChangedEventArgs)

        internal int    _track;
        internal float  _volume;
        internal float  _balance;
        internal bool   _muted;

        #endregion

        internal AudioEventArgs() { }

        /// <summary>
        /// Gets the (zero-based) index of the audio track whose volume, balance, or mute status has been changed. A value of -1 indicates that the player's audio has been changed without playing any audio tracks.
        /// </summary>
        public int Track
        {
            get { return _track; }
        }

        /// <summary>
        /// Gets the volume of the audio track specified in the Track property of this event data. Values from 0.0 (mute) to 1.0 (max).
        /// </summary>
        public float Volume
        {
            get { return _volume; }
        }

        /// <summary>
        /// Gets the balance of the audio track specified in the Track property of this event data. Values from -1.0 (left) to 1.0 (right).
        /// </summary>
        public float Balance
        {
            get { return _balance; }
        }

        /// <summary>
        /// Gets the mute status of the audio track specified in the Track property of this event data. 
        /// </summary>
        public bool Muted
        {
            get { return _muted; }
        }
    }
}