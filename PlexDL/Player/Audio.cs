using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Audio methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Audio : HideObjectMembers
    {
        #region Fields (Audio Class)

        private Player _base;

        #endregion Fields (Audio Class)

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
                _base._lastError = Player.NO_ERROR;
                return _base._hasAudio;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio output of the player is enabled (default: true).
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio output of the player is muted (default: false).
        /// </summary>
        public bool Mute
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return !_base._audioEnabled;
            }
            set { _base.AV_SetAudioEnabled(!value); }
        }

        /// <summary>
        /// Gets or sets the active audio track of the playing media. See also: Player.Media.GetAudioTracks.
        /// </summary>
        public int Track
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioTrackCurrent;
            }
            set { _base.AV_SetTrack(value, true); }
        }

        /// <summary>
        /// Gets the number of audio channels in the active audio track of the playing media. See also: Player.Audio.DeviceChannelCount.
        /// </summary>
        public int ChannelCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (!_base._playing) return 0;
                return _base._mediaChannelCount;
            }
        }

        /// <summary>
        /// Gets the number of output channels of the player's audio output device. See also: Player.Audio.ChannelCount.
        /// </summary>
        public int DeviceChannelCount
        {
            get
            {
                _base._lastError = Player.NO_ERROR;

                if (_base.pm_HasPeakMeter) return _base.pm_PeakMeterChannelCount;
                else return Player.Device_GetChannelCount(_base._audioDevice);
            }
        }

        /// <summary>
        /// Gets or sets the value of each individual audio channel (up to 16 channels), values from 0.0 (mute) to 1.0 (max). See also: Player.Audio.ChannelCount and .DeviceChannelCount.
        /// </summary>
        public float[] ChannelVolumes
        {
            get
            {
                _base._lastError = Player.NO_ERROR;

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
                        _base._lastError = Player.NO_ERROR;

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

        /// <summary>
        /// Gets or sets the audio volume of the player, values from 0.0 (mute) to 1.0 (max) (default: 1.0).
        /// </summary>
        public float Volume
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioVolume;
            }
            set { _base.AV_SetAudioVolume(value, true, true); }
        }

        /// <summary>
        /// Gets or sets the audio balance of the player, values from -1.0 (left) to 1.0 (right) (default: 0.0).
        /// </summary>
        public float Balance
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioBalance;
            }
            set { _base.AV_SetAudioBalance(value, true, true); }
        }

        /// <summary>
        /// Gets or sets the audio volume of the player's audio output device, value 0.0 (mute) to 1.0 (max).
        /// </summary>
        public float MasterVolume
        {
            get
            {
                float volume = Player.AudioDevice_MasterVolume(_base._audioDevice, 0, false);
                if (volume == -1)
                {
                    volume = 0;
                    _base._lastError = HResult.E_FAIL; // device not ready
                }
                else _base._lastError = Player.NO_ERROR;

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
                    _base._lastError = volume == -1 ? HResult.E_FAIL : Player.NO_ERROR;
                }
            }
        }

        /// <summary>
        /// Gets or sets the audio output device used by the player (default: null). The default audio output device of the system is indicated by null. See also: Player.Audio.GetDevices and .GetDefaultDevice.
        /// </summary>
        public AudioDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._audioDevice;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
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

                    if (_base._lastError == Player.NO_ERROR)
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

                        if (_base._mediaAudioDeviceChanged != null) _base._mediaAudioDeviceChanged(_base, EventArgs.Empty);
                    }
                    else
                    {
                        _base.AV_CloseSession(false, true, StopReason.Error);
                    }
                }
            }
        }

        ///// <summary>
        ///// Gets the number of enabled audio output devices from the system.
        ///// </summary>
        //public int DeviceCount
        //{
        //    get
        //    {
        //        _base._lastError = Player.NO_ERROR;
        //        uint count = 0;

        //        IMMDeviceCollection deviceCollection;
        //        IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
        //        deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
        //        Marshal.ReleaseComObject(deviceEnumerator);
        //        if (deviceCollection != null)
        //        {
        //            deviceCollection.GetCount(out count);
        //            Marshal.ReleaseComObject(deviceCollection);
        //        }
        //        return (int)count;
        //    }
        //}

        /// <summary>
        /// Returns a list of the system's enabled audio output devices. Returns null if no enabled audio output devices are present.
        /// </summary>
        public AudioDevice[] GetDevices()
        {
            IMMDeviceCollection deviceCollection;
            IMMDevice device;
            AudioDevice[] audioDevices = null;

            _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, (uint)DeviceState.Active, out deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                uint count;
                deviceCollection.GetCount(out count);

                if (count > 0)
                {
                    audioDevices = new AudioDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioDevice();

                        deviceCollection.Item((uint)i, out device);
                        Player.GetDeviceInfo(device, audioDevices[i]);

                        Marshal.ReleaseComObject(device);
                    }

                    _base._lastError = Player.NO_ERROR;
                }

                Marshal.ReleaseComObject(deviceCollection);
            }

            return audioDevices;
        }

        /// <summary>
        /// Returns the system's default audio output device. Returns null if no default audio output device is present.
        /// </summary>
        public AudioDevice GetDefaultDevice()
        {
            IMMDevice device;
            AudioDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_PLAYBACK_DEVICE;
            }

            return audioDevice;
        }

        ///// <summary>
        ///// Returns the index of the system's default audio output device in a list of audio output devices. Returns -1 if the device is not in the list.
        ///// </summary>
        ///// <param name="devices">The list of audio output devices in which the system's default audio output device is to be found.</param>
        //public int GetDefaultDeviceIndex(AudioDevice[] devices)
        //{
        //    int index = -1;

        //    if (devices != null && devices.Length > 0)
        //    {
        //        AudioDevice device = GetDefaultDevice();
        //        if (device != null)
        //        {
        //            for (int i = 0; i < devices.Length; i++)
        //            {
        //                if (devices[i].Id == device.Id)
        //                {
        //                    index = i;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return index;
        //}

        //private void GetAudioDeviceInfo(IMMDevice device, AudioDevice audioDevice)
        //{
        //    //if (device != null && audioDevice != null)
        //    {
        //        device.GetId(out audioDevice._id);

        //        IPropertyStore store;
        //        device.OpenPropertyStore((uint)STGM.STGM_READ, out store);

        //        if (store != null)
        //        {
        //            PropVariant property = new PropVariant();

        //            store.GetValue(Player.PKEY_Device_Description, property);
        //            audioDevice._name = (string)property;

        //            store.GetValue(Player.PKEY_DeviceInterface_FriendlyName, property);
        //            audioDevice._adapter = (string)property;

        //            property.Dispose();
        //            Marshal.ReleaseComObject(store);
        //        }
        //    }
        //}
    }
}