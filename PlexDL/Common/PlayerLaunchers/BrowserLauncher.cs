using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Diagnostics;
using UIHelpers;

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
                    Process.Start(stream.StreamInformation.Links.View.ToString()); //normal MP4 stream (this won't trigger a browser download if it's a supported file)
                    LoggingHelpers.RecordGeneralEntry("Started streaming " + stream.StreamInformation.ContentTitle + " (Browser)");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Error occurred whilst trying to launch the default browser\n\n" + ex, @"Launch Error");
                LoggingHelpers.RecordException(ex.Message, "BrowserLaunchError");
            }
        }
    }
}