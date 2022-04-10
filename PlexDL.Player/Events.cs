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

        private const int   NO_ERROR = 0;

        private Player      _base;

        #endregion

        internal Events(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Occurs when media playback has ended, just after the Player.Events.MediaEndedNotice event occurs.
        /// </summary>
        public event EventHandler<EndedEventArgs> MediaEnded
        {
            add
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaEndedNotice += value;
            }
            remove { _base._mediaEndedNotice -= value; }
        }

        /// <summary>
        /// Occurs when media playback has reached the end of the media and is paused.
        /// <br/>Pausing at the end of media is enabled by subscribing to this event.
        /// </summary>
        public event EventHandler<EndedPausedEventArgs> MediaEndedPaused
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaEndedPaused += value;
                if (_base._mediaEndedPaused != null) _base._endPauseMode = true;
            }
            remove
            {
                _base._mediaEndedPaused -= value;
                if (_base._mediaEndedPaused == null) _base._endPauseMode = false;
            }
        }

        /// <summary>
        /// Occurs when the player's media repeat setting has changed.
        /// </summary>
        public event EventHandler MediaRepeatChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaRepeated += value;
            }
            remove { _base._mediaRepeated -= value; }
        }

        /// <summary>
        /// Occurs when the player's chapter repeat setting has changed.
        /// </summary>
        public event EventHandler MediaChapterRepeatChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaChapterRepeatChanged += value;
            }
            remove { _base._mediaChapterRepeatChanged -= value; }
        }

        /// <summary>
        /// Occurs when a chapter playback has ended and is repeated.
        /// <br/>Applies only to chapters played using the Player.Play method.
        /// </summary>
        public event EventHandler MediaChapterRepeated
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaChapterRepeated += value;
            }
            remove { _base._mediaChapterRepeated -= value; }
        }

        /// <summary>
        /// Occurs when media starts playing, just before the Player.Events.MediaStartedNotice event occurs.
        /// </summary>
        public event EventHandler MediaStarted
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaStarted += value;
            }
            remove { _base._mediaStarted -= value; }
        }

        /// <summary>
        /// Occurs when media starts playing, just after the Player.Events.MediaStarted event occurs.
        /// </summary>
        public event EventHandler MediaStartedNotice
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaStartedNotice += value;
            }
            remove { _base._mediaStartedNotice -= value; }
        }

        /// <summary>
        /// Occurs when a new chapter has started playing.
        /// <br/>Applies only to chapters played using the Player.Play method.
        /// </summary>
        public event EventHandler<ChapterStartedEventArgs> MediaChapterStarted
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaChapterStarted += value;
            }
            remove { _base._mediaChapterStarted -= value; }
        }

        /// <summary>
        /// Occurs when the player's pause mode is activated (playing media is paused) or deactivated (paused media is resumed).
        /// </summary>
        public event EventHandler MediaPausedChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaFullScreenModeChanged += value;
            }
            remove { _base._mediaFullScreenModeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio volume has changed.
        /// </summary>
        public event EventHandler<AudioEventArgs> MediaAudioVolumeChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaAudioVolumeChanged += value;
            }
            remove { _base._mediaAudioVolumeChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio balance has changed.
        /// </summary>
        public event EventHandler<AudioEventArgs> MediaAudioBalanceChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
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
        //        _base._lastError = NO_ERROR;
        //        _base._mediaAudioMuteChanged += value;
        //    }
        //    remove { _base._mediaAudioMuteChanged -= value; }
        //}

        /// <summary>
        /// Occurs when the player's audio mute setting has changed.
        /// </summary>
        public event EventHandler<AudioEventArgs> MediaAudioMuteChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaAudioMuteChanged += value;
            }
            remove { _base._mediaAudioMuteChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio multi track setting has changed.
        /// </summary>
        public event EventHandler MediaAudioMultiTrackChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaAudioMultiTrackChanged += value;
            }
            remove { _base._mediaAudioMultiTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the videobounds of the video on the player's display window have changed
        /// <br/>by using the player's VideoBounds, Video Zoom, etc. options.
        /// </summary>
        public event EventHandler MediaVideoBoundsChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaVideoBoundsChanged += value;
            }
            remove { _base._mediaVideoBoundsChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's video aspect ratio has changed.
        /// </summary>
        public event EventHandler MediaVideoAspectRatioChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaVideoAspectRatioChanged += value;
            }
            remove { _base._mediaVideoAspectRatioChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's video View3D setting has changed.
        /// </summary>
        public event EventHandler MediaVideo3DViewChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaVideoView3DChanged += value;
            }
            remove { _base._mediaVideoView3DChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's video crop setting has changed (by using the Video Crop option).
        /// </summary>
        public event EventHandler MediaVideoCropChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaVideoCropChanged += value;
            }
            remove { _base._mediaVideoCropChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's video rotation setting has changed (by using the Video Rotation option).
        /// </summary>
        public event EventHandler MediaVideoRotationChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaVideoRotationChanged += value;
            }
            remove { _base._mediaVideoRotationChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's playback speed setting has changed.
        /// </summary>
        public event EventHandler MediaSpeedChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaDisplayClonesChanged += value;
            }
            remove { _base._mediaDisplayClonesChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio output peak level has changed.
        /// <br/>Audio output peak levels are enabled by subscribing to this event.
        /// <br/>The player handles all changes to the audio output devices.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaAudioOutputLevelChanged
        {
            add
            {
                if (_base.PeakMeter_Open(_base._audioDevice, false))
                {
                    if (_base._outputLevelArgs == null) _base._outputLevelArgs = new PeakLevelEventArgs();
                    _base._mediaAudioOutputLevelChanged += value;
                    _base._lastError = NO_ERROR;
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

                    _base._mediaAudioOutputLevelChanged -= value;
                    if (_base._mediaAudioOutputLevelChanged == null)
                    {
                        _base.PeakMeter_Close();
                        _base.StopMainTimerCheck();
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the player's audio input peak level has changed.
        /// <br/>Audio input peak levels are enabled by subscribing to this event.
        /// </summary>
        public event EventHandler<PeakLevelEventArgs> MediaAudioInputLevelChanged
        {
            add
            {
                _base._lastError = NO_ERROR;

                if (_base._inputLevelArgs == null) _base._inputLevelArgs = new PeakLevelEventArgs();
                _base._mediaAudioInputLevelChanged += value;

                if (_base._micDevice == null)
                {
                    _base.pm_InputMeterPending = true;
                }
                else if (_base.InputMeter_Open(_base._micDevice, false))
                {
                    //_base._mediaAudioInputLevelChanged += value;
                    _base.pm_InputMeterPending = false;
                    _base.StartMainTimerCheck();
                }
            }
            remove
            {
                _base._mediaAudioInputLevelChanged -= value;
                if (_base._mediaAudioInputLevelChanged == null)
                {
                    if (_base.pm_HasInputMeter)
                    {
                        _base.InputMeter_Close();
                        _base.StopMainTimerCheck();
                    }
                    _base.pm_InputMeterPending = false;
                }
            }
        }

        /// <summary>
        /// Occurs when the player's current subtitle has changed.
        /// <br/>Subtitles are enabled by subscribing to this event.
        /// </summary>
        public event EventHandler<SubtitleEventArgs> MediaSubtitleChanged
        {
            add
            {
                _base._lastError = NO_ERROR;

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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaAudioTrackChanged += value;
            }
            remove { _base._mediaAudioTrackChanged -= value; }
        }

        /// <summary>
        /// Occurs when the player's audio output device has changed.
        /// <br/>The player handles all changes to the player's audio output devices.
        /// <br/>This event can be used to update the user interface of an application.
        /// <br/>See also: Player.Events.MediaAudioTrackDeviceChanged.
        /// </summary>
        public event EventHandler MediaAudioDeviceChanged
        {
            add
            {
                _base._mediaAudioDeviceChanged += value;
                _base._lastError = NO_ERROR;
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
        /// Occurs when the audio output device of an audio track in audio multi-track mode has changed.
        /// <br/>The audio devices of the audio tracks in audio multi-track mode are independent of the audio device of the player.
        /// <br/>See also: Player.Events.MediaAudioDeviceChanged.
        /// </summary>
        public event EventHandler<AudioTrackDeviceEventArgs> MediaAudioTrackDeviceChanged
        {
            add
            {
                _base._mediaAudioTrackDeviceChanged += value;
                _base._lastError = NO_ERROR;
            }
            remove
            {
                if (_base._mediaAudioTrackDeviceChanged != null)
                {
                    _base._mediaAudioTrackDeviceChanged -= value;
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Occurs when the audio output devices of the system have changed.
        /// <br/>The player handles all changes to the system's audio output devices.
        /// <br/>This event can be used to update the user interface of an application.
        /// </summary>
        public event EventHandler<SystemAudioDevicesEventArgs> MediaSystemAudioDevicesChanged
        {
            add
            {
                if (Player.AudioDevicesClientOpen())
                {
                    if (_base._mediaSystemAudioDevicesChanged == null) Player._masterSystemAudioDevicesChanged += _base.SystemAudioDevicesChanged;
                    _base._mediaSystemAudioDevicesChanged += value;
                    _base._lastError = NO_ERROR;
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
        /// Occurs when a video color attribute (for example, brightness) of the player has changed.
        /// </summary>
        public event EventHandler<VideoColorEventArgs> MediaVideoColorChanged
        {
            add
            {
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
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
                _base._lastError = NO_ERROR;
                _base._mediaWebcamFormatChanged += value;
            }
            remove { _base._mediaWebcamFormatChanged -= value; }
        }

        ///// <summary>
        ///// Occurs when the player's video recorder starts recording.
        ///// </summary>
        //public event EventHandler MediaRecorderStarted
        //{
        //    add
        //    {
        //        _base._lastError = NO_ERROR;
        //        _base._mediaRecorderStarted += value;
        //    }
        //    remove { _base._mediaRecorderStarted -= value; }
        //}

        ///// <summary>
        ///// Occurs when the player's video recorder stops recording.
        ///// </summary>
        //public event EventHandler MediaRecorderStopped
        //{
        //    add
        //    {
        //        _base._lastError = NO_ERROR;
        //        _base._mediaRecorderStopped += value;
        //    }
        //    remove { _base._mediaRecorderStopped -= value; }
        //}

        ///// <summary>
        ///// Occurs when the player's video recorder pause mode is activated (recording is paused) or deactivated (recording is resumed).
        ///// </summary>
        //public event EventHandler MediaRecorderPausedChanged
        //{
        //    add
        //    {
        //        _base._lastError = NO_ERROR;
        //        _base._mediaRecorderPausedChanged += value;
        //    }
        //    remove { _base._mediaRecorderPausedChanged -= value; }
        //}

        /// <summary>
        /// Occurs when the player's motion detection has detected motion or (optionally with Player.MotionDetection.NoMotionNotification)
        /// <br/>when a certain period of time has passed without motion being detected.
        /// <br/>The player's video motion detection is enabled by subscribing to this event and started with the Player.MotionDetection.Start method.
        /// </summary>
        public event EventHandler<MotionEventArgs> MediaVideoMotionDetected
        {
            add
            {
                _base._mediaMotionDetected += value;
                if (!_base._hasMotionDetection)
                {
                    if (_base._motionEventArgs == null) _base._motionEventArgs = new MotionEventArgs();
                    _base._hasMotionDetection = true;
                }
                _base._lastError = NO_ERROR;
            }
            remove
            {
                _base._mediaMotionDetected -= value;
                if (_base._mediaMotionDetected == null && _base._hasMotionDetection)
                {
                    _base.AV_StopMotionTimer(true);
                }
            }
        }

        ///// <summary>
        ///// Occurs when the player's video motion detection has detected a matching image.
        ///// <br/>Intended for use with fixed image video motion detection.
        ///// <br/>The player's video motion detection is enabled by subscribing to this event.
        ///// </summary>
        //public event EventHandler MediaVideoMatchDetected
        //{
        //	add
        //	{
        //		_base._mediaMatchDetected += value;
        //		_base._hasMotionDetection = true;
        //		_base._lastError = NO_ERROR;
        //	}
        //	remove
        //	{
        //		_base._mediaMatchDetected -= value;
        //		if (_base._mediaMatchDetected == null && _base._mediaMotionDetected == null && _base._hasMotionDetection)
        //		{
        //			_base.AV_StopMotionTimer(true);
        //		}
        //	}
        //}

        /// <summary>
        /// Occurs when the player's device recorder has started recording.
        /// </summary>
        public event EventHandler MediaDeviceRecorderStarted
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaDeviceRecorderStarted += value;
            }
            remove { _base._mediaDeviceRecorderStarted -= value; }
        }

        /// <summary>
        /// Occurs when the player's device recorder has stopped recording.
        /// </summary>
        public event EventHandler MediaDeviceRecorderStopped
        {
            add
            {
                _base._lastError = NO_ERROR;
                _base._mediaDeviceRecorderStopped += value;
            }
            remove { _base._mediaDeviceRecorderStopped -= value; }
        }
    }
}