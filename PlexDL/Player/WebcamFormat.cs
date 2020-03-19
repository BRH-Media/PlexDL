using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store webcam video output format information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class WebcamFormat : HideObjectMembers
    {
        internal int _streamIndex;
        internal int _typeIndex;
        internal int _width;
        internal int _height;
        internal float _frameRate;

        internal WebcamFormat(int streamIndex, int typeIndex, int width, int height, float frameRate)
        {
            _streamIndex = streamIndex;
            _typeIndex = typeIndex;
            _width = width;
            _height = height;
            _frameRate = frameRate;
        }

        /// <summary>
        /// Gets the track number of the format.
        /// </summary>
        public int Track => _streamIndex;

        /// <summary>
        /// Gets the index of the format.
        /// </summary>
        public int Index => _typeIndex;

        /// <summary>
        /// Gets the width of the video frames of the format, in pixels.
        /// </summary>
        public int VideoWidth => _width;

        /// <summary>
        /// Gets the height of the video frames of the format, in pixels.
        /// </summary>
        public int VideoHeight => _height;

        /// <summary>
        /// Gets the video frame rate of the format, in frames per second.
        /// </summary>
        public float FrameRate => _frameRate;
    }
}