namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the changes that have occured in the system's audio devices.
    /// </summary>
    public enum SystemAudioDevicesNotification
    {
        /// <summary>
        /// The default audio device has changed.
        /// </summary>
        DefaultChanged,
        /// <summary>
        /// A new audio device has been added.
        /// </summary>
        Added,
        /// <summary>
        /// An audio device has been removed.
        /// </summary>
        Removed,
        /// <summary>
        /// An audio device has been disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// An audio device has been activated.
        /// </summary>
        Activated,
        /// <summary>
        /// The description (for example "Speakers") of an audio device has changed.
        /// </summary>
        DescriptionChanged
    }
}