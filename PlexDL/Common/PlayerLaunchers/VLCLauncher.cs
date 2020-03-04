using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class VLCLauncher
    {
        public static void LaunchVLC(DownloadInfo stream)
        {
            try
            {
                Process p = new Process();
                SVarController c = new SVarController();
                string vlc = Home.settings.Player.VLCMediaPlayerPath;
                string arg = Home.settings.Player.VLCMediaPlayerArgs;
                c.Input = arg;
                c.Variables = c.BuildFromDlInfo(stream);
                arg = c.YieldString();
                p.StartInfo.FileName = vlc;
                p.StartInfo.Arguments = arg;
                p.Start();
                LoggingHelpers.AddToLog("Started streaming " + stream.ContentTitle + " (VLC)");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                return;
            }
        }
    }
}