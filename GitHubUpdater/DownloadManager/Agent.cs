using System.IO;
using System.Net.Http;

namespace GitHubUpdater.DownloadManager
{
    public static class Agent
    {
        public static ReturnStatus DoDownload(Job downloadJob)
        {
            try
            {
                if (downloadJob == null) return ReturnStatus.NullJob;

                var handler = new HttpClientHandler();
                using var httpClient = new HttpClient(handler);
                using var request = new HttpRequestMessage(new HttpMethod("GET"), downloadJob.DownloadUri);
                var response = httpClient.SendAsync(request).Result;
                var responseBody = response.Content.ReadAsByteArrayAsync().Result;

                //does the file already exist?
                if (File.Exists(downloadJob.DownloadPath))
                    File.Delete(downloadJob.DownloadPath); //delete it

                //flush bytes to specified file
                File.WriteAllBytes(downloadJob.DownloadPath, responseBody);

                //successfully downloaded
                return ReturnStatus.Downloaded;
            }
            catch
            {
                //ignore error
                return ReturnStatus.Errored;
            }
        }
    }
}