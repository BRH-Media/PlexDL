using GitHubUpdater;
using System;
using System.Windows.Forms;

namespace PlexDL.Common.Update
{
    public static class UpdateManager
    {
        public static void RunUpdateCheck()
        {
            var version = new Version(Application.ProductVersion.TrimEnd('.', '*'));
            var updater = new UpdateClient()
            {
                Author = "brhsoftco",
                RepositoryName = "plexdl",
                CurrentInstalledVersion = version
            };

            updater.CheckIfLatest();
        }
    }
}