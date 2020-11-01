using System;
using System.Drawing;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store metadata properties obtained from media files.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class Metadata : HideObjectMembers, IDisposable
    {
        #region Fields (Metadata Class)

        internal string     _artist;
        internal string     _title;
        internal string     _album;
        internal int        _trackNumber;
        internal string     _year;
        internal TimeSpan   _duration;
        internal string     _genre;
        internal Image      _image;

        private bool        _disposed;

        #endregion

        internal Metadata() { }

        /// <summary>
        /// Gets the main artist(s)/performer(s)/band/orchestra of the media.
        /// </summary>
        public string Artist { get { return _artist; } }

        /// <summary>
        /// Gets the title of the media.
        /// </summary>
        public string Title { get { return _title; } }

        /// <summary>
        /// Gets the title of the album that contains the media.
        /// </summary>
        public string Album { get { return _album; } }

        /// <summary>
        /// Gets the track number of the media.
        /// </summary>
        public int TrackNumber { get { return _trackNumber; } }

        /// <summary>
        /// Gets the year the media was published.
        /// </summary>
        public string Year { get { return _year; } }

        /// <summary>
        /// Gets the duration (length) of the media.
        /// </summary>
        public TimeSpan Duration { get { return _duration; } }

        /// <summary>
        /// Gets the genre of the media.
        /// </summary>
        public string Genre { get { return _genre; } }

        /// <summary>
        /// Gets the image attached to the media.
        /// </summary>
        public Image Image { get { return _image; } }

        /// <summary>
        /// Remove the metadata information and clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed   = true;
                _artist     = null;
                _title      = null;
                _album      = null;
                _year       = null;
                _genre      = null;
                if (_image != null)
                {
                    try { _image.Dispose(); }
                    catch { /* ignore */ }
                    _image = null;
                }
            }
        }
    }
}