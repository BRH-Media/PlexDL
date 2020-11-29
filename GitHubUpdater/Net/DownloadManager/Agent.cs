using GitHubUpdater.Enums;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubUpdater.Net.DownloadManager
{
    public static class Agent
    {
        public static async Task<DownloadStatus> DoDownload(this Job downloadJob)
        {
            try
            {
                if (downloadJob == null) return DownloadStatus.NullJob;

                var handler = new HttpClientHandler();
                using var httpClient = new HttpClient(handler);
                using var request = new HttpRequestMessage(new HttpMethod("GET"), downloadJob.DownloadUri);
                var response = await httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsByteArrayAsync();

                //does the file already exist?
                if (File.Exists(downloadJob.DownloadPath))
                    File.Delete(downloadJob.DownloadPath); //delete it

                //flush bytes to specified file
                File.WriteAllBytes(downloadJob.DownloadPath, responseBody);

                //successfully downloaded
                return DownloadStatus.Downloaded;
            }
            catch
            {
                //ignore error
                return DownloadStatus.Errored;
            }
        }
    }
}