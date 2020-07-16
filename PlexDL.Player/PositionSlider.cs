using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Position Slider methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PositionSlider : HideObjectMembers
    {
        #region Fields (PositionSlider Class)

        private const int MAX_SCROLL_VALUE = 60000;
        private Player _base;

        #endregion

        internal PositionSlider(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the media playback position slider (trackbar) that is controlled by the player.
        /// </summary>
        public TrackBar TrackBar
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._positionSlider;
            }
            set
            {
                if (value != _base._positionSlider)
                {
                    if (_base._hasPositionSlider)
                    {
                        _base._timer.Stop();

                        _base._positionSlider.MouseDown -= _base.PositionSlider_MouseDown;
                        //_base._positionSlider.MouseUp -= _base.PositionSlider_MouseUp;
                        //_base._positionSlider.MouseMove -= _base.PositionSlider_MouseMove;
                        _base._positionSlider.Scroll -= _base.PositionSlider_Scroll;
                        _base._positionSlider.MouseWheel -= _base.PositionSlider_MouseWheel;

                        _base._positionSlider = null;
                        _base._hasPositionSlider = false;

                        _base._psTracking = false;
                        _base._psValue = 0;
                        _base._psBusy = false;
                        _base._psSkipped = false;

                        if (_base._psTimer != null)
                        {
                            _base._psTimer.Dispose();
                            _base._psTimer = null;
                        }
                    }

                    if (value != null)
                    {
                        _base._positionSlider = value;
                        _base._hasPositionSlider = true;

                        _base._psHorizontal = (_base._positionSlider.Orientation == Orientation.Horizontal);
                        _base._positionSlider.SmallChange = 0;
                        _base._positionSlider.LargeChange = 0;
                        _base._positionSlider.TickFrequency = 0;

                        SetPositionSliderMode(_base._psHandlesProgress);

                        // add events
                        _base._positionSlider.MouseDown += _base.PositionSlider_MouseDown;
                        //_base._positionSlider.MouseUp += _base.PositionSlider_MouseUp;
                        //_base._positionSlider.MouseMove += _base.PositionSlider_MouseMove;
                        _base._positionSlider.Scroll += _base.PositionSlider_Scroll;
                        _base._positionSlider.MouseWheel += _base.PositionSlider_MouseWheel;

                        if (!_base._playing) _base._positionSlider.Enabled = false;

                        _base._psTimer = new Timer();
                        _base._psTimer.Interval = 100;
                        _base._psTimer.Tick += _base.PositionSlider_TimerTick;
                    }
                    _base.StartMainTimerCheck();
                    _base._lastError = Player.NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Gets or sets the mode (track or progress) of the player's position slider (default: PositionSliderMode.Track).
        /// </summary>
        public PositionSliderMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psHandlesProgress ? PositionSliderMode.Progress : PositionSliderMode.Track;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                SetPositionSliderMode(value != PositionSliderMode.Track);
            }
        }

        private void SetPositionSliderMode(bool progressMode)
        {
            _base._psHandlesProgress = progressMode;
            if (_base._hasPositionSlider)
            {
                if (_base._psHandlesProgress)
                {
                    _base._positionSlider.Minimum = (int)(_base._startTime * Player.TICKS_TO_MS);
                    _base._positionSlider.Maximum = _base._stopTime == 0 ? (_base._mediaLength == 0 ? 10 : (int)(_base._mediaLength * Player.TICKS_TO_MS)) : (int)(_base._stopTime * Player.TICKS_TO_MS);

                    if (_base._playing)
                    {
                        int pos = (int)(_base.PositionX * Player.TICKS_TO_MS);
                        if (pos < _base._positionSlider.Minimum) _base._positionSlider.Value = _base._positionSlider.Minimum;
                        else if (pos > _base._positionSlider.Maximum) _base._positionSlider.Value = _base._positionSlider.Maximum;
                        else _base._positionSlider.Value = pos;
                    }
                }
                else
                {
                    _base._positionSlider.Minimum = 0;
                    _base._positionSlider.Maximum = _base._mediaLength == 0 ? 10 : (int)(_base._mediaLength * Player.TICKS_TO_MS);
                    if (_base._playing) _base._positionSlider.Value = (int)(_base.PositionX * Player.TICKS_TO_MS);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's display window is updated immediately when seeking with the player's position slider (default: false).
        /// </summary>
        public bool LiveUpdate
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psLiveSeek;
            }
            set
            {
                _base._psLiveSeek = value;
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds that the slider value changes when the scroll box is moved with the mouse wheel (default: 0 (not enabled)).
        /// </summary>
        public int MouseWheel
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psMouseWheel;
            }
            set
            {
                if (value <= 0) _base._psMouseWheel = 0;
                else if (value > MAX_SCROLL_VALUE) _base._psMouseWheel = MAX_SCROLL_VALUE;
                else _base._psMouseWheel = value;
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates when the audio output is muted when seeking with the player's position slider (default: SilentSeek.OnMoving).
        /// </summary>
        public SilentSeek SilentSeek
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._psSilentSeek;
            }
            set
            {
                _base._psSilentSeek = value;
                _base._lastError = Player.NO_ERROR;
            }
        }
    }
}