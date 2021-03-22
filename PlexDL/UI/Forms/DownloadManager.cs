using PlexDL.AltoHTTP.Common.Downloader;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using System;
using UIHelpers;

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class DownloadManager : DoubleBufferedForm
    {
        public HttpDownloadQueue QueueProvider { get; set; }

        public DownloadManager()
            => InitializeComponent();

        public static void ShowDownloadManager(HttpDownloadQueue queue)
        {
            try
            {
                //show the form
                new DownloadManager
                {
                    QueueProvider = queue
                }.ShowDialog();
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"DownloadManagerShowError");

                //alert the user
                UIMessages.Error($"Error in download manager show:\n\n{ex}");
            }
        }

        private void DoLoad()
        {
            try
            {
                //validate queue
                if (QueueProvider != null)
                {
                    //go through each QueueElement
                }
                else
                {
                    //log the error
                    LoggingHelpers.RecordException(@"Null HTTP Queue provider", @"DownloadManagerLoadError");

                    //alert user
                    UIMessages.Warning(@"Null HTTP Queue provider was specified to the Download Manager; couldn't load the form correctly.");

                    //close the form
                    Close();
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"DownloadManagerLoadError");

                //alert the user
                UIMessages.Error($"Error in download manager load:\n\n{ex}");

                //close the form
                Close();
            }
        }

        private void DownloadManager_Load(object sender, EventArgs e)
            => DoLoad();
    }
}