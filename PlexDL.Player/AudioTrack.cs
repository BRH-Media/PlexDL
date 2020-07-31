using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media audio track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class AudioTrack : HideObjectMembers
    {
        #region Fields (Audio Track Class)

        internal Guid _mediaType;
        internal string _name;
        internal string _language;
        internal int _channelCount;
        internal int _samplerate;
        internal int _bitdepth;
        internal int _bitrate;

        #endregion Fields (Audio Track Class)

        internal AudioTrack()
        {
        }

        /// <summary>
        /// Gets the media type (GUID) of the track (see Media Foundation documentation).
        /// </summary>
        public Guid MediaType { get { return _mediaType; } }

        /// <summary>
        /// Gets the name of the track.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the language of the track.
        /// </summary>
        public string Language { get { return _language; } }

        /// <summary>
        /// Gets the number of channels in the track.
        /// </summary>
        public int ChannelCount { get { return _channelCount; } }

        /// <summary>
        /// Gets the sample rate of the track.
        /// </summary>
        public int SampleRate { get { return _samplerate; } }

        /// <summary>
        /// Gets the bit depth of the track.
        /// </summary>
        public int BitDepth { get { return _bitdepth; } }

        /// <summary>
        /// Gets the bit rate of the track.
        /// </summary>
        public int Bitrate { get { return _bitrate; } }
    }
}