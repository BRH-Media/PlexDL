using PlexDL.Common.Structures;
using System.Diagnostics;

namespace PlexDL.Common
{
    public static class BrowserLauncher
    {
        public static void LaunchBrowser(DownloadInfo stream)
        {
            Process.Start(stream.Link);
        }
    }
}