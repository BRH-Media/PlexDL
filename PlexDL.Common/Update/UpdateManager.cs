using GitHubUpdater;
using PlexDL.Common.Globals;
using System.Reflection;

namespace PlexDL.Common.Update
{
    public static class UpdateManager
    {
        public static void RunUpdateCheck(bool silentCheck = false)
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            var updater = new UpdateClient
            {
                Author = "brh-media",
                RepositoryName = "plexdl",
                CurrentInstalledVersion = version,
                DebugMode = Flags.IsDebug
            };

            updater.CheckIfLatest(silentCheck);
        }
    }
}