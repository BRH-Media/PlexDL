using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class BrowserLauncher
    {
        public static void LaunchBrowser(PlexObject stream)
        {
            try
            {
                if (Methods.StreamAdultContentCheck(stream))
                {
                    Process.Start(stream.StreamInformation.Link);
                    LoggingHelpers.RecordGeneralEntry("Started streaming " + stream.StreamInformation.ContentTitle + " (Browser)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch the default browser\n\n" + ex, "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "BrowserLaunchError");
            }
        }
    }
}