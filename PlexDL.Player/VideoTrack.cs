using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media video track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class VideoTrack : HideObjectMembers
    {
        #region Fields (Video Track Class)

        internal Guid   _mediaType;
        internal string _name;
        internal string _language;
        internal float  _frameRate;
        internal int    _width;
        internal int    _height;

        #endregion

        internal VideoTrack() { }

        /// <summary>
        /// Gets the media type (MF GUID) of the track (see Media Foundation documentation).
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
        /// Gets the frame rate of the track.
        /// </summary>
        public float FrameRate { get { return _frameRate; } }

        /// <summary>
        /// Gets the video width of the track.
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// Gets the video height of the track.
        /// </summary>
        public int Height { get { return _height; } }
    }
}