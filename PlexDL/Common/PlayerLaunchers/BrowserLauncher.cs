using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class BrowserLauncher
    {
        public static void LaunchBrowser(DownloadInfo stream)
        {
            try
            {
                Process.Start(stream.Link);
                LoggingHelpers.AddToLog("Started streaming " + stream.ContentTitle + " (Browser)");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch the default browser\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "BrowserLaunchError");
                return;
            }
        }
    }
}