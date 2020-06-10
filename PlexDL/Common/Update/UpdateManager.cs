using GitHubUpdater;

namespace PlexDL.Common.Update
{
    public static class UpdateManager
    {
        public static void RunUpdateCheck()
        {
            var updater = new UpdateClient()
            {
                Author = "brhsoftco",
                RepositoryName = "plexdl"
            };
        }
    }
}