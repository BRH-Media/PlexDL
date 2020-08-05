using GitHubUpdater;
using System.Reflection;

namespace PlexDL.Common.Update
{
    public static class UpdateManager
    {
        public static void RunUpdateCheck(bool silentCheck = false)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var updater = new UpdateClient
            {
                Author = "brhsoftco",
                RepositoryName = "plexdl",
                CurrentInstalledVersion = version,
                DebugMode = Flags.IsDebug
            };

            updater.CheckIfLatest(silentCheck);
        }
    }
}