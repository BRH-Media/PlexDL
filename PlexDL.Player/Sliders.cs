using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Sliders methods and properties of the PlexDL.Player.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Sliders : HideObjectMembers
    {
        #region Fields (Sliders Class)

        //private const int       MAX_SCROLL_VALUE = 60000;
        private Player _base;

        private PositionSlider _positionSliderClass;

        #endregion Fields (Sliders Class)

        internal Sliders(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the shuttle slider (trackbar for Player.Position.Step method) that is controlled by the player.
        /// </summary>
        public TrackBar Shuttle
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._shuttleSlider;
            }
            set
            {
                if (value != _base._shuttleSlider)
                {
                    if (_base._hasShuttleSlider)
                    {
                        _base._shuttleSlider.MouseDown -= _base.ShuttleSlider_MouseDown;
                        //_base._shuttleSlider.MouseUp    -= _base.ShuttleSlider_MouseUp;
                        _base._shuttleSlider.MouseWheel -= _base.ShuttleSlider_MouseWheel;

                        _base._shuttleSlider = null;
                        _base._hasShuttleSlider = false;
                    }

                    if (value != null)
                    {
                        _base._shuttleSlider = value;

                        _base._shuttleSlider.SmallChange = 1;
                        _base._shuttleSlider.LargeChange = 1;

                        _base._shuttleSlider.TickFrequency = 1;

                        _base._shuttleSlider.Minimum = -5;
                        _base._shuttleSlider.Maximum = 5;
                        _base._shuttleSlider.Value = 0;

                        _base._shuttleSlider.MouseDown += _base.ShuttleSlider_MouseDown;
                        //_base._shuttleSlider.MouseUp    += _base.ShuttleSlider_MouseUp;
                        _base._shuttleSlider.MouseWheel += _base.ShuttleSlider_MouseWheel;

                        //_shuttleSlider.Enabled = _playing;
                        _base._hasShuttleSlider = true;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the audio volume slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar AudioVolume
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._volumeSlider;
            }
            set
            {
                if (value != _base._volumeSlider)
                {
                    if (_base._volumeSlider != null)
                    {
                        _base._volumeSlider.MouseWheel -= _base.VolumeSlider_MouseWheel;
                        _base._volumeSlider.Scroll -= _base.VolumeSlider_Scroll;
                        _base._volumeSlider = null;
                    }

                    if (value != null)
                    {
                        _base._volumeSlider = value;

                        _base._volumeSlider.Minimum = 0;
                        _base._volumeSlider.Maximum = 100;
                        _base._volumeSlider.TickFrequency = 10;
                        _base._volumeSlider.SmallChange = 1;
                        _base._volumeSlider.LargeChange = 10;

                        _base._volumeSlider.Value = (int)(_base._audioVolume * 100);

                        _base._volumeSlider.Scroll += _base.VolumeSlider_Scroll;
                        _base._volumeSlider.MouseWheel += _base.VolumeSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the audio balance slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar AudioBalance
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._balanceSlider;
            }
            set
            {
                if (value != _base._balanceSlider)
                {
                    if (_base._balanceSlider != null)
                    {
                        _base._balanceSlider.MouseWheel -= _base.BalanceSlider_MouseWheel;
                        _base._balanceSlider.Scroll -= _base.BalanceSlider_Scroll;
                        _base._balanceSlider = null;
                    }

                    if (value != null)
                    {
                        _base._balanceSlider = value;

                        _base._balanceSlider.Minimum = -100;
                        _base._balanceSlider.Maximum = 100;
                        _base._balanceSlider.TickFrequency = 20;
                        _base._balanceSlider.SmallChange = 1;
                        _base._balanceSlider.LargeChange = 10;

                        _base._balanceSlider.Value = (int)(_base._audioBalance * 100);
                        _base._balanceSlider.Scroll += _base.BalanceSlider_Scroll;
                        _base._balanceSlider.MouseWheel += _base.BalanceSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the playback speed slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Speed
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._speedSlider;
            }
            set
            {
                if (value != _base._speedSlider)
                {
                    if (_base._speedSlider != null)
                    {
                        _base._speedSlider.MouseWheel -= _base.SpeedSlider_MouseWheel;
                        _base._speedSlider.Scroll -= _base.SpeedSlider_Scroll;
                        //_base._speedSlider.MouseUp      -= _base.SpeedSlider_MouseUp;
                        _base._speedSlider.MouseDown -= _base.SpeedSlider_MouseDown;

                        _base._speedSlider = null;
                    }

                    if (value != null)
                    {
                        _base._speedSlider = value;

                        _base._speedSlider.Minimum = 0;
                        _base._speedSlider.Maximum = 12;
                        _base._speedSlider.TickFrequency = 1;
                        _base._speedSlider.SmallChange = 1;
                        _base._speedSlider.LargeChange = 1;

                        _base.SpeedSlider_ValueToSlider(_base._speed);

                        _base._speedSlider.MouseDown += _base.SpeedSlider_MouseDown;
                        //_base._speedSlider.MouseUp      += _base.SpeedSlider_MouseUp;
                        _base._speedSlider.Scroll += _base.SpeedSlider_Scroll;
                        _base._speedSlider.MouseWheel += _base.SpeedSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="location">The relative x- and y-coordinates on the slider.</param>
        public int PointToValue(TrackBar slider, Point location)
        {
            return SliderValue.FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public int PointToValue(TrackBar slider, int x, int y)
        {
            return SliderValue.FromPoint(slider, x, y);
        }

        /// <summary>
        /// Returns the location of the specified value on the specified slider (trackbar).
        /// </summary>
        /// /// <param name="slider">The slider whose value location should be obtained.</param>
        /// <param name="value">The value of the slider.</param>
        public Point ValueToPoint(TrackBar slider, int value)
        {
            return SliderValue.ToPoint(slider, value);
        }

        /// <summary>
        /// Provides access to the position slider settings of the player (for example, Player.Sliders.Position.TrackBar).
        /// </summary>
        public PositionSlider Position
        {
            get
            {
                if (_positionSliderClass == null) _positionSliderClass = new PositionSlider(_base);
                return _positionSliderClass;
            }
        }

        /// <summary>
        /// Gets or sets the video image brightness slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Brightness
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._brightnessSlider;
            }
            set
            {
                if (value != _base._brightnessSlider)
                {
                    if (_base._brightnessSlider != null)
                    {
                        _base._brightnessSlider.MouseWheel -= _base.BrightnessSlider_MouseWheel;
                        _base._brightnessSlider.Scroll -= _base.BrightnessSlider_Scroll;
                        _base._brightnessSlider = null;
                    }

                    if (value != null)
                    {
                        _base._brightnessSlider = value;

                        _base._brightnessSlider.Minimum = -100;
                        _base._brightnessSlider.Maximum = 100;
                        _base._brightnessSlider.TickFrequency = 10;
                        _base._brightnessSlider.SmallChange = 1;
                        _base._brightnessSlider.LargeChange = 10;

                        _base._brightnessSlider.Value = (int)(_base._brightness * 100);

                        _base._brightnessSlider.Scroll += _base.BrightnessSlider_Scroll;
                        _base._brightnessSlider.MouseWheel += _base.BrightnessSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image contrast slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Contrast
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._contrastSlider;
            }
            set
            {
                if (value != _base._contrastSlider)
                {
                    if (_base._contrastSlider != null)
                    {
                        _base._contrastSlider.MouseWheel -= _base.ContrastSlider_MouseWheel;
                        _base._contrastSlider.Scroll -= _base.ContrastSlider_Scroll;
                        _base._contrastSlider = null;
                    }

                    if (value != null)
                    {
                        _base._contrastSlider = value;

                        _base._contrastSlider.Minimum = -100;
                        _base._contrastSlider.Maximum = 100;
                        _base._contrastSlider.TickFrequency = 10;
                        _base._contrastSlider.SmallChange = 1;
                        _base._contrastSlider.LargeChange = 10;

                        _base._contrastSlider.Value = (int)(_base._contrast * 100);

                        _base._contrastSlider.Scroll += _base.ContrastSlider_Scroll;
                        _base._contrastSlider.MouseWheel += _base.ContrastSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image hue slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Hue
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._hueSlider;
            }
            set
            {
                if (value != _base._hueSlider)
                {
                    if (_base._hueSlider != null)
                    {
                        _base._hueSlider.MouseWheel -= _base.HueSlider_MouseWheel;
                        _base._hueSlider.Scroll -= _base.HueSlider_Scroll;
                        _base._hueSlider = null;
                    }

                    if (value != null)
                    {
                        _base._hueSlider = value;

                        _base._hueSlider.Minimum = -100;
                        _base._hueSlider.Maximum = 100;
                        _base._hueSlider.TickFrequency = 10;
                        _base._hueSlider.SmallChange = 1;
                        _base._hueSlider.LargeChange = 10;

                        _base._hueSlider.Value = (int)(_base._hue * 100);

                        _base._hueSlider.Scroll += _base.HueSlider_Scroll;
                        _base._hueSlider.MouseWheel += _base.HueSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the video image saturation slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar Saturation
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._saturationSlider;
            }
            set
            {
                if (value != _base._saturationSlider)
                {
                    if (_base._saturationSlider != null)
                    {
                        _base._saturationSlider.MouseWheel -= _base.SaturationSlider_MouseWheel;
                        _base._saturationSlider.Scroll -= _base.SaturationSlider_Scroll;
                        _base._saturationSlider = null;
                    }

                    if (value != null)
                    {
                        _base._saturationSlider = value;

                        _base._saturationSlider.Minimum = -100;
                        _base._saturationSlider.Maximum = 100;
                        _base._saturationSlider.TickFrequency = 10;
                        _base._saturationSlider.SmallChange = 1;
                        _base._saturationSlider.LargeChange = 10;

                        _base._saturationSlider.Value = (int)(_base._saturation * 100);

                        _base._saturationSlider.Scroll += _base.SaturationSlider_Scroll;
                        _base._saturationSlider.MouseWheel += _base.SaturationSlider_MouseWheel;
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
        }
    }
}