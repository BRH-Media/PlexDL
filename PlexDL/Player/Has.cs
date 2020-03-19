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

        private Player _base;

        #endregion Fields (Has Class)

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
                _base._lastError = Player.NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has audio output peak level information (by subscribing to the Player.Events.MediaPeakLevelChanged event).
        /// </summary>
        public bool AudioPeakLevels
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.pm_HasPeakMeter;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains video.
        /// </summary>
        public bool Video
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasVideo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display overlay.
        /// </summary>
        public bool Overlay
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasOverlay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has one or more display clones.
        /// </summary>
        public bool DisplayClones
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
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
                _base._lastError = Player.NO_ERROR;
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
                _base._lastError = Player.NO_ERROR;
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
                _base._lastError = Player.NO_ERROR;
                return _base._hasDisplayShape;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing media (includes paused media).
        /// </summary>
        public bool Media
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._playing;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing a webcam (includes paused webcam).
        /// </summary>
        public bool Webcam
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an audio input device (with or without a webcam - includes paused audio input).
        /// </summary>
        public bool AudioInput
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._webcamMode && _base._webcamAggregated || _base._micMode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player has a display window.
        /// </summary>
        public bool Display
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hasDisplay;
            }
        }
    }
}