using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the playback Speed methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Speed : HideObjectMembers
    {
        #region Fields (Speed Class)

        private Player _base;

        #endregion Fields (Speed Class)

        internal Speed(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets a value indicating the speed at which media is played by the player (normal speed = 1.0). This setting is adjusted by the player if media cannot be played at the set speed.
        /// </summary>
        public float Rate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speed;
            }
            set
            {
                if (value < _base.mf_SpeedMinimum || value > _base.mf_SpeedMaximum)
                {
                    _base._lastError = HResult.MF_E_UNSUPPORTED_RATE;
                }
                else _base.AV_SetSpeed(value, true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether stream thinning (displaying fewer video frames) should be used when playing media. This option can be used to increase the maximum playback speed of media (default: false).
        /// </summary>
        public bool Boost
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speedBoost;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (value != _base._speedBoost)
                {
                    _base._speedBoost = value;

                    if (_base.mf_RateControl != null)
                    {
                        ((IMFRateSupport)_base.mf_RateControl).GetFastestRate(MFRateDirection.Forward, _base._speedBoost, out _base.mf_SpeedMaximum);
                        ((IMFRateSupport)_base.mf_RateControl).GetSlowestRate(MFRateDirection.Forward, _base._speedBoost, out _base.mf_SpeedMinimum);

                        if (_base._speed != Player.DEFAULT_SPEED)
                        {
                            float trueSpeed;
                            _base.mf_RateControl.GetRate(_base._speedBoost, out trueSpeed);
                            if (_base._speed != trueSpeed)
                            {
                                _base._speed = trueSpeed == 0 ? 1 : trueSpeed;
                                _base.mf_Speed = _base._speed;
                                if (_base._speedSlider != null) _base.SpeedSlider_ValueToSlider(_base._speed);
                                if (_base._mediaSpeedChanged != null) _base._mediaSpeedChanged(this, EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating the minimum speed at which the playing media can be played by the player.
        /// </summary>
        public float Minimum
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.mf_SpeedMinimum;
            }
        }

        /// <summary>
        /// Gets a value indicating the maximum speed at which the playing media can be played by the player.
        /// </summary>
        public float Maximum
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base.mf_SpeedMaximum;
            }
        }
    }
}