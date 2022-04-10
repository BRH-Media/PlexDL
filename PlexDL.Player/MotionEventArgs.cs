using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaMotionDetected event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class MotionEventArgs : HideObjectEventArgs
    {
        #region Fields (MotionEventArgs)

        internal int _motion;

        #endregion

        internal MotionEventArgs() { }

        /// <summary>
        /// Gets the amount of video motion detected as a percentage of the total number of pixels in an image.
        /// 
        /// <br/>Values from 0 to 100.
        /// </summary>
        public int Motion
        {
            get { return _motion; }
        }
    }
}