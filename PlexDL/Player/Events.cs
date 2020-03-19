using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Events of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Events : HideObjectMembers
    {
        #region Fields (Events Class)

        private Player _base;

        #endregion Fields (Events Class)

        internal Events(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Occurs when media has finished playing.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEnded
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEnded += value;
            }
            remove => _base._mediaEnded -= value;
        }

        /// <summary>
        /// Occurs when media has finished playing, just before the Player.Events.MediaEnded event occurs.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEndedNotice
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEndedNotice += value;
            }
            remove => _base._mediaEndedNotice -= value;
        }

        /// <summary>
        /// Occurs when the repeat setting of the player has changed.
        /// </summary>
        public event EventHandler MediaRepeatChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeatChanged += value;
            }
            remove => _base._mediaRepeatChanged -= value;
        }

        /// <summary>
        /// Occurs when media has finished playing and is repeated.
        /// </summary>
        public event EventHandler MediaRepeated
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeated += value;
            }
            remove => _base._mediaRepeated -= value;
        }

        /// <summary>
        /// Occurs when media starts playing.
        /// </summary>
        public event EventHandler MediaStarted
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaStarted += value;
            }
            remove => _base._mediaStarted -= value;
        }

        /// <summary>
        /// Occurs when the player's pause mode is activated (playing media is paused) or deactivated (paused media is resumed).
        /// </summary>
        public event EventHandler MediaPausedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaPausedChanged += value;
            }
            remove => _base._mediaPausedChanged -= value;
        }

        /// <summary>
        /// Occurs when the playback position of the playing media has changed.
        /// </summary>
        public event EventHandler<PositionEventArgs> MediaPositionChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaPositionChanged += value;
            }
            remove => _base._mediaPositionChanged -= value;
        }

        /// <summary>
        /// Occurs when the start- and/or endtime of the playing media has changed.
        /// </summary>
        public event EventHandler MediaStartStopTimeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaStartStopTimeChanged += value;
            }
            remove => _base._mediaStartStopTimeChanged -= value;
        }

        /// <summary>
        /// Occurs when the display window of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayChanged += value;
            }
            remove => _base._mediaDisplayChanged -= value;
        }

        /// <summary>
        /// Occurs when the displaymode of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayModeChanged += value;
            }
            remove => _base._mediaDisplayModeChanged -= value;
        }

        /// <summary>
        /// Occurs when the shape of the display window of the player has changed.
        /// </summary>
        public event EventHandler MediaDisplayShapeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayShapeChanged += value;
            }
            remove => _base._mediaDisplayShapeChanged -= value;
        }

        /// <summary>
        /// Occurs when the fullscreen setting of the player has changed.
        /// </summary>
        public event EventHandler MediaFullScreenChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenChanged += value;
            }
            remove => _base._mediaFullScreenChanged -= value;
        }

        /// <summary>
        /// Occurs when the fullscreen mode of the player has changed.
        /// </summary>
        public event EventHandler MediaFullScreenModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenModeChanged += value;
            }
            remove => _base._mediaFullScreenModeChanged -= value;
        }

        /// <summary>
        /// Occurs when the audio volume of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioVolumeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioVolumeChanged += value;
            }
            remove => _base._mediaAudioVolumeChanged -= value;
        }

        /// <summary>
        /// Occurs when the audio balance of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioBalanceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioBalanceChanged += value;
            }
            remove => _base._mediaAudioBalanceChanged -= value;
        }

        ///// <summary>
        ///// Occurs when the audio enabled setting of the player has changed.
        ///// </summary>
        //public event EventHandler MediaAudioEnabledChanged
        //{
        //    add
        //    {
        //        _base._lastError = Player.NO_ERROR;
        //        _base._mediaAudioMuteChanged += value;
        //    }
        //    remove { _base._mediaAudioMuteChanged -= value; }
        //}

        /// <summary>
        /// Occurs when the audio mute setting of the player has changed.
        /// </summary>
        public event EventHandler MediaAudioMuteChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioMuteChanged += value;
            }
            remove => _base._mediaAudioMuteChanged -= value;
        }

        /// <summary>
        /// Occurs when the videobounds of the video on the display window of the player have changed (by using VideoBounds, VideoZoom or VideoMove options).
        /// </summary>
        public event EventHandler MediaVideoBoundsChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoBoundsChanged += value;
            }
            remove => _base._mediaVideoBoundsChanged -= value;
        }

        /// <summary>
        /// Occurs when the playback speed setting of the player has changed.
        /// </summary>
        public event EventHandler MediaSpeedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaSpeedChanged += value;
            }
            remove => _base._mediaSpeedChanged -= value;
        }

        /// <summary>
        /// Occurs when the display overlay of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayChanged += value;
            }
            remove { _base._mediaOverlayChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display overlay mode setting of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayModeChanged += value;
            }
            remove { _base._mediaOverlayModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the display overlay hold setting of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayHoldChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayHoldChanged += value;
            }
            remove { _base._mediaOverlayHoldChanged -= value; }
        }

        /// <summary>
        /// Occurs when the active state of the display overlay of the player has changed.
        /// </summary>
        public event EventHandler MediaOverlayActiveChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaOverlayActiveChanged += value;
            }
            remove { _base._mediaOverlayActiveChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio output peak level of the player has changed. Device changes are handled automatically by the player.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaPeakLevelChanged
        {
            add
            {
                if (_base.PeakMeter_Open(_base._audioDevice, false))
                {
                    if (_base._peakLevelArgs == null) _base._peakLevelArgs = new PeakLevelEventArgs();
                    _base._mediaPeakLevelChanged += value;
                    _base._lastError = Player.NO_ERROR;
                    _base.StartMainTimerCheck();
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                if (_base.pm_HasPeakMeter)
                {
                    _base._peakLevelArgs._channelCount = _base.pm_PeakMeterChannelCount;
                    _base._peakLevelArgs._masterPeakValue = -1;
                    _base._peakLevelArgs._channelsValues = _base.pm_PeakMeterValuesStop;
                    value(this, _base._peakLevelArgs);

                    _base._mediaPeakLevelChanged -= value;
                    if (_base._mediaPeakLevelChanged == null)
                    {
                        _base.PeakMeter_Close();
                        _base.StopMainTimerCheck();
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the current subtitle of the player has changed.
        /// </summary>
        public event EventHandler<SubtitleEventArgs> MediaSubtitleChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;

                if (_base.st_SubtitleChangedArgs == null) _base.st_SubtitleChangedArgs = new SubtitleEventArgs();
                _base._mediaSubtitleChanged += value;

                if (!_base.st_SubtitlesEnabled)
                {
                    _base.st_SubtitlesEnabled = true;
                    if (!_base.st_HasSubtitles && _base._playing)
                    {
                        _base.Subtitles_Start(true);
                        _base.StartMainTimerCheck();
                    }
                }
            }
            remove
            {
                _base._mediaSubtitleChanged -= value;
                if (_base._mediaSubtitleChanged == null)
                {
                    if (_base.st_HasSubtitles)
                    {
                        _base.st_SubtitleOn = false; // prevent 'no title' event firing
                        _base.Subtitles_Stop();
                    }

                    _base.st_SubtitlesEnabled = false;
                    _base.StopMainTimerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when the active video track of the playing media has changed.
        /// </summary>
        public event EventHandler MediaVideoTrackChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoTrackChanged += value;
            }
            remove { _base._mediaVideoTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the active audio track of the playing media has changed.
        /// </summary>
        public event EventHandler MediaAudioTrackChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioTrackChanged += value;
            }
            remove { _base._mediaAudioTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio output device of the player is set to a different device. The player handles all changes to the audio output devices. You can use this event to update the application interface.
        /// </summary>
        public event EventHandler MediaAudioDeviceChanged
        {
            add
            {
                _base._mediaAudioDeviceChanged += value;
                _base._lastError = Player.NO_ERROR;
                _base.StartSystemDevicesChangedHandlerCheck();
            }
            remove
            {
                if (_base._mediaAudioDeviceChanged != null)
                {
                    _base._mediaAudioDeviceChanged -= value;
                    _base.StopSystemDevicesChangedHandlerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when the audio output devices of the system have changed. The player handles all changes to the system audio output devices. You can use this event to update the application interface.
        /// </summary>
        public event EventHandler<SystemAudioDevicesEventArgs> MediaSystemAudioDevicesChanged
        {
            add
            {
                if (Player.AudioDevicesClientOpen())
                {
                    Player._mediaSystemAudioDevicesChanged += value;
                    _base._mediaLocalAudioDevicesChanged += value; // used to unsubscribe
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                Player._mediaSystemAudioDevicesChanged -= value;
                _base._mediaLocalAudioDevicesChanged -= value; // used to unsubscribe
                if (Player._mediaSystemAudioDevicesChanged == null)
                {
                    //Player.AudioDevicesClientClose();
                    _base.StopSystemDevicesChangedHandlerCheck();
                }
            }
        }

        /// <summary>
        /// Occurs when a video image color attribute (for example, brightness) of the player has changed.
        /// </summary>
        public event EventHandler<VideoColorEventArgs> MediaVideoColorChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoColorChanged += value;
            }
            remove { _base._mediaVideoColorChanged -= value; }
        }

        /// <summary>
        /// Occurs when the audio input device of the player is set to a different device.
        /// </summary>
        public event EventHandler MediaAudioInputDeviceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioInputDeviceChanged += value;
            }
            remove => _base._mediaAudioInputDeviceChanged -= value;
        }

        /// <summary>
        /// Occurs when the format of the playing webcam has changed.
        /// </summary>
        public event EventHandler MediaWebcamFormatChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaWebcamFormatChanged += value;
            }
            remove => _base._mediaWebcamFormatChanged -= value;
        }
    }
}