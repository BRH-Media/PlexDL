using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Audio Input methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AudioInput : HideObjectMembers
    {
        #region Fields (Audio Input Class)

        private Player _base;

        #endregion Fields (Audio Input Class)

        internal AudioInput(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Gets the number of the system's enabled audio input devices. See also: Player.AudioInput.GetDevices.
        /// </summary>
        public int DeviceCount
        {
            get
            {
                IMMDeviceCollection deviceCollection;
                uint count = 0;

                IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
                deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.Active, out deviceCollection);
                Marshal.ReleaseComObject(deviceEnumerator);

                if (deviceCollection != null)
                {
                    deviceCollection.GetCount(out count);
                    Marshal.ReleaseComObject(deviceCollection);
                }

                _base._lastError = Player.NO_ERROR;
                return (int)count;
            }
        }

        /// <summary>
        /// Returns a list of the system's enabled audio input devices. Returns null if no enabled audio input devices are present. See also: Player.AudioInput.DeviceCount and Player.AudioInput.GetDefaultDevice.
        /// </summary>
        public AudioInputDevice[] GetDevices()
        {
            IMMDeviceCollection deviceCollection;
            IMMDevice device;
            AudioInputDevice[] audioDevices = null;

            _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.EnumAudioEndpoints(EDataFlow.eCapture, (uint)DeviceState.Active, out deviceCollection);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (deviceCollection != null)
            {
                uint count;
                deviceCollection.GetCount(out count);

                if (count > 0)
                {
                    audioDevices = new AudioInputDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        audioDevices[i] = new AudioInputDevice();

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
        /// Returns the system's default audio input device. Returns null if no default audio input device is present. See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice GetDefaultDevice()
        {
            IMMDevice device;
            AudioInputDevice audioDevice = null;

            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out device);
            Marshal.ReleaseComObject(deviceEnumerator);

            if (device != null)
            {
                audioDevice = new AudioInputDevice();
                Player.GetDeviceInfo(device, audioDevice);

                Marshal.ReleaseComObject(device);
                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _base._lastError = HResult.MF_E_NO_AUDIO_RECORDING_DEVICE;
            }

            return audioDevice;
        }

        /// <summary>
        /// Gets a value that indicates whether an audio input device is playing (on its own or with a webcam device - including paused audio input). Use the Player.Play method to play an audio input device.
        /// </summary>
        public bool Playing
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode) return _base._webcamAggregated;
                return _base._micMode;
            }
        }

        /// <summary>
        /// Gets or sets (changes) the audio input device that is playing (on its own or with a webcam device). Use the Player.Play method to play an audio input device. See also: Player.AudioInput.GetDevices.
        /// </summary>
        public AudioInputDevice Device
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._micDevice;
            }
            set
            {
                _base._lastError = Player.NO_ERROR;
                if (_base._webcamMode || _base._micMode)
                {
                    _base._lastError = Player.NO_ERROR;
                    if ((value == null && _base._micDevice != null) ||
                        (value != null && _base._micDevice == null) ||
                        _base._micDevice._id != value._id)
                    {
                        _base._micDevice = value;
                        _base.AV_UpdateTopology();
                        if (_base._mediaAudioInputDeviceChanged != null) _base._mediaAudioInputDeviceChanged(_base, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Updates or restores the audio playback of the playing audio input device.
        /// </summary>
        public int Update()
        {
            if (_base._micMode)
            {
                _base._lastError = Player.NO_ERROR;
                _base.AV_UpdateTopology();
            }
            else _base._lastError = HResult.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED;
            return (int)_base._lastError;
        }
    }
}