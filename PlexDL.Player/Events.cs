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

        #endregion

        internal Events(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Occurs when media playback has ended.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEnded
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEnded += value;
            }
            remove { _base._mediaEnded -= value; }
        }

        /// <summary>
        /// Occurs when media playback has ended, just before the Player.Events.MediaEnded event occurs.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEndedNotice
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaEndedNotice += value;
            }
            remove { _base._mediaEndedNotice -= value; }
        }

        /// <summary>
        /// Occurs when the player's media repeat setting has changed.
        /// </summary>
        public event EventHandler MediaRepeatChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeatChanged += value;
            }
            remove { _base._mediaRepeatChanged -= value; }
        }

        /// <summary>
        /// Occurs when media playback has ended and is repeated.
        /// </summary>
        public event EventHandler MediaRepeated
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRepeated += value;
            }
            remove { _base._mediaRepeated -= value; }
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
            remove { _base._mediaStarted -= value; }
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
            remove { _base._mediaPausedChanged -= value; }
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
            remove { _base._mediaPositionChanged -= value; }
        }

        /// <summary>
        /// Occurs when the start or stop time of the playing media has changed.
        /// </summary>
        public event EventHandler MediaStartStopTimeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaStartStopTimeChanged += value;
            }
            remove { _base._mediaStartStopTimeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's display window has changed.
        /// </summary>
        public event EventHandler MediaDisplayChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayChanged += value;
            }
            remove { _base._mediaDisplayChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's displaymode has changed.
        /// </summary>
        public event EventHandler MediaDisplayModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayModeChanged += value;
            }
            remove { _base._mediaDisplayModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the shape of the player's display window has changed.
        /// </summary>
        public event EventHandler MediaDisplayShapeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayShapeChanged += value;
            }
            remove { _base._mediaDisplayShapeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's full screen mode has changed.
        /// </summary>
        public event EventHandler MediaFullScreenChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenChanged += value;
            }
            remove { _base._mediaFullScreenChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's full screen display mode has changed.
        /// </summary>
        public event EventHandler MediaFullScreenModeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaFullScreenModeChanged += value;
            }
            remove { _base._mediaFullScreenModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio volume has changed.
        /// </summary>
        public event EventHandler MediaAudioVolumeChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioVolumeChanged += value;
            }
            remove { _base._mediaAudioVolumeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio balance has changed.
        /// </summary>
        public event EventHandler MediaAudioBalanceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioBalanceChanged += value;
            }
            remove { _base._mediaAudioBalanceChanged -= value; }
        }

        ///// <summary>
        ///// Occurs when the player's audio enabled setting has changed.
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
        /// Occurs when the player's audio mute setting has changed.
        /// </summary>
        public event EventHandler MediaAudioMuteChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioMuteChanged += value;
            }
            remove { _base._mediaAudioMuteChanged -= value; }
        }

        /// <summary>
        /// Occurs when the videobounds of the video on the player's display window have changed (by using VideoBounds, VideoZoom or VideoMove options).
        /// </summary>
        public event EventHandler MediaVideoBoundsChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaVideoBoundsChanged += value;
            }
            remove { _base._mediaVideoBoundsChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's playback speed setting has changed.
        /// </summary>
        public event EventHandler MediaSpeedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaSpeedChanged += value;
            }
            remove { _base._mediaSpeedChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's display overlay has changed.
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
        /// Occurs when the player's display overlay mode setting has changed.
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
        /// Occurs when the player's display overlay hold setting has changed.
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
        /// Occurs when the active state of the player's display overlay has changed.
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
        /// Occurs when a display clone is added or removed from the player.
        /// </summary>
        public event EventHandler MediaDisplayClonesChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaDisplayClonesChanged += value;
            }
            remove { _base._mediaDisplayClonesChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio output peak level has changed. Device changes are handled automatically by the player.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaPeakLevelChanged
        {
            add
            {
                if (_base.PeakMeter_Open(_base._audioDevice, false))
                {
                    if (_base._outputLevelArgs == null) _base._outputLevelArgs = new PeakLevelEventArgs();
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
                    _base._outputLevelArgs._channelCount = _base.pm_PeakMeterChannelCount;
                    _base._outputLevelArgs._masterPeakValue = -1;
                    _base._outputLevelArgs._channelsValues = _base.pm_PeakMeterValuesStop;
                    value(this, _base._outputLevelArgs);

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
        /// Occurs when the player's audio input peak level has changed.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaInputLevelChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._micDevice == null)
                {
                    //_base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
                    _base.pm_InputMeterPending = true;
                }
                else if (_base.InputMeter_Open(_base._micDevice, false))
                {
                    if (_base._inputLevelArgs == null) _base._inputLevelArgs = new PeakLevelEventArgs();
                    _base._mediaInputLevelChanged += value;
                    _base.pm_InputMeterPending = false;
                    _base.StartMainTimerCheck();
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                if (_base.pm_HasInputMeter)
                {
                    _base._inputLevelArgs._channelCount = _base.pm_InputMeterChannelCount;
                    _base._inputLevelArgs._masterPeakValue = -1;
                    _base._inputLevelArgs._channelsValues = _base.pm_InputMeterValuesStop;
                    value(this, _base._inputLevelArgs);

                    _base._mediaInputLevelChanged -= value;
                    if (_base._mediaInputLevelChanged == null)
                    {
                        _base.InputMeter_Close();
                        _base.StopMainTimerCheck();
                    }
                }
                _base.pm_InputMeterPending = false;
            }
        }

        /// <summary>
        /// Occurs when the player's current subtitle has changed.
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
        /// Occurs when the player's audio output device has changed. The player handles all changes to the audio output devices. You can use this event to update the application's interface.
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
        /// Occurs when the audio output devices of the system have changed. The player handles all changes to the system audio output devices. You can use this event to update the application's interface.
        /// </summary>
        public event EventHandler<SystemAudioDevicesEventArgs> MediaSystemAudioDevicesChanged
        {
            add
            {
                if (Player.AudioDevicesClientOpen())
                {
                    if (_base._mediaSystemAudioDevicesChanged == null) Player._masterSystemAudioDevicesChanged += _base.SystemAudioDevicesChanged;
                    _base._mediaSystemAudioDevicesChanged += value;
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.ERROR_NOT_READY;
            }
            remove
            {
                _base._mediaSystemAudioDevicesChanged -= value;
                if (_base._mediaSystemAudioDevicesChanged == null)
                {
                    Player._masterSystemAudioDevicesChanged -= _base.SystemAudioDevicesChanged;
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
        /// Occurs when the player's audio input device has changed.
        /// </summary>
        public event EventHandler MediaAudioInputDeviceChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaAudioInputDeviceChanged += value;
            }
            remove { _base._mediaAudioInputDeviceChanged -= value; }
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
            remove { _base._mediaWebcamFormatChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's video recorder starts recording.
        /// </summary>
        public event EventHandler MediaRecorderStarted
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRecorderStarted += value;
            }
            remove { _base._mediaRecorderStarted -= value; }
        }

        /// <summary>
        /// Occurs when the player's video recorder stops recording.
        /// </summary>
        public event EventHandler MediaRecorderStopped
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRecorderStopped += value;
            }
            remove { _base._mediaRecorderStopped -= value; }
        }

        /// <summary>
        /// Occurs when the player's video recorder pause mode is activated (recording is paused) or deactivated (recording is resumed).
        /// </summary>
        public event EventHandler MediaRecorderPausedChanged
        {
            add
            {
                _base._lastError = Player.NO_ERROR;
                _base._mediaRecorderPausedChanged += value;
            }
            remove { _base._mediaRecorderPausedChanged -= value; }
        }
    }
}