using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Images properties of the PlexDL.Player.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Images : HideObjectMembers
    {
        #region Fields (Images Class)

        private const int MINIMUM_FRAMERATE = 4;
        private const int MAXIMUM_FRAMERATE = 30;
        private const int MINIMUM_DURATION = 3;
        private const int MAXIMUM_DURATION = 60;

        private Player _base;

        #endregion Fields (Images Class)

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
                _base._lastError = Player.NO_ERROR;
                return _base._imagesEnabled;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                _base._imagesEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the frame rate at which images are played. Values from 4 to 30 frames per second (default: 16).
        /// </summary>
        public int FrameRate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
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
                    _base._lastError = Player.NO_ERROR;
                    _base._imageFrameRate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the duration of image playback. Values from 3 to 60 seconds (default: 5).
        /// </summary>
        public int Duration
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
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
                    _base._lastError = Player.NO_ERROR;
                    _base._imageDuration = value * Player.ONE_SECOND_TICKS;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether an image is playing (including paused image). Use the Player.Play method to play an image.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._imageMode;
            }
        }

        /// <summary>
        /// Updates or restores the playing image on the display of the player. For special use only, generally not required.
        /// </summary>
        public int Update()
        {
            if (_base._imageMode && _base.mf_VideoDisplayControl != null)
            {
                _base._lastError = Player.NO_ERROR;
                _base.mf_VideoDisplayControl.RepaintVideo();
            }
            else { _base._lastError = HResult.MF_E_NOT_AVAILABLE; }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the image on the player's display will be preserved when media has finished playing (default: false). If set to true, the value must be reset to false when all media playback is complete to clear the display. Same as: Player.Display.Hold. See also: Player.Image.HoldClear.
        /// </summary>
        public bool Hold
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._displayHold;
            }
            set
            {
                if (value != _base._displayHold)
                {
                    _base._displayHold = value;
                    if (!value) _base.AV_ClearHold();
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Clears the player's display when the Player.Image.Hold option is enabled and no media is playing. Same as 'Player.Image.Hold = false' but does not disable the Player.Image.Hold option. Same as: Player.Display.HoldClear. See also: Player.Image.Hold.
        /// </summary>
        public int HoldClear()
        {
            if (_base._displayHold)
            {
                _base.AV_ClearHold();
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }
    }
}