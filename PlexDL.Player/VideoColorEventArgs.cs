using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaVideoColorChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class VideoColorEventArgs : HideObjectEventArgs
    {
        internal VideoColorAttribute _colorAttribute;
        internal double              _colorValue;

        internal VideoColorEventArgs(VideoColorAttribute attribute, double value)
        {
            _colorAttribute = attribute;
            _colorValue = value;
        }

        /// <summary>
        /// Gets the video image color attribute that has changed (for example, VideoColorAttribute.Brightness).
        /// </summary>
        public VideoColorAttribute ColorAttribute
        {
            get { return _colorAttribute; }
        }

        /// <summary>
        /// Gets the value of the changed video image color attribute. Values from -1.0 to 1.0.
        /// </summary>
        public double ColorValue
        {
            get { return _colorValue; }
        }
    }
}