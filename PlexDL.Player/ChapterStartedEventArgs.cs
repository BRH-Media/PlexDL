using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaChapterStarted event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ChapterStartedEventArgs : HideObjectEventArgs
    {
        #region Fields (ChapterStartedEventArgs)

        internal int _index;
        internal string _title;

        #endregion

        internal ChapterStartedEventArgs(int index, string title)
        {
            _index = index;
            _title = title;
        }

        /// <summary>
        /// Gets the (zero-based) index in the chapter list of the chapter that started playing. A value of -1 indicates that chapter playback has ended before media playback has ended (this event is not raised when media playback has ended).
        /// </summary>
        public int Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Gets the title of the chapter that started playing.
        /// </summary>
        public string Title
        {
            get { return _title; }
        }
    }
}