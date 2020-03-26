using System;
using System.Data;
using System.Windows.Forms;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class PvsLauncher
    {
        public static void LaunchPvs(PlexObject stream, DataTable titles)
        {
            if (stream != null && titles != null)
                try
                {
                    if (Methods.StreamAdultContentCheck(stream))
                    {
                        //downloads won't work properly if you're streaming at the same time
                        if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
                        {
                            var frm = new UI.Player
                            {
                                StreamingContent = stream,
                                TitlesTable = titles
                            };
                            LoggingHelpers.AddToLog("Started streaming " + stream.StreamInformation.ContentTitle + " (PVS)");
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(
                                "You cannot stream \n" + stream.StreamInformation.ContentTitle +
                                "\n because a download is already running. Cancel the download before attempting to stream within PlexDL.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            LoggingHelpers.AddToLog("Tried to stream content via PVS, but a download is running.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex, "Launch Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                }
            else
                LoggingHelpers.AddToLog("Tried to stream content via PVS, but one or more values were null.");
        }
    }
}