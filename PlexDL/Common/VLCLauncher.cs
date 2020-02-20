using PlexDL.Common.Structures;
using PlexDL.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PlexDL.Common
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Home.recordException(ex.Message, "VLCLaunchError");
                return;
            }
        }
    }
}