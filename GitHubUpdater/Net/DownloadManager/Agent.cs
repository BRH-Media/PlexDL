using GitHubUpdater.Enums;
using PlexDL.AltoHTTP.Common.Net;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubUpdater.Net.DownloadManager
{
    public static class Agent
    {
        public static string UpdateDirectory { get; set; } = $@"{Globals.UpdateRootDir}";

        public static async Task<DownloadStatus> DoDownload(this Job downloadJob)
        {
            try
            {
                //don't execute the download if the job isn't valid
                if (downloadJob == null)
                    return DownloadStatus.NullJob;

                //use the generic resource downloader
                var responseBytes = await ResourceGrab.GrabBytes(downloadJob.DownloadUri, 0);

                //validate the downloaded bytes
                if (responseBytes != null)
                {
                    //make sure there are actually valid bytes
                    if (responseBytes.Length > 0)
                    {
                        //flush bytes to specified file
                        File.WriteAllBytes(downloadJob.DownloadPath, responseBytes);

                        //successfully downloaded
                        return DownloadStatus.Downloaded;
                    }
                }

                //if the checks above fail, they will land here
                return DownloadStatus.NullDownload;
            }
            catch (TaskCanceledException ex)
            {
                //log it
                ex.ExportError();

                //this only gets raised on cancellation and is therefore non-critical
                return DownloadStatus.Timeout;
            }
            catch (ThreadAbortException ex)
            {
                //log it
                ex.ExportError();

                //this only gets raised on cancellation and is therefore non-critical
                return DownloadStatus.Cancelled;
            }
            catch (Exception ex)
            {
                //export error
                ex.ExportError();

                //set status to errored
                return DownloadStatus.Errored;
            }
        }
    }
}