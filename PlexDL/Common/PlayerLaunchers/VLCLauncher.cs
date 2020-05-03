using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class VlcLauncher
    {
        public static void LaunchVlc(PlexObject stream)
        {
            try
            {
                if (Methods.StreamAdultContentCheck(stream))
                {
                    var p = new Process();
                    var c = new SVarController();
                    var vlc = GlobalStaticVars.Settings.Player.VLCMediaPlayerPath;
                    var arg = GlobalStaticVars.Settings.Player.VLCMediaPlayerArgs;
                    if (VlcInstalled())
                    {
                        c.Input = arg;
                        c.Variables = c.BuildFromDlInfo(stream.StreamInformation);
                        arg = c.YieldString();
                        p.StartInfo.FileName = vlc;
                        p.StartInfo.Arguments = arg;
                        p.Start();
                        LoggingHelpers.RecordGenericEntry("Started streaming " + stream.StreamInformation.ContentTitle + " (VLC)");
                    }
                    else
                    {
                        MessageBox.Show(@"PlexDL could not find VLC Media Player. Please locate VLC and then try again.");
                        var ofd = new OpenFileDialog
                        {
                            Filter = @"vlc.exe",
                            Title = @"Locate VLC Media Player",
                            Multiselect = false,
                            FileName = @"vlc.exe"
                        };
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            var fileName = ofd.FileName;
                            var baseName = Path.GetFileName(fileName);
                            if (baseName == "vlc.exe")
                            {
                                GlobalStaticVars.Settings.Player.VLCMediaPlayerPath = fileName;
                                LaunchVlc(stream);
                            }
                            else
                            {
                                MessageBox.Show(@"Invalid VLC Media Player executable", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex, "Launch Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
            }
        }

        public static bool VlcInstalled()
        {
            return File.Exists(GlobalStaticVars.Settings.Player.VLCMediaPlayerPath);
        }
    }
}