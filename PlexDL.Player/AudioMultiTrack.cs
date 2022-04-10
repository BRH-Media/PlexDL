using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Audio Multi Track methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AudioMultiTrack : HideObjectMembers
    {
        #region Fields

        private const int                   NO_ERROR = 0;

        private Player                      _base;
        private AudioTrackDeviceEventArgs   _trackEventArgs;

        #endregion

        internal AudioMultiTrack(Player player)
        {
            _base = player;
            _trackEventArgs = new AudioTrackDeviceEventArgs();
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the player plays all audio tracks of the playing media simultaneously (default: false).
        /// <br/>If true, the properties (for example, the volume) of each individual audio track can be set using the player's usual audio methods
        /// <br/>when a track is selected (Player.Audio.Track) or at any time using one of the Player.AudioMultiTrack.Set... methods.
        /// </summary>
        public bool Enabled
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioMultiTrack;
            }
            set
            {
                if (value != _base._audioMultiTrack)
                {
                    _base._audioMultiTrack = value;
                    if (_base._hasAudio)
                    {
                        if (!value)
                        {
                            if (_base._audioTrackCount > 1 || _base._audioTracks[0].AudioDevice != null || _base._audioTracks[0].Enabled != _base._audioEnabled)
                            {
                                for (int i = 0; i < _base._audioTrackCount; i++)
                                {
                                    for (int j = 0; j < Player.MAX_AUDIO_CHANNELS; j++) _base._audioTracks[i].ChannelVolumes[j] = 0;

                                    _base._audioTracks[i].Volume = 0;
                                    _base._audioTracks[i].Balance = 0;
                                    _base._audioTracks[i].AudioDevice = null;
                                    _base._audioTracks[i].Enabled = _base._audioEnabled; // important
                                }
                                _base.AV_UpdateTopology();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _base._audioTrackCount; i++)
                            {
                                _base._audioTracks[i].Enabled = _base._audioEnabled;
                            }
                        }
                    }
                    _base._mediaAudioMultiTrackChanged?.Invoke(_base, EventArgs.Empty);
                }
                _base._lastError = NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets the active audio track of the playing media.
        /// <br/>Same as Player.Audio.Track.
        /// </summary>
        public int ActiveTrack
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioTrackCurrent;
            }
            set { _base.AV_SetTrack(value, true); }
        }

        /// <summary>
        /// Gets the number of audio tracks in the playing media.
        /// <br/>Same as Player.Audio.TrackCount.
        /// </summary>
        public int Count
        {
            get
            {
                _base._lastError = NO_ERROR;
                return _base._audioTrackCount;
            }
        }

        /// <summary>
        /// Returns a string with a detailed description of the specified audio track of the playing media.
        /// <br/>Same as Player.Audio.GetTrackString.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose description string is to be obtained.</param>
        public string GetString(int track)
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
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return string.Empty;
        }

        /// <summary>
        /// Mutes or unmutes the specified audio track of the playing media.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track to be muted or unmuted.</param>
        /// <param name="mute">A value that indicates whether to mute (true) or unmute (false) the audio track.</param>
        public int SetMute(int track, bool mute)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    try
                    {
                        if (mute == _base._audioTracks[track].Enabled)
                        {
                            if (track == _base._audioTrackCurrent)
                            {
                                if (mute) _base.mf_AudioStreamVolume.SetAllVolumes(_base._audioChannelCount, _base._audioChannelsMute);
                                else _base.mf_AudioStreamVolume.SetAllVolumes(_base._audioChannelCount, _base._audioChannelsVolume);
                            }
                            else
                            {
                                if (mute) _base._audioTracks[track].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioChannelsMute);
                                else _base._audioTracks[track].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioTracks[track].ChannelVolumes);
                            }
                            _base._audioTracks[track].Enabled = !mute;

                            if (_base._mediaAudioMuteChanged != null)
                            {
                                _base._audioEventArgs._track = track;
                                _base._audioEventArgs._volume = _base._audioTracks[track].Volume;
                                _base._audioEventArgs._balance = _base._audioTracks[track].Balance;
                                _base._audioEventArgs._muted = !_base._audioTracks[track].Enabled;
                                _base._mediaAudioMuteChanged(this, _base._audioEventArgs);
                            }
                        }
                        _base._lastError = NO_ERROR;
                    }
                    catch { _base._lastError = HResult.E_FAIL; }
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Mutes or unmutes all audio tracks of the playing media.
        /// </summary>
        /// <param name="mute">A value that indicates whether to mute (true) or unmute (false) the audio tracks.</param>
        public int SetMuteAll(bool mute)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                try
                {
                    for (int i = 0; i < _base._audioTrackCount; i++)
                    {
                        if (mute == _base._audioTracks[i].Enabled)
                        {
                            if (mute) _base._audioTracks[i].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioChannelsMute);
                            else _base._audioTracks[i].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioTracks[i].ChannelVolumes);

                            _base._audioTracks[i].Enabled = !mute;

                            if (_base._mediaAudioMuteChanged != null)
                            {
                                _base._audioEventArgs._track = i;
                                _base._audioEventArgs._volume = _base._audioTracks[i].Volume;
                                _base._audioEventArgs._balance = _base._audioTracks[i].Balance;
                                _base._audioEventArgs._muted = !_base._audioTracks[i].Enabled;
                                _base._mediaAudioMuteChanged(this, _base._audioEventArgs);
                            }
                        }
                    }
                }
                catch { /* ignored */ }
            }

            _base.AV_SetAudioEnabled(!mute);
            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns a value indicating whether the specified audio track of the playing media is muted.
        /// </summary>
        /// <param name="track">The (zero based) track number of the audio track whose audio mute status is to be obtained.</param>
        public bool GetMute(int track)
        {
            bool muted = false;

            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    muted = !_base._audioTracks[track].Enabled;
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return muted;
        }

        /// <summary>
        /// Sets the audio volume of the specified audio track of the playing media.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio volume is to be set.</param>
        /// <param name="volume">The volume value to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        public int SetVolume(int track, float volume)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount && volume >= 0 && volume <= 1)
                {
                    if (track == _base._audioTrackCurrent)
                    {
                        _base.AV_SetAudioVolume(volume, true, true);
                    }
                    else
                    {
                        if (volume != _base._audioTracks[track].Volume)
                        {
                            float balance = _base._audioTracks[track].Balance;

                            _base._audioTracks[track].Volume = volume;
                            for (int i = 0; i < Player.MAX_AUDIO_CHANNELS; i++) { _base._audioTracks[track].ChannelVolumes[i] = volume; }

                            if (balance != 0)
                            {
                                if (balance < 0) _base._audioTracks[track].ChannelVolumes[1] = volume * (1 + balance);
                                else _base._audioTracks[track].ChannelVolumes[0] = volume * (1 - balance);
                            }

                            if (_base._audioTracks[track].Enabled)
                            {
                                try
                                {
                                    _base._audioTracks[track].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioTracks[track].ChannelVolumes);
                                }
                                catch { /* ignored */ }
                            }

                            if (_base._mediaAudioVolumeChanged != null)
                            {
                                _base._audioEventArgs._track    = track;
                                _base._audioEventArgs._volume   = _base._audioTracks[track].Volume;
                                _base._audioEventArgs._balance  = _base._audioTracks[track].Balance;
                                _base._audioEventArgs._muted    = !_base._audioTracks[track].Enabled;
                                _base._mediaAudioVolumeChanged(this, _base._audioEventArgs);
                            }
                        }
                        _base._lastError = NO_ERROR;
                    }
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the audio volume of all audio tracks of the playing media.
        /// </summary>
        /// <param name="volume">The volume value to be set.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).</param>
        public int SetVolumeAll(float volume)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (_base._audioTrackCount > 0 && volume >= 0 && volume <= 1)
                {
                    for (int i = 0; i < _base._audioTrackCount; i++)
                    {
                        SetVolume(i, volume);
                    }
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns the audio volume value of the specified audio track of the playing media.
        /// <br/>Values from 0.0 (mute) to 1.0 (max).
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio volume is to be obtained.</param>
        public float GetVolume(int track)
        {
            float volume = 0;

            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    if (track == _base._audioTrackCurrent) volume = _base._audioVolume;
                    else volume = _base._audioTracks[track].Volume;
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return volume;
        }

        /// <summary>
        /// Sets the audio balance of the specified audio track of the playing media.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio balance is to be set.</param>
        /// <param name="balance">The balance value to be set.
        /// <br/>Values from -1.0 (left) to 1.0 (right).</param>
        public int SetBalance(int track, float balance)
        {
            if (_base._audioMultiTrack && _base._hasAudio)
            {
                if (track >= 0 && track < _base._audioTrackCount && balance >= -1 && balance <= 1)
                {
                    if (track == _base._audioTrackCurrent)
                    {
                        _base.AV_SetAudioBalance(balance, true, true);
                    }
                    else
                    {
                        if (balance != _base._audioTracks[track].Balance)
                        {
                            float volume = _base._audioTracks[track].Volume;
                            _base._audioTracks[track].Balance = balance;

                            if (balance <= 0)
                            {
                                _base._audioTracks[track].ChannelVolumes[0] = volume;
                                _base._audioTracks[track].ChannelVolumes[1] = volume * (1 + balance);
                            }
                            else
                            {
                                _base._audioTracks[track].ChannelVolumes[0] = volume * (1 - balance);
                                _base._audioTracks[track].ChannelVolumes[1] = volume;
                            }

                            if (_base._audioTracks[track].Enabled)
                            {
                                try
                                {
                                    _base._audioTracks[track].MF_VolumeService.SetAllVolumes(_base._audioChannelCount, _base._audioTracks[track].ChannelVolumes);
                                }
                                catch { /* ignored */ }
                            }

                            if (_base._mediaAudioBalanceChanged != null)
                            {
                                _base._audioEventArgs._track = track;
                                _base._audioEventArgs._volume = _base._audioTracks[track].Volume;
                                _base._audioEventArgs._balance = _base._audioTracks[track].Balance;
                                _base._audioEventArgs._muted = !_base._audioTracks[track].Enabled;
                                _base._mediaAudioBalanceChanged(this, _base._audioEventArgs);
                            }
                        }
                        _base._lastError = NO_ERROR;
                    }
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the audio balance of all audio tracks of the playing media.
        /// </summary>
        /// <param name="balance">The balance value to be set.
        /// <br/>Values from -1.0 (left) to 1.0 (right).</param>
        public int SetBalanceAll(float balance)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (_base._audioTrackCount > 0 && balance >= -1 && balance <= 1)
                {
                    for (int i = 0; i < _base._audioTrackCount; i++)
                    {
                        SetBalance(i, balance);
                    }
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns the audio balance value of the specified audio track of the playing media.
        /// <br/>Values from -1.0 (left) to 1.0 (right).
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio balance value is to be obtained.</param>
        public float GetBalance(int track)
        {
            float balance = 0;

            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    if (track == _base._audioTrackCurrent) balance = _base._audioBalance;
                    else balance = _base._audioTracks[track].Balance;
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return balance;
        }

        /// <summary>
        /// Sets the audio device used by the specified audio track of the playing media.
        /// <br/>The audio device used by the player is indicated by null (Player.Audio.Device).
        /// <br/>The audio devices of the audio tracks in the audio multitrack mode are independent of the main audio device of the player.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio device is to be set.</param>
        /// <param name="device">The audio device to be set.</param>
        public int SetDevice(int track, AudioDevice device)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    bool setDevice = false;

                    if (_base._audioTracks[track].AudioDevice == null)
                    {
                        if (device != null) setDevice = true;
                    }
                    else if (device == null || _base._audioTracks[track].AudioDevice._id != device._id)
                    {
                        setDevice = true;
                    }

                    if (setDevice)
                    {
                        _base._audioTracks[track].AudioDevice = device;
                        _base.AV_UpdateTopology();

                        if (_base._lastError == NO_ERROR)
                        {
                            _trackEventArgs._track = track;
                            _base._mediaAudioTrackDeviceChanged?.Invoke(_base, _trackEventArgs);
                            _base._lastError = NO_ERROR;
                        }
                    }
                    else _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Sets the audio device used by all audio tracks of the playing media.
        /// <br/>The audio device used by the player is indicated by null (Player.Audio.Device).
        /// <br/>The audio devices of the audio tracks in the audio multitrack mode are independent of the main audio device of the player.
        /// </summary>
        /// <param name="device">The audio device to be set.</param>
        public int SetDeviceAll(AudioDevice device)
        {
            if (_base._hasAudio && _base._audioMultiTrack)
            {
                int count = _base._audioTrackCount;
                if (count > 0)
                {
                    AudioDevice[] devices = new AudioDevice[count];
                    for (int i = 0; i < count; i++) devices[i] = device;
                    Devices = devices;
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns the audio device used by the specified audio track of the playing media.
        /// <br/>The audio device used by the player is indicated by null (Player.Audio.Device).
        /// <br/>The audio devices of the audio tracks in the audio multitrack mode are independent of the main audio device of the player.
        /// </summary>
        /// <param name="track">The (zero-based) track number of the audio track whose audio device is to be obtained.</param>
        public AudioDevice GetDevice(int track)
        {
            AudioDevice device = null;

            if (_base._hasAudio && _base._audioMultiTrack)
            {
                if (track >= 0 && track < _base._audioTrackCount)
                {
                    device = _base._audioTracks[track].AudioDevice;
                    _base._lastError = NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return device;
        }

        /// <summary>
        /// Gets or sets the audio output devices (all or several at once) used by each individual audio track of the playing media.
        /// <br/>The value null represents the audio output device used by the player (Player.Audio.Device).
        /// <br/>In audio multitrack mode, the audio devices of the audio tracks are independent of the audio device of the player.
        /// </summary>
        public AudioDevice[] Devices
        {
            get
            {
                AudioDevice[] devices = null;
                if (_base._audioMultiTrack && _base._hasAudio)
                {
                    _base._lastError = NO_ERROR;
                    int count = _base._audioTrackCount;
                    devices = new AudioDevice[count];
                    for (int i = 0; i < count; i++)
                    {
                        devices[i] = _base._audioTracks[i].AudioDevice;
                    }
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return devices;
            }
            set
            {
                if (_base._hasAudio && _base._audioMultiTrack)
                {
                    if (value != null)
                    {
                        bool setDevices = false;
                        int count = value.Length <= _base._audioTrackCount ? value.Length : _base._audioTrackCount;

                        for (int i = 0; i < count; i++)
                        {
                            if (_base._audioTracks[i].AudioDevice != value[i])
                            {
                                _base._audioTracks[i].AudioDevice = value[i];
                                setDevices = true;
                            }
                        }
                        _base._lastError = NO_ERROR;

                        if (setDevices)
                        {
                            _base.AV_UpdateTopology();
                            _trackEventArgs._track = -1;
                            if (_base._lastError == NO_ERROR) _base._mediaAudioTrackDeviceChanged?.Invoke(_base, _trackEventArgs);
                        }
                    }
                    else _base._lastError = HResult.E_INVALIDARG;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }
    }
}