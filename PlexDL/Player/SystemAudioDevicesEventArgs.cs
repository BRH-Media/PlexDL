using System;
using System.ComponentModel;

namespace PlexDL.Player
{
    /// <summary>
    /// Provides data for the Player.Events.MediaSystemAudioDevicesChanged event.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SystemAudioDevicesEventArgs : HideObjectEventArgs
    {
        internal string _deviceId;
        internal bool _inputDevice;
        internal SystemAudioDevicesNotification _notification;

        /// <summary>
        /// Gets the identification of the audio device that is the subject of this notification (event).
        /// </summary>
        public string DeviceId => _deviceId;

        /// <summary>
        /// Gets a value indicating whether the device that is the subject of this notification (event) is an audio intput device.
        /// </summary>
        public bool IsInputDevice => _inputDevice;

        /// <summary>
        /// Gets the reason for this notification (event).
        /// </summary>
        public SystemAudioDevicesNotification Notification => _notification;
    }
}