namespace PlexDL.AltoHTTP.Enums
{
    /// <summary>
    ///     Download states
    /// </summary>
    public enum DownloadState
    {
        /// <summary>
        ///     Download has started
        /// </summary>
        Started,

        /// <summary>
        ///     Download is paused
        /// </summary>
        Paused,

        /// <summary>
        ///     Download is proceeding
        /// </summary>
        Downloading,

        /// <summary>
        ///     Download has completed
        /// </summary>
        Completed,

        /// <summary>
        ///     Download is cancelled
        /// </summary>
        Cancelled,

        /// <summary>
        ///     An error occurred whilst downloading
        /// </summary>
        ErrorOccurred
    }
}