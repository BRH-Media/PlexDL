using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using UIHelpers;

namespace PlexDL.Common.PlayerLaunchers
{
    public static class PvsLauncher
    {
        public static void LaunchPvs(PlexObject stream)
        {
            if (stream != null)
                try
                {
                    if (!Methods.StreamAdultContentCheck(stream)) return;

                    //downloads won't work properly if you're streaming at the same time
                    if (!Flags.IsDownloadRunning && !Flags.IsEngineRunning)
                    {
                        var frm = new UI.Player
                        {
                            StreamingContent = stream
                        };
                        LoggingHelpers.RecordGeneralEntry("Started streaming " + stream.StreamInformation.ContentTitle + " (PVS)");
                        frm.ShowDialog();
                    }
                    else
                    {
                        UIMessages.Warning(
                            "You cannot stream \n" + stream.StreamInformation.ContentTitle +
                            "\n because a download is already running. Cancel the download before attempting to stream within PlexDL.",
                            @"Validation Error");
                        LoggingHelpers.RecordGeneralEntry("Tried to stream content via PVS, but a download is running.");
                    }
                }
                catch (Exception ex)
                {
                    UIMessages.Error("Error occurred whilst trying to launch VLC\n\n" + ex, @"Launch Error");
                    LoggingHelpers.RecordException(ex.Message, "VLCLaunchError");
                }
            else
                LoggingHelpers.RecordGeneralEntry("Tried to stream content via PVS, but one or more values were null.");
        }
    }
}