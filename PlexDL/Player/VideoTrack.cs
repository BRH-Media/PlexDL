using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media video track information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class VideoTrack : HideObjectMembers
    {
        #region VideoTrack Fields

        internal Guid _mediaType;
        internal string _name;
        internal string _language;
        internal float _frameRate;
        internal int _width;
        internal int _height;

        #endregion VideoTrack Fields

        internal VideoTrack()
        {
        }

        /// <summary>
        /// Gets the media type (MF GUID) of the track (see Media Foundation documentation).
        /// </summary>
        public Guid MediaType => _mediaType;

        /// <summary>
        /// Gets the name of the track.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets the language of the track.
        /// </summary>
        public string Language => _language;

        /// <summary>
        /// Gets the frame rate of the track.
        /// </summary>
        public float FrameRate => _frameRate;

        /// <summary>
        /// Gets the video width of the track.
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Gets the video height of the track.
        /// </summary>
        public int Height => _height;
    }
}