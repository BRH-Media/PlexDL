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
        /// Represents online (internet) media files (including file streams and live streams).
        /// </summary>
        OnlineFile,
        /// <summary>
        /// Represents local capture devices (including webcams and audio input devices).
        /// </summary>
        LocalCapture,
        /// <summary>
        /// Represents online (internet) capture devices.
        /// </summary>
        OnlineCapture
    }
}