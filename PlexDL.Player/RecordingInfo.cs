using System;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to store recording information.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class RecordingInfo : HideObjectMembers
    {
        #region Fields (RecordingInfo Class)

        internal string     _recorderName;
        internal string     _fileName;

        internal bool       _hasAudio;
        internal string     _audioDeviceName;
        internal AudioTrack _audio;

        internal bool       _hasVideo;
        internal string     _videoDeviceName;
        internal VideoTrack _video;

        #endregion

        internal RecordingInfo() { }

        /// <summary>
        /// Gets the  name of the recorder (player) used for the recording.
        /// </summary>
        public string RecorderName { get { return _recorderName; } }

        /// <summary>
        /// Gets the path and file name of the recording.
        /// </summary>
        public string FileName { get { return _fileName; } }

        /// <summary>
        /// Gets a value indicating whether the recording contains audio.
        /// </summary>
        public bool HasAudio { get { return _hasAudio; } }

        /// <summary>
        /// Gets the name of the audio device used for the recording.
        /// </summary>
        public string AudioDeviceName { get { return _audioDeviceName; } }

        /// <summary>
        /// Gets the audio track information of the recording.
        /// </summary>
        public AudioTrack Audio { get { return _audio; } }

        /// <summary>
        /// Gets a value indicating whether the recording contains video.
        /// </summary>
        public bool HasVideo { get { return _hasVideo; } }

        /// <summary>
        /// Gets the name of the video device used for the recording.
        /// </summary>
        public string VideoDeviceName { get { return _videoDeviceName; } }

        /// <summary>
        /// Gets the video track information of the recording.
        /// </summary>
        public VideoTrack Video { get { return _video; } }

        /// <summary>
        /// Returns a string with a detailed description of the audio track of the recording.
        /// </summary>
        public string AudioString()
        {
            string result = string.Empty;

            if (_hasAudio)
            {
                if (!string.IsNullOrEmpty(_audioDeviceName)) result = _audioDeviceName + " - ";
                if (_audio != null) result += _audio.ToString();
            }
            return result;
        }

        /// <summary>
        ///  Returns a string with a detailed description of the video track of the recording.
        /// </summary>
        public string VideoString()
        {
            string result = string.Empty;

            if (_hasVideo)
            {
                if (!string.IsNullOrEmpty(_videoDeviceName)) result = _videoDeviceName + " - ";
                if (_video != null) result += _video.ToString();
            }
            return result;
        }
    }
}