using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Images properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Images : HideObjectMembers
    {
        #region Fields (Images Class)

        private const int NO_ERROR          = 0;
        private const int MINIMUM_FRAMERATE = 4;
        private const int MAXIMUM_FRAMERATE = 30;
        private const int MINIMUM_DURATION  = 1;
        private const int MAXIMUM_DURATION  = 60;

        private Player _base;

        #endregion

        internal Images(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player also plays images (default: true).
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._imagesEnabled;
            }
            set
            {
                _base._lastError = NO_ERROR;
                _base._imagesEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the frame rate (frames per second (fps)) at which images are played by the player.
        /// <br/>Values from 4 to 30 frames per second (default: 16 fps).
        /// </summary>
        public int FrameRate
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._imageFrameRate;
            }
            set
            {
                if (value < MINIMUM_FRAMERATE || value > MAXIMUM_FRAMERATE)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    _base._lastError = NO_ERROR;
                    _base._imageFrameRate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates how long an image is displayed by the player.
        /// <br/>Values from 1 to 60 seconds (default: 5).
        /// <br/>Due to the conversion to a video file, it is more efficient to decrease the
        /// <br/>playback speed (Player.Speed.Rate) than to increase the duration. 
        /// </summary>
        public int Duration
        {
            get
            {
                _base._lastError = NO_ERROR;
                return (int)(_base._imageDuration / Player.ONE_SECOND_TICKS);
            }
            set
            {
                if (value < MINIMUM_DURATION || value > MAXIMUM_DURATION)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    _base._lastError = NO_ERROR;
                    _base._imageDuration = value * Player.ONE_SECOND_TICKS;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is playing an image (including paused images).
        /// <br/>Images can be played using the Player.Play method.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._imageMode;
            }
        }

        /// <summary>
        /// Updates or restores the video image on the player's display window.
        /// <br/>For special use only, generally not required.
        /// </summary>
        public int Update()
        {
            if (_base._imageMode && _base.mf_VideoDisplayControl != null)
            {
                _base._lastError = NO_ERROR;
                _base.mf_VideoDisplayControl.RepaintVideo();
            }
            else { _base._lastError = HResult.MF_E_NOT_AVAILABLE; }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the contents of the player's display window are preserved
        /// <br/>after a media file has finished playing (default: false).
        /// <br/>Can be used to smooth the transition between consecutively played media.
        /// <br/>If set to true, the value must be reset to false when all media has finished playing to clear the display.
        /// <br/>Same as Player.Display.Hold.
        /// <br/>See also: Player.Images.HoldClear.
        /// </summary>
        public bool Hold
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._displayHold;
            }
            set
            {
                if (value != _base._displayHold)
                {
                    _base._displayHold = value;
                    if (!value) _base.AV_ClearHold();
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Clears the player's display when the Player.Image.Hold option is enabled and no media is playing.
        /// <br/>Same as Player.Display.HoldClear.
        /// <br/>See also: Player.Image.Hold.
        /// </summary>
        public int HoldClear()
        {
            if (_base._displayHold)
            {
                _base.AV_ClearHold();
                _base._lastError = NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }
    }
}