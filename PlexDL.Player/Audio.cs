using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Audio methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public sealed class Audio : HideObjectMembers
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        #region Fields (Audio Class)

        #region Constants

        private const int   NO_ERROR                = 0;

        private const int   VOLUME_FADE_INTERVAL    = 20;
        private const int   BALANCE_FADE_INTERVAL   = 10;

        private const int   FADE_INTERVAL_MINIMUM   = 1;
        private const int   FADE_INTERVAL_MAXIMUM   = 100;

        private const float FADE_STEP_VALUE         = 0.01f;

        #endregion


        private Player      _base;

        // Volume fading
        private bool        _volumeIsFading;
        private Timer       _volumeFadeTimer;
        private int         _volumeFadeInterval         = VOLUME_FADE_INTERVAL;
        private float       _volumeFadeStep;
        private float       _volumeFadeEndValue;
        private float       _volumeFadeCurrentValue;

        // Balance fading
        private bool        _balanceIsFading;
        private Timer       _balanceFadeTimer;
        private int         _balanceFadeInterval        = BALANCE_FADE_INTERVAL;
        private float       _balanceFadeStep;
        private float       _balanceFadeEndValue;
        private float       _balanceFadeCurrentValue;

        // Device volume fading
        private bool        _deviceIsFading;
        private Timer       _deviceFadeTimer;
        private int         _deviceFadeInterval         = VOLUME_FADE_INTERVAL;
        private float       _deviceFadeStep;
        private float       _deviceFadeEndValue;
        private float       _deviceFadeCurrentValue;

        #endregion

        internal Audio(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets a value indicating whether the playing media contains audio.
        /// </summary>
        public bool Present
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's audio output is enabled (default: true).
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's audio output is muted (default: false).
        /// <br/>See also: Player.AudioMultiTrack.SetMuteAll.
        /// </summary>
        public bool Mute
        {
            get
            {
                _base._lastError = NO_ERROR;
                return !_base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(!value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether audio tracks in subsequent media files are ignored by the player (default: false).
        /// <br/>The audio track information remains available. Allows to play video from media with unsupported audio formats.
        /// </summary>
        public bool Cut
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioCut;
            }
            set
            {
                _base._audioCut = value;
                if (value) _base._videoCut = false;
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's audio output is in mono (default: false).
        /// </summary>
        public bool Mono
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioMono;
            }
            set
            {
                if (value != _base._audioMono)
                {
                    _base._audioMono = value;
                    if (_base._playing && _base._hasAudio)
                    {
                        _base._audioMonoRestore = !value;
                        _base.AV_UpdateTopology();
                    }
                }
                _base._lastError = NO_ERROR;
            }
        }

        // Tracks:

        /// <summary>
        /// Gets or sets the active audio track of the playing media.
        /// <br/>See also: Player.Audio.TrackCount and Player.Audio.GetTracks.
        /// </summary>
        public int Track
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioTrackCurrent;
            }
            set { _base.AV_SetTrack(value, true); }
        }

        /// <summary>
        /// Gets a string with a detailed description of the active audio track of the playing media.
        /// <br/>See also: Player.Audio.GetTrackString.
        /// </summary>
        public string TrackString
        {
            get
            {
                if (_base._hasAudio)
                {
                    _base._lastError = NO_ERROR;
                    return _base.AV_GetAudioTracks()[_base._audioTrackCurrent].ToString();
                }

                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a string with a detailed description of the specified audio track of the playing media.
        /// <br/>See also: Player.Audio.TrackString.
        /// </summary>
        /// <param name="track">The track number of the audio track to get the string of.</param>
        public string GetTrackString(int track)
        {
            if (_base._hasAudio)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    _base._lastError = NO_ERROR;
                    return _base.AV_GetAudioTracks()[track].ToString();
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return string.Empty;
        }

        /// <summary>
        /// Gets the number of audio tracks in the playing media.
        /// <br/>See also: Player.Audio.Track and Player.Audio.GetTracks.
        /// </summary>
        public int TrackCount
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioTrackCount;
            }
        }

        /// <summary>
        /// Returns the audio tracks in the playing media.
        /// <br/>See also: Player.Audio.Track and Player.Audio.TrackCount.
        /// </summary>
        public AudioTrack[] GetTracks()
        {
            return _base.AV_GetAudioTracks();
        }

        // Channels:

        /// <summary>
        /// Gets the number of audio channels (e.g. 2 for stereo) in the active audio track of the playing media.
        /// </summary>
        public int ChannelCount
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (!_base._playing) return 0;
                return _base._mediaChannelCount;
            }
        }

        /// <summary>
        /// Gets or sets the volume of each individual audio output channel (up to 8 channels) of the player.
        /// <br/>Values from 0.0 (mute) to 1.0 (max) (default: 1.0).
        /// <br/>See also: Player.Audio.ChannelCount.
        /// </summary>
        public float[] ChannelVolumes
        {
            get
            {
                _base._lastError = NO_ERROR;

                float[] volumes = new float[Player.MAX_AUDIO_CHANNELS];
                for (int i = 0; i < Player.MAX_AUDIO_CHANNELS; i++)
                {
                    volumes[i] = _base._audioChannelsVolume[i];
                }
                return volumes;
            }
            set
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                if (value != null && value.Length > 0)
                {
                    if (_base._audioChannelsVolumeCopy == null) _base._audioChannelsVolumeCopy = new float[Player.MAX_AUDIO_CHANNELS];

                    int length = value.Length;
                    bool valid = true;
                    float newVolume = 0.0f;

                    for (int i = 0; i < Player.MAX_AUDIO_CHANNELS; i++)
                    {
                        if (i < length)
                        {
                            if (value[i] < 0.0f || value[i] > 1.0f)
                            {
                                valid = false;
                                break;
                            }
                            _base._audioChannelsVolumeCopy[i] = value[i];
                            if (i < 2 && value[i] > newVolume) newVolume = value[i];
                        }
                        else _base._audioChannelsVolumeCopy[i] = _base._audioChannelsVolume[i]; //  0.0f;
                    }

                    if (valid)
                    {
                        _base._lastError = NO_ERROR;

                        float newBalance;
                        if (value[0] >= value[1])
                        {
                            if (value[0] == 0.0f) newBalance = 0.0f;
                            else newBalance = (value[1] / value[0]) - 1;
                        }
                        else
                        {
                            if (value[1] == 0.0f) newBalance = 0.0f;
                            else newBalance = 1 - (value[0] / value[1]);
                        }

                        _base.AV_SetAudioChannels(_base._audioChannelsVolumeCopy, newVolume, newBalance);

                        //if (!_base._audioEnabled)
                        //{
                        //    _base._audioEnabled = true;
                        //    if (_base._mediaAudioMuteChanged != null) _base._mediaAudioMuteChanged(_base, EventArgs.Empty);
                        //}
                    }
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the maximum number of audio channels used by the player (default: 0 (no limit)). When set, the setting applies to the next media to be played. This option can be used to play media with more audio channels than is supported for the audio type.
        ///// </summary>
        //public int ChannelsLimit
        //{
        //    get
        //    {
        //        _base._lastError = NO_ERROR;
        //        return _base._audioChannelsLimit;
        //    }

        //    set
        //    {
        //        if (value > 0 && value <= Player.MAX_AUDIO_CHANNELS)
        //        {
        //            _base._lastError = NO_ERROR;
        //            _base._audioChannelsLimit = value;
        //        }
        //        else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
        //    }
        //}

        // Volume:

        /// <summary>
        /// Gets or sets the audio volume of the player.
        /// <br/>Values from 0.0 (mute) to 1.0 (max) (default: 1.0).
        /// </summary>
        public float Volume
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioVolume;
            }
            set { _base.AV_SetAudioVolume(value, true, true); }
        }

        /// <summary>
        /// Gradually increases or decreases the audio volume of the player to the specified level.
        /// </summary>
        /// <param name="level">The audio volume level to be set. Values from 0.0 (mute) to 1.0 (max).</param>
        public int VolumeTo(float level)
        {
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (_volumeIsFading)
                {
                    _volumeFadeCurrentValue = _base._audioVolume;
                    _volumeFadeEndValue     = level;
                    _volumeFadeStep         = _volumeFadeEndValue > _volumeFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;
                }
                else if (level != _base._audioVolume)
                {
                    _volumeIsFading         = true;
                    _volumeFadeCurrentValue = _base._audioVolume;

                    _volumeFadeEndValue     = level;
                    _volumeFadeStep         = _volumeFadeEndValue > _volumeFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;

                    _volumeFadeTimer        = new Timer
                    {
                        Interval            = _volumeFadeInterval
                    };
                    _volumeFadeTimer.Tick   += VolumeStep_Tick;
                    _volumeFadeTimer.Start();
                }
                _base._lastError = NO_ERROR;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gradually increases or decreases the audio volume of the player to the specified level.
        /// </summary>
        /// <param name="level">The audio volume level to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        /// <param name="interval">The time, in milliseconds, between two consecutive volume values.
        /// <br/>Values from 1 to 100 (default: 20).
        /// <br/>This interval is used until it is changed again.</param>
        public int VolumeTo(float level, int interval)
        {
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM || interval < FADE_INTERVAL_MINIMUM || interval > FADE_INTERVAL_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                return (int)_base._lastError;
            }

            _volumeFadeInterval = interval;
            if (_volumeIsFading) _volumeFadeTimer.Interval = interval;

            return VolumeTo(level);
        }

        private void VolumeStep_Tick(object sender, EventArgs e)
        {
            bool endReached = Math.Abs(_base._audioVolume - _volumeFadeEndValue) < FADE_STEP_VALUE;
            if (endReached || _volumeFadeCurrentValue != _base._audioVolume)
            {
                _volumeIsFading = false;
                _volumeFadeTimer.Dispose();
                _volumeFadeTimer = null;
                if (endReached) _base.AV_SetAudioVolume(_volumeFadeEndValue, true, true);
            }
            else
            {
                _volumeFadeCurrentValue += _volumeFadeStep;
                _base.AV_SetAudioVolume(_volumeFadeCurrentValue, true, true);
                Application.DoEvents();
            }
        }

        // Balance:

        /// <summary>
        /// Gets or sets the audio balance of the player.
        /// <br/>Values from -1.0 (left) to 1.0 (right) (default: 0.0).
        /// </summary>
        public float Balance
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioBalance;
            }
            set { _base.AV_SetAudioBalance(value, true, true); }
        }

        /// <summary>
        /// Gradually changes the audio balance of the player to the specified level.
        /// </summary>
        /// <param name="level">The audio balance level to be set.
        /// <br/>Values from -1.0 (left) to 1.0 (right).</param>
        public int BalanceTo(float level)
        {
            if (level < Player.AUDIO_BALANCE_MINIMUM || level > Player.AUDIO_BALANCE_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else
            {
                if (_balanceIsFading)
                {
                    _balanceFadeCurrentValue    = _base._audioBalance;
                    _balanceFadeEndValue        = level;
                    _balanceFadeStep            = _balanceFadeEndValue > _balanceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;
                }
                else if (level != _base._audioBalance)
                {
                    _balanceIsFading            = true;
                    _balanceFadeCurrentValue    = _base._audioBalance;

                    _balanceFadeEndValue        = level;
                    _balanceFadeStep            = _balanceFadeEndValue > _balanceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;

                    _balanceFadeTimer           = new Timer
                    {
                        Interval                = _balanceFadeInterval
                    };
                    _balanceFadeTimer.Tick      += BalanceStep_Tick;
                    _balanceFadeTimer.Start();
                }
                _base._lastError = NO_ERROR;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gradually changes the audio balance of the player to the specified level.
        /// </summary>
        /// <param name="level">The audio balance level to be set.
        /// <br/>Values from -1.0 (left) to 1.0 (right).</param>
        /// <param name="interval">The time, in milliseconds, between two consecutive balance values.
        /// <br/>This interval is used until it is changed again.
        /// <br/>Values from 1 to 100 (default: 10).</param>
        public int BalanceTo(float level, int interval)
        {
            if (level < Player.AUDIO_BALANCE_MINIMUM || level > Player.AUDIO_BALANCE_MAXIMUM || interval < FADE_INTERVAL_MINIMUM || interval > FADE_INTERVAL_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                return (int)_base._lastError;
            }

            _balanceFadeInterval = interval;
            if (_balanceIsFading) _balanceFadeTimer.Interval = interval;

            return BalanceTo(level);
        }

        private void BalanceStep_Tick(object sender, EventArgs e)
        {
            bool endReached = Math.Abs(_base._audioBalance - _balanceFadeEndValue) < FADE_STEP_VALUE;
            if (endReached || _balanceFadeCurrentValue != _base._audioBalance)
            {
                _balanceIsFading = false;
                _balanceFadeTimer.Dispose();
                _balanceFadeTimer = null;
                if (endReached) _base.AV_SetAudioBalance(_balanceFadeEndValue, true, true);
            }
            else
            {
                _balanceFadeCurrentValue += _balanceFadeStep;
                _base.AV_SetAudioBalance(_balanceFadeCurrentValue, true, true);
                Application.DoEvents();
            }
        }

        // Audio Devices:

        /// <summary>
        /// Gets the number of the system's enabled audio output devices.
        /// <br/>See also: Player.Audio.GetDevices.
        /// </summary>
        public int DeviceCount
        {
            get
            {
                uint count = 0;

                IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out IMMDeviceCollection deviceCollection);
                Marshal.ReleaseComObject(deviceEnumerator);

                if (deviceCollection != null)
                {
                    deviceCollection.GetCount(out count);
                    Marshal.ReleaseComObject(deviceCollection);
                }

                _base._lastError = NO_ERROR;
                return (int)count;
            }
        }

        /// <summary>
        /// Returns the system's enabled audio output devices.
        /// <br/>See also: Player.Audio.DeviceCount and Player.Audio.GetDefaultDevice.
        /// </summary>
        public AudioDevice[] GetDevices()
        {
            AudioDevice[] audioDevices = null;
            _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out IMMDeviceCollection deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                deviceCollection.GetCount(out uint count);
                if (count > 0)
                {
                    audioDevices = new AudioDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioDevice();

                        deviceCollection.Item((uint)i, out IMMDevice device);
                        Player.GetDeviceInfo(device, audioDevices[i]);

                        Marshal.ReleaseComObject(device);
                    }
                    _base._lastError = NO_ERROR;
                }
                Marshal.ReleaseComObject(deviceCollection);
            }
            return audioDevices;
        }

        /// <summary>
        /// Returns the system's default audio output device.
        /// <br/>See also: Player.Audio.DeviceCount and Player.Audio.GetDevices.
        /// </summary>
        public AudioDevice GetDefaultDevice()
        {
            AudioDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out IMMDevice device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;
            }

            return audioDevice;
        }

        // Active Audio Device:

        /// <summary>
        /// Gets or sets the audio output device used by the player (default: null).
        /// <br/>The default audio output device of the system is indicated by null.
        /// <br/>See also: Player.Audio.DeviceCount and Player.Audio.GetDevices.
        /// </summary>
        public AudioDevice Device
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioDevice;
            }
            set
            {
                _base._lastError = NO_ERROR;
                bool setDevice = false;

                if (value == null)
                {
                    if (_base._audioDevice != null)
                    {
                        _base._audioDevice = null;
                        setDevice = true;
                    }
                }
                else if (_base._audioDevice == null || value._id != _base._audioDevice._id)
                {
                    AudioDevice[] devices = GetDevices();
                    for (int i = 0; i < devices.Length; i++)
                    {
                        if (value._id == devices[i]._id)
                        {
                            _base._audioDevice = devices[i];
                            setDevice = true;
                            break;
                        }
                    }
                    if (!setDevice) _base._lastError = HResult.ERROR_SYSTEM_DEVICE_NOT_FOUND;
                }

                if (setDevice)
                {
                    if (_base._hasAudio) // = also playing
                    {
                        _base.AV_UpdateTopology();
                    }

                    if (_base._lastError == NO_ERROR)
                    {
                        if (_base.pm_HasPeakMeter)
                        {
                            _base.StartSystemDevicesChangedHandlerCheck();
                            _base.PeakMeter_Open(_base._audioDevice, true);
                        }
                        else
                        {
                            if (_base._audioDevice == null) _base.StopSystemDevicesChangedHandlerCheck();
                            else _base.StartSystemDevicesChangedHandlerCheck();
                        }
                        _base._mediaAudioDeviceChanged?.Invoke(_base, EventArgs.Empty);
                        _base._lastError = NO_ERROR;
                    }
                    else
                    {
                        _base.AV_CloseSession(false, true, StopReason.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the volume of the player's audio output device.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).
        /// </summary>
        public float DeviceVolume
        {
            get
            {
                float volume = Player.AudioDevice_MasterVolume(_base._audioDevice, 0, false);
                if (volume == -1)
                {
                    volume = 0;
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE; // device not ready
                }
                else _base._lastError = NO_ERROR;

                return volume;
            }
            set
            {
                if (value < Player.AUDIO_VOLUME_MINIMUM || value > Player.AUDIO_VOLUME_MAXIMUM)
                {
                    _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else
                {
                    float volume = Player.AudioDevice_MasterVolume(_base._audioDevice, value, true);
                    if (volume == -1) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    else
                    {
                        if (volume < 0.001) DeviceMute = true;
                        else if (DeviceMute) DeviceMute = false;
                        _base._lastError = NO_ERROR;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player's audio output device is muted.
        /// </summary>
        public bool DeviceMute
        {
            get
            {
                bool muteState = false;
                _base._lastError = (HResult)Player.AudioDevice_MasterMute(_base._audioDevice, ref muteState, false);
                return muteState;

            }
            set
            {
                bool muteState = value;
                _base._lastError = (HResult)Player.AudioDevice_MasterMute(_base._audioDevice, ref muteState, true);
            }
        }

        /// <summary>
        /// Gets the number of audio output channels of the player's audio output device.
        /// </summary>
        public int DeviceChannelCount
        {
            get
            {
                _base._lastError = NO_ERROR;

                if (_base.pm_HasPeakMeter) return _base.pm_PeakMeterChannelCount;
                else return Player.Device_GetChannelCount(_base._audioDevice);
            }
        }

        /// <summary>
        /// Gradually increases or decreases the volume of the player's audio output device to the specified level.
        /// </summary>
        /// <param name="level">The audio volume level to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        public int DeviceVolumeTo(float level)
        {
            float currentVolume = DeviceVolume;
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else if (currentVolume != -1)
            {
                if (_deviceIsFading)
                {
                    _deviceFadeCurrentValue     = currentVolume;
                    _deviceFadeEndValue         = level;
                    _deviceFadeStep             = _deviceFadeEndValue > _deviceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;
                }
                else if (level != DeviceVolume)
                {
                    _deviceIsFading             = true;
                    _deviceFadeCurrentValue     = currentVolume;

                    _deviceFadeEndValue         = level;
                    _deviceFadeStep             = _deviceFadeEndValue > _deviceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;

                    _deviceFadeTimer            = new Timer
                    {
                        Interval                = _deviceFadeInterval
                    };
                    _deviceFadeTimer.Tick       += DeviceVolumeStep_Tick;
                    _deviceFadeTimer.Start();
                }
                _base._lastError = NO_ERROR;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gradually increases or decreases the volume of the player's audio output device to the specified level.
        /// </summary>
        /// <param name="level">The audio volume level to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        /// <param name="interval">The time, in milliseconds, between two consecutive volume values.
        /// <br/>This interval is used until it is changed again.
        /// <br/>Values from 1 to 100 (default: 20).</param>
        public int DeviceVolumeTo(float level, int interval)
        {
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM || interval < FADE_INTERVAL_MINIMUM || interval > FADE_INTERVAL_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                return (int)_base._lastError;
            }

            _deviceFadeInterval = interval;
            if (_deviceIsFading) _deviceFadeTimer.Interval = interval;

            return DeviceVolumeTo(level);
        }

        private void DeviceVolumeStep_Tick(object sender, EventArgs e)
        {
            try
            {
                float currentVolume = DeviceVolume;
                bool endReached = currentVolume == -1 || Math.Abs(currentVolume - _deviceFadeEndValue) < FADE_STEP_VALUE;
                if (endReached || Math.Abs(currentVolume - _deviceFadeCurrentValue) > FADE_STEP_VALUE) //_masterFadeCurrentValue != MasterVolume)
                {
                    _deviceIsFading = false;
                    _deviceFadeTimer.Dispose();
                    _deviceFadeTimer = null;
                    if (endReached) DeviceVolume = _deviceFadeEndValue;
                }
                else
                {
                    _deviceFadeCurrentValue += _deviceFadeStep;
                    DeviceVolume = _deviceFadeCurrentValue;
                    Application.DoEvents();
                }
            }
            catch
            {
                _deviceIsFading = false;
                _deviceFadeTimer.Dispose();
                _deviceFadeTimer = null;
            }
        }

    }
}