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
        /// Gets the identification of the audio device that is the subject of this notification.
        /// </summary>
        public string DeviceId
        {
            get { return _deviceId; }
        }

        /// <summary>
        /// Gets a value that indicates whether the audio device that is the subject of this notification is an audio intput device (or audio output device).
        /// </summary>
        public bool IsInputDevice
        {
            get { return _inputDevice; }
        }

        /// <summary>
        /// Gets the reason for this notification.
        /// </summary>
        public SystemAudioDevicesNotification Notification
        {
            get { return _notification; }
        }
    }
}