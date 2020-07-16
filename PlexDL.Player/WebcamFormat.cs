using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store webcam video output format information.
    /// </summary>
    [CLSCompliant(true)]
    [Serializable]
    public sealed class WebcamFormat : HideObjectMembers
    {
        #region Fields (Webcam Video Format Class)

        internal int    _streamIndex;
        internal int    _typeIndex;
        internal int    _width;
        internal int    _height;
        internal float  _frameRate;

        #endregion

        internal WebcamFormat(int streamIndex, int typeIndex, int width, int height, float frameRate)
        {
            _streamIndex    = streamIndex;
            _typeIndex      = typeIndex;
            _width          = width;
            _height         = height;
            _frameRate      = frameRate;
        }

        /// <summary>
        /// Gets the track number of the format.
        /// </summary>
        public int Track { get { return _streamIndex; } }

        /// <summary>
        /// Gets the index of the format.
        /// </summary>
        public int Index { get { return _typeIndex; } }

        /// <summary>
        /// Gets the width of the video frames of the format, in pixels.
        /// </summary>
        public int VideoWidth { get { return _width; } }

        /// <summary>
        /// Gets the height of the video frames of the format, in pixels.
        /// </summary>
        public int VideoHeight { get { return _height; } }

        /// <summary>
        /// Gets the video frame rate of the format, in frames per second.
        /// </summary>
        public float FrameRate { get { return _frameRate; } }

    }
}