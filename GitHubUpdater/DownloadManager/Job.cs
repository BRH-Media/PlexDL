using System;

namespace GitHubUpdater.DownloadManager
{
    public class Job
    {
        public string DownloadPath { get; set; } = @"";
        public long DownloadSize { get; set; } = 0;
        public Uri DownloadUri { get; set; } = null;
    }
}