namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the media source category.
    /// </summary>
    public enum MediaSourceCategory
    {
        // the order of these items should not be changed.

        /// <summary>
        /// Represents no category.
        /// </summary>
        None,
        /// <summary>
        /// Represents local media files (including byte arrays and images).
        /// </summary>
        LocalFile,
        /// <summary>
        /// Represents remote (online) media files.
        /// </summary>
        RemoteFile,
        /// <summary>
        /// Represents online live media streams (broadcasts).
        /// </summary>
        LiveStream,
        /// <summary>
        /// Represents local capture devices (including webcams and audio input devices).
        /// </summary>
        LocalCapture,
        /// <summary>
        /// Represents remote (online) capture devices.
        /// </summary>
        RemoteCapture
    }
}