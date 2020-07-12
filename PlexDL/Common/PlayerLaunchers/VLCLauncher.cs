using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PlexDL.Common.Globals.Providers;
using UIHelpers;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class VlcLauncher
    {
        public static void LaunchVlc(PlexObject stream)
        {
            try
            {
                if (!Methods.StreamAdultContentCheck(stream)) return;

                var p = new Process();
                var c = new SVarController();
                var vlc = ObjectProvider.Settings.Player.VlcMediaPlayerPath;
                var arg = ObjectProvider.Settings.Player.VlcMediaPlayerArgs;
                if (VlcInstalled())
                {
                    c.Input = arg;
                    c.Variables = c.BuildFromDlInfo(stream.StreamInformation);
                    arg = c.YieldString();
                    p.StartInfo.FileName = vlc;
                    p.StartInfo.Arguments = arg;
                    p.Start();
                    LoggingHelpers.RecordGeneralEntry("Started streaming " + stream.StreamInformation.ContentTitle + " (VLC)");
                }
                else
                {
                    UIMessages.Error(@"PlexDL could not find VLC Media Player. Please locate VLC and then try again.");
                    var ofd = new OpenFileDialog
                    {
                        Filter = @"vlc.exe",
                        Title = @"Locate VLC Media Player",
                        Multiselect = false,
                        FileName = @"vlc.exe"
                    };

                    if (ofd.ShowDialog() != DialogResult.OK) return;

                    var fileName = ofd.FileName;
                    var baseName = Path.GetFileName(fileName);
                    if (baseName == "vlc.exe")
                    {
                        ObjectProvider.Settings.Player.VlcMediaPlayerPath = fileName;
                        LaunchVlc(stream);
                    }
                    else
                    {
                        UIMessages.Error(@"Invalid VLC Media Player executable");
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst trying to launch VLC\n\n" + ex, @"Launch Error");
                LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
            }
        }

        public static bool VlcInstalled()
        {
            return File.Exists(ObjectProvider.Settings.Player.VlcMediaPlayerPath);
        }
    }
}