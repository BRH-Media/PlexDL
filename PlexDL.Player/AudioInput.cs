using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Audio Input methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public sealed class AudioInput : HideObjectMembers
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        #region Fields (Audio Input Class)

        #region Constants

        private const int   NO_ERROR                = 0;

        private const int   VOLUME_FADE_INTERVAL    = 20;

        private const int   FADE_INTERVAL_MINIMUM   = 1;
        private const int   FADE_INTERVAL_MAXIMUM   = 100;

        private const float FADE_STEP_VALUE         = 0.01f;

        #endregion

        private Player      _base;

        // Device volume fading
        private bool        _deviceIsFading;
        private Timer       _deviceFadeTimer;
        private int         _deviceFadeInterval     = VOLUME_FADE_INTERVAL;
        private float       _deviceFadeStep;
        private float       _deviceFadeEndValue;
        private float       _deviceFadeCurrentValue;

        #endregion

        internal AudioInput(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets the number of the system's enabled audio input devices.
        /// <br/>See also: Player.AudioInput.GetDevices.
        /// </summary>
        public int DeviceCount
        {
            get
            {
                uint count = 0;

                IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.Active, out IMMDeviceCollection deviceCollection);
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
        /// Returns the system's enabled audio input devices.
        /// <br/>See also: Player.AudioInput.DeviceCount and Player.AudioInput.GetDefaultDevice.
        /// </summary>
        public AudioInputDevice[] GetDevices()
        {
            AudioInputDevice[] audioDevices = null;
            _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.Active, out IMMDeviceCollection deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                deviceCollection.GetCount(out uint count);
                if (count > 0)
                {
                    audioDevices = new AudioInputDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioInputDevice();

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
        /// Returns the system's default audio input device.
        /// <br/>See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice GetDefaultDevice()
        {
            AudioInputDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia, out IMMDevice device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioInputDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
            }

            return audioDevice;
        }

        /// <summary>
        /// Gets a value indicating whether an audio input device is playing (by itself or with a webcam) (including paused audio input).
        /// <br/>Audio input devices can be played using the Player.Play method.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = NO_ERROR;
                if (_base._webcamMode) return _base._webcamAggregated;
                return _base._micMode;
            }
        }

        /// <summary>
        /// Gets or sets (changes (when not recording)) the playing audio input device (by itself or with a webcam).
        /// <br/>Audio input devices can be played using the Player.Play method.
        /// <br/>See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice Device
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._micDevice;
            }
            set
            {
                if (!_base._recording && (_base._webcamMode || _base._micMode))
                {
                    //_base._lastError = NO_ERROR;
                    if (!((value == null && _base._micDevice == null) || (value != null && _base._micDevice != null && value._id == _base._micDevice._id)))
                    {
                        if (_base.pm_HasInputMeter)
                        {
                            _base.InputMeter_Close();
                            _base.pm_InputMeterPending = true;
                        }

                        _base._micDevice = value;
                        _base.AV_UpdateTopology();

                        _base._mediaAudioInputDeviceChanged?.Invoke(_base, EventArgs.Empty);
                    }
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Updates or restores the playing audio input device.
        /// <br/>For special use only, generally not required.
        /// </summary>
        public int Update()
        {
            if (_base._micMode)
            {
                _base._lastError = NO_ERROR;
                _base.AV_UpdateTopology();
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gets or sets the volume of the playing audio input device.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).
        /// </summary>
        public float Volume
        {
            get
            {
                float volume = Player.AudioDevice_InputLevel(_base._micDevice, 0, false);
                if (volume == -1)
                {
                    volume = 0;
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE; // device not ready
                }
                else _base._lastError = NO_ERROR;

                //float volume = Player.AudioDevice_InputLevel(_base._micDevice, 0, false);
                //if (volume == -1) volume = 0;
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
                    float volume = Player.AudioDevice_InputLevel(_base._micDevice, value, true);
                    if (volume == -1) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    else _base._lastError = NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the playing audio input device is muted.
        /// </summary>
        public bool Mute
        {
            get
            {
                bool muteState = false;

                if (_base._micDevice != null) _base._lastError = (HResult)Player.AudioDevice_InputMute(_base._micDevice, ref muteState, false);
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

                return muteState;

            }
            set
            {
                if (_base._micDevice == null) _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                else
                {
                    bool muteState = value;
                    _base._lastError = (HResult)Player.AudioDevice_InputMute(_base._micDevice, ref muteState, true);
                }
            }
        }

        /// <summary>
        /// Gets the number of audio channels of the playing audio input device.
        /// </summary>
        public int ChannelCount
        {
            get
            {
                _base._lastError = NO_ERROR;

                if (_base.pm_HasInputMeter) return _base.pm_InputMeterChannelCount;
                else if (_base._micDevice != null)  return Player.Device_GetChannelCount(_base._micDevice);
                return 0;
            }
        }

        /// <summary>
        /// Gradually increases or decreases the volume of the playing audio input device to the specified level.
        /// </summary>
        /// <param name="level">The audio input volume level to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        public int VolumeTo(float level)
        {
            float currentVolume = Volume;
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else if (currentVolume != -1)
            {
                if (_deviceIsFading)
                {
                    _deviceFadeCurrentValue = currentVolume;
                    _deviceFadeEndValue = level;
                    _deviceFadeStep = _deviceFadeEndValue > _deviceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;
                }
                else if (level != Volume)
                {
                    _deviceIsFading = true;
                    _deviceFadeCurrentValue = currentVolume;

                    _deviceFadeEndValue = level;
                    _deviceFadeStep = _deviceFadeEndValue > _deviceFadeCurrentValue ? FADE_STEP_VALUE : -FADE_STEP_VALUE;

                    _deviceFadeTimer = new Timer
                    {
                        Interval = _deviceFadeInterval
                    };
                    _deviceFadeTimer.Tick += VolumeStep_Tick;
                    _deviceFadeTimer.Start();
                }
                _base._lastError = NO_ERROR;
            }
            return (int)_base._lastError;
        }

        /// <summary>
        /// Gradually increases or decreases the volume of the playing audio input device to the specified level.
        /// </summary>
        /// <param name="level">The audio input volume level to be set. Values from 0.0 (mute) to 1.0 (max).</param>
        /// <param name="interval">The time, in milliseconds, between two consecutive volume values.
        /// <br/>This interval is used until it is changed again.
        /// <br/>Values from 1 to 100 (default: 20).</param>
        public int VolumeTo(float level, int interval)
        {
            if (level < Player.AUDIO_VOLUME_MINIMUM || level > Player.AUDIO_VOLUME_MAXIMUM || interval < FADE_INTERVAL_MINIMUM || interval > FADE_INTERVAL_MAXIMUM)
            {
                _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                return (int)_base._lastError;
            }

            _deviceFadeInterval = interval;
            if (_deviceIsFading) _deviceFadeTimer.Interval = interval;

            return VolumeTo(level);
        }

        private void VolumeStep_Tick(object sender, EventArgs e)
        {
            try
            {
                float currentVolume = Volume;
                bool endReached = currentVolume == -1 || Math.Abs(currentVolume - _deviceFadeEndValue) < FADE_STEP_VALUE;
                if (endReached || Math.Abs(currentVolume - _deviceFadeCurrentValue) > FADE_STEP_VALUE) //_masterFadeCurrentValue != MasterVolume)
                {
                    _deviceIsFading = false;
                    _deviceFadeTimer.Dispose();
                    _deviceFadeTimer = null;
                    if (endReached) Volume = _deviceFadeEndValue;
                }
                else
                {
                    _deviceFadeCurrentValue += _deviceFadeStep;
                    Volume = _deviceFadeCurrentValue;
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