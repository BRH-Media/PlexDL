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

        internal string[]   _title;
        internal string[]   _language;
        internal TimeSpan   _startTime;
        internal TimeSpan   _endTime;

        #endregion

        internal MediaChapter() { }

        /// <summary>
        /// Gets the title of the media chapter. The chapter can have multiple titles in different languages.
        /// </summary>
        public string[] Title
        {
            get { return _title; }
        }

        /// <summary>
        /// Gets the language (3 letter name (ISO 639.2)) used for the title of the media chapter or null if not available. The index of the language corresponds to the index of the title.
        /// </summary>
        public string[] Language
        {
            get { return _language; }
        }

        /// <summary>
        /// Gets the start time of the media chapter.
        /// </summary>
        public TimeSpan StartTime
        {
            get { return _startTime; }
        }

        /// <summary>
        /// Gets the end time of the media chapter. TimeSpan.Zero indicates the beginning of the next chapter or the end of the file.
        /// </summary>
        public TimeSpan EndTime
        {
            get { return _endTime; }
        }
    }
}