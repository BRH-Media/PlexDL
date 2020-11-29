using System;

namespace GitHubUpdater.Net.DownloadManager
{
    /// <summary>
    /// Represents a file to download regarding an update from the GitHub API
    /// </summary>
    public class Job
    {
        /// <summary>
        /// The path to save the downloaded file to
        /// </summary>
        public string DownloadPath { get; set; } = @"";

        /// <summary>
        /// The size of the file to download
        /// </summary>
        public long DownloadSize { get; set; } = 0;

        /// <summary>
        /// The online location of the file to download
        /// </summary>
        public Uri DownloadUri { get; set; } = null;
    }
}