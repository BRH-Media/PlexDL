using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store media chapter information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class MediaChapter : HideObjectMembers
    {
        #region Fields (Media Chapter Class)

        internal string _title;
        internal TimeSpan _startTime;

        #endregion Fields (Media Chapter Class)

        internal MediaChapter()
        {
        }

        /// <summary>
        /// Gets the title of the media chapter.
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// Gets the start time of the media chapter.
        /// </summary>
        public TimeSpan StartTime
        {
            get { return _startTime; }
        }
    }
}