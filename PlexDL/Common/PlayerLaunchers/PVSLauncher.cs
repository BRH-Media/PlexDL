using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using PlexDL.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class PVSLauncher
    {
        public static void LaunchPVS(PlexObject stream, DataTable titles)
        {
            if (stream != null && titles != null)
                try
                {
                    //downloads won't work properly if you're streaming at the same time
                    if (!Home.IsDownloadRunning && !Home.IsEngineRunning)
                    {
                        Player frm = new Player();
                        frm.StreamingContent = stream;
                        frm.TitlesTable = titles;
                        LoggingHelpers.addToLog("Started streaming " + stream.StreamInformation.ContentTitle + " (PVS)");
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("You cannot stream \n" + stream.StreamInformation.ContentTitle + "\n because a download is already running. Cancel the download before attempting to stream within PlexDL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        LoggingHelpers.addToLog("Tried to stream content via PVS, but a download is running.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred whilst trying to launch VLC\n\n" + ex.ToString(), "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoggingHelpers.recordException(ex.Message, "VLCLaunchError");
                    return;
                }
            else
                LoggingHelpers.addToLog("Tried to stream content via PVS, but one or more values were null.");
        }
    }
}