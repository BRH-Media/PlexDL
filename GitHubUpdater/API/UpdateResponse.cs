using System;

namespace GitHubUpdater.API
{
    public class UpdateResponse
    {
        public Version CurrentVersion { get; set; } = null;
        public DateTime Generated { get; set; } = DateTime.Now;
        public Application UpdateData { get; set; } = null;
    }
}