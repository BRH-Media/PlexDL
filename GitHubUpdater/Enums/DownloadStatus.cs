namespace GitHubUpdater.Enums
{
    /// <summary>
    /// Represents the download status of a download job.
    /// </summary>
    public enum DownloadStatus
    {
        /// <summary>
        /// The job was successful.
        /// </summary>
        Downloaded,

        /// <summary>
        /// An error occurred whilst downloading (critical).
        /// </summary>
        Errored,

        /// <summary>
        /// The user/system cancelled the download.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The system cancelled the download due to a HTTP timeout occurring
        /// </summary>
        Timeout,

        /// <summary>
        /// Represents an unknown job status.
        /// </summary>
        Unknown,

        /// <summary>
        /// The job object provided was null, meaning the download immediately failed.
        /// </summary>
        NullJob,

        /// <summary>
        /// The downloaded job asset was null
        /// </summary>
        NullDownload
    }
}