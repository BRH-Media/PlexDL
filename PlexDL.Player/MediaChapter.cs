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
        /// Initializes a new instance of the MediaChapter class.
        /// </summary>
        /// <param name="title">The title of the media chapter.</param>
        /// <param name="startTime">The start time of the media chapter.</param>
        /// <param name="endTime">The end time of the media chapter. TimeSpan.Zero indicates the beginning of the next chapter or the end of the media.</param>
        public MediaChapter(string title, TimeSpan startTime, TimeSpan endTime)
        {
            _title          = new string[1];
            _title[0]       = string.IsNullOrWhiteSpace(title) ? string.Empty : title;
            _startTime      = startTime;
            _endTime        = endTime;
            _language       = new string[1];
            _language[0]    = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the MediaChapter class.
        /// </summary>
        /// <param name="titles">The title(s) of the media chapter.</param>
        /// <param name="startTime">The start time of the media chapter.</param>
        /// <param name="endTime">The end time of the media chapter. TimeSpan.Zero indicates the beginning of the next chapter or the end of the media.</param>
        /// <param name="languages">The language(s) (3 letter name (ISO 639.2)) of the title(s) of the media chapter. Must be the same number as the number of titles.</param>
        public MediaChapter(string[] titles, TimeSpan startTime, TimeSpan endTime, string[] languages)
        {
            if (titles == null)
            {
                _title          = new string[1];
                _title[0]       = string.Empty;
                _language       = new string[1];
                _language[0]    = string.Empty;
            }
            else
            {
                //_title = new string[titles.Length];
                //titles.CopyTo(_title, 0);
                _title = titles;

                if (languages == null)
                {
                    _language    = new string[1];
                    _language[0] = string.Empty;
                }
                else
                {
                    //_language = new string[languages.Length];
                    //languages.CopyTo(_language, 0);
                    _language = languages;
                }
            }
            _startTime  = startTime;
            _endTime    = endTime;
        }

        /// <summary>
        /// Gets the title of the media chapter. The chapter can have multiple titles in different languages when extracted from a media file.
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