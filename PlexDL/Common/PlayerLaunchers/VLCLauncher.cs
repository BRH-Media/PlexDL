using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using PlexDL.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class VLCLauncher
    {
        public static void LaunchVLC(PlexObject stream)
        {
            try
            {
                if (Methods.StreamAdultContentCheck(stream))
                {
                    var p = new Process();
                    var c = new SVarController();
                    var vlc = Home.Settings.Player.VLCMediaPlayerPath;
                    var arg = Home.Settings.Player.VLCMediaPlayerArgs;
                    c.Input = arg;
                    c.Variables = c.BuildFromDlInfo(stream.StreamInformation);
                    arg = c.YieldString();
                    p.StartInfo.FileName = vlc;
                    p.StartInfo.Arguments = arg;
                    p.Start();
                    LoggingHelpers.AddToLog("Started streaming " + stream.StreamInformation.ContentTitle + " (VLC)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                return;
            }
        }
    }
}