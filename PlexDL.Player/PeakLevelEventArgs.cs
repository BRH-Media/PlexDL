using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaPeakLevelChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PeakLevelEventArgs : HideObjectEventArgs
    {
        internal int _channelCount;
        internal float _masterPeakValue;
        internal float[] _channelsValues;

        /// <summary>
        /// Gets the number of audio output channels (and number of peak values) returned by the ChannelsValues property (usually 2 for stereo devices).
        /// </summary>
        public int ChannelCount
        {
            get { return _channelCount; }
        }

        /// <summary>
        /// Gets the highest peak value of the audio output channels (ChannelsValues). Values are from 0.0 to 1.0 (inclusive) or -1 if media playback is paused or stopped.
        /// </summary>
        public float MasterPeakValue
        {
            get { return _masterPeakValue; }
        }

        /// <summary>
        /// Gets the peak values of the audio output channels: ChannelsValues[0] contains the value of the left audio output channel and ChannelsValues[1] of the right channel. More channels can be present with, for example, surround sound systems. Values are from 0.0 to 1.0 (inclusive) or -1 if media playback is paused or stopped.
        /// </summary>
        public float[] ChannelsValues
        {
            get { return _channelsValues; }
        }
    }
}