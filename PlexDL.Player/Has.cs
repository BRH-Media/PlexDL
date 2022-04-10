using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Has (active components) properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Has : HideObjectMembers
    {
        #region Fields (Has Class)

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

        internal Has(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains audio.
        /// </summary>
        public bool Audio
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains audio but no video.
        /// </summary>
        public bool AudioOnly
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._hasVideo) return false;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has active peak level information of the audio output.
        /// </summary>
        public bool AudioPeakLevels
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.pm_HasPeakMeter;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has active peak level information of the audio input.
        /// </summary>
        public bool AudioInputLevels
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.pm_HasInputMeter;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player's multi-track audio playback is enabled.
        /// </summary>
        public bool AudioMultiTrack
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioMultiTrack;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video.
        /// </summary>
        public bool Video
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasVideo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video but no audio.
        /// </summary>
        public bool VideoOnly
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._hasAudio) return false;
                return _base._hasVideo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player's video rotation is activated.
        /// </summary>
        public bool VideoRotation
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._videoRotation > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display overlay.
        /// <br/>See also: Player.Has.VideoOverlay.
        /// </summary>
        public bool Overlay
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player's display overlay is shown.
        /// </summary>
        public bool OverlayShown
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasOverlayShown;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a video bitmap overlay.
        /// <br/>See also: Player.Has.Overlay.
        /// </summary>
        public bool BitmapOverlay
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasImageOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has one or more display clones.
        /// </summary>
        public bool DisplayClones
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.dc_HasDisplayClones;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media has active subtitles.
        /// </summary>
        public bool Subtitles
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base.st_HasSubtitles;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has one or more taskbar progress indicators.
        /// </summary>
        public bool TaskbarProgress
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasTaskbarProgress;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a custom shaped display window.
        /// </summary>
        public bool DisplayShape
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasDisplayShape;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing media (including paused media).
        /// <br/>See also: Player.Media.SourceType.
        /// </summary>
        public bool Media
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._playing;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player pauses media instead of ending it when playback is complete.
        /// </summary>
        public bool MediaEndedPaused
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._endPauseMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an image (including paused image).
        /// <br/>See also: Player.Media.SourceType.
        /// </summary>
        public bool Image
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._imageMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing a webcam (including paused webcam).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool Webcam
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing a webcam with audio (including paused webcam with audio).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool WebcamWithAudio
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._webcamAggregated;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing a live stream (including paused live stream).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool LiveStream
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._liveStreamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an online file (not live) stream (including paused online file stream).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool FileStream
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._fileStreamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an online (file or live) stream (including paused online stream).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool OnlineStream
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._liveStreamMode | _base._fileStreamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an audio input device (without a webcam - including paused audio input).
        /// <br/>See also: Player.Media.SourceType and Player.Media.SourceCategory.
        /// </summary>
        public bool AudioInput
        {
            get
            {
                _base._lastError = NO_ERROR;
                return ((_base._webcamMode && _base._webcamAggregated) || _base._micMode);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display window.
        /// </summary>
        public bool Display
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasDisplay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing media chapters.
        /// <br/>Applies only to chapters played using the Player.Play method.
        /// </summary>
        public bool Chapters
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._chapterMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the devices (webcam and/or audio input) played by the player are recorded.
        /// </summary>
        public bool DeviceRecording
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._recording;
            }
        }

        /// <summary>
        /// Gets a value indicating whether video motion detection is enabled and active.
        /// </summary>
        public bool MotionDetection
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._motionDetectionActive;
            }
        }
    }
}