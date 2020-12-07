using GitHubUpdater.Enums;
using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Security.Hashing;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubUpdater.Net.DownloadManager
{
    public static class Agent
    {
        public static string UpdateDirectory { get; set; } = @"";

        public static async Task<DownloadStatus> DoDownload(this Job downloadJob)
        {
            try
            {
                //don't execute the download if the job isn't valid
                if (downloadJob == null)
                    return DownloadStatus.NullJob;

                //use the generic resource downloader
                var responseBytes = await ResourceGrab.GrabBytes(downloadJob.DownloadUri);

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
            catch (TaskCanceledException)
            {
                //this only gets raised on cancellation and is therefore non-critical
                return DownloadStatus.Cancelled;
            }
            catch (ThreadAbortException)
            {
                //this only gets raised on cancellation and is therefore non-critical
                return DownloadStatus.Cancelled;
            }
            catch (Exception ex)
            {
                //export error
                ExportError(ex);

                //set status to errored
                return DownloadStatus.Errored;
            }

            //default
            return DownloadStatus.Unknown;
        }

        public static void ExportError(Exception ex)
        {
            try
            {
                //file name is MD5-hashed current date and time (for uniqueness)
                var fileName = $"UpdateError_{MD5Helper.CalculateMd5Hash(DateTime.Now.ToString(CultureInfo.CurrentCulture))}.log";

                //store all errors in the UpdateDirectory
                var errorsPath = $@"{UpdateDirectory}\errors";

                //ensure the 'errors' folder exists
                if (!Directory.Exists(errorsPath))
                    Directory.CreateDirectory(errorsPath);

                //full path
                var filePath = $@"{errorsPath}\{fileName}";

                //export error to log
                File.WriteAllText(filePath, ex.ToString());
            }
            catch
            {
                //ignore
            }
        }
    }
}