﻿using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Position methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Position : HideObjectMembers
    {
        #region Fields (Position Class)

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

        internal Position(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media,
        /// <br/>measured from the (natural) beginning of the media.
        /// </summary>
        public TimeSpan FromBegin
        {
            get
            {
                if (_base._playing)
                {
                    _base._lastError = NO_ERROR;
                    if (!_base._fileMode) return TimeSpan.FromTicks(_base.PositionX - _base._deviceStart);
                    else return TimeSpan.FromTicks(_base.PositionX);
                }
                else
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return TimeSpan.Zero;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media,
        /// <br/>measured from the (natural) end of the media.
        /// </summary>
        public TimeSpan ToEnd
        {
            get
            {
                long toEnd = 0;

                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    toEnd = _base._mediaLength - _base.PositionX;
                    if (toEnd < 0) toEnd = 0;
                    _base._lastError = NO_ERROR;
                }
                return TimeSpan.FromTicks(toEnd);
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._mediaLength - value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media,
        /// <br/>measured from its (adjustable) start time.
        /// <br/>See also: Player.Media.StartTime.
        /// </summary>
        public TimeSpan FromStart
        {
            get
            {
                if (_base._playing)
                {
                    _base._lastError = NO_ERROR;
                    if (!_base._fileMode) return TimeSpan.FromTicks(_base.PositionX - _base._deviceStart);
                    else return TimeSpan.FromTicks(_base.PositionX - _base._startTime);
                }
                else
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return TimeSpan.Zero;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._startTime + value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media,
        /// <br/>measured from its (adjustable) stop time.
        /// <br/>See also: Player.Media.StopTime.
        /// </summary>
        public TimeSpan ToStop
        {
            get
            {
                long toEnd = 0;

                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (_base._stopTime == 0)
                    {
                        toEnd = _base._mediaLength - _base.PositionX;
                        if (toEnd < 0) toEnd = 0;
                    }
                    else toEnd = _base._stopTime - _base.PositionX;
                    _base._lastError = NO_ERROR;
                }
                return TimeSpan.FromTicks(toEnd);
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else _base.SetPosition(_base._stopTime - value.Ticks);
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media
        /// <br/>relative to its (natural) begin and end time.
        /// <br/>Values from 0.0 to 1.0.
        /// <br/>See also: Player.Position.Progress.
        /// </summary>
        public float Track
        {
            get
            {
                if (!_base._fileMode || !_base._playing || _base._mediaLength <= 0)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return 0;
                }
                else
                {
                    _base._lastError = NO_ERROR;
                    return (float)_base.PositionX / _base._mediaLength;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (value >= 0 && value < 1)
                    {
                        _base.SetPosition((long)(value * _base._mediaLength));
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
            }
        }

        /// <summary>
        /// Gets or sets the playback position of the playing media
        /// <br/>relative to its (adjustable) start and stop time.
        /// <br/>Values from 0.0 to 1.0.
        /// <br/>See also: Player.Position.Track.
        /// </summary>
        public float Progress
        {
            get
            {
                if (!_base._fileMode || !_base._playing)
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return 0;
                }
                else
                {
                    _base._lastError = NO_ERROR;

                    long pos = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                    if (pos == 0 || pos <= _base._startTime) return 0;

                    float pos2 = (_base.PositionX - _base._startTime) / (pos - _base._startTime);
                    if (pos2 < 0) return 0;
                    return pos2 > 1 ? 1 : pos2;
                }
            }
            set
            {
                if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    if (value >= 0 && value < 1)
                    {
                        _base._lastError = NO_ERROR;

                        long pos = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                        if (pos <= _base._startTime) return;

                        _base.SetPosition((long)(value * (pos - _base._startTime)) + _base._startTime);
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
            }
        }

        /// <summary>
        /// Rewinds the playback position of the playing media to its (adjustable) start time.
        /// <br/>See also: Player.Media.StartTime.
        /// </summary>
        public int Rewind()
        {
            if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            else _base.SetPosition(_base._startTime);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Changes the playback position of the playing media in any direction by the given amount of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds to skip.
        /// <br/>Negative values move the playback position backwards.</param>
        public int Skip(int seconds)
        {
            if (!_base._fileMode || !_base._playing) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            else _base.SetPosition(_base.PositionX + (seconds * Player.ONE_SECOND_TICKS));
            return (int)_base._lastError;
        }

        /// <summary>
        /// Changes the playback position of the playing media in any direction by the given amount of video frames.
        /// <br/>The result can differ from the specified value.
        /// <br/>See also: Player.Position.StepEnd.
        /// </summary>
        /// <param name="frames">The number of frames to step.
        /// <br/>Negative values move the playback position backwards.</param>
        public int Step(int frames)
        {
            if (!_base._fileMode || !_base._playing)
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return (int)_base._lastError;
            }
            else return _base.Step(frames);
        }

        /// <summary>
        /// Gets or sets a value that indicates the margin at the end of media files that is used to avoid
        /// <br/>stepping past the end of media files using the Player.Position.Step method.
        /// <br/>Values from 10 to 1000 milliseconds (default: 200).
        /// </summary>
        public int StepEOFMargin
        {
            get
            {
                _base._lastError = NO_ERROR;
                return (int)(_base._stepMargin * Player.TICKS_TO_MS);
            }
            set
            {
                if (value < 10 || value > 1000)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    _base._stepMargin = value * Player.MS_TO_TICKS;
                    _base._lastError = NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Restores the player's video image after using the Player.Position.Step method
        /// <br/>while the player is paused (step-end playback recovery).
        /// </summary>
        public int StepEnd()
        {
            if (_base._stepMode && _base._hasVideo && _base._paused) _base.AV_UpdateTopology();

            _base._lastError = NO_ERROR;
            return NO_ERROR;
        }
    }
}