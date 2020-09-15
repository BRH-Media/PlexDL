using PlexDL.Common.CastAPI.PlayQueue;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Net.Http;

namespace PlexDL.Common.CastAPI
{
    public static class PlayQueueHandler
    {
        public static QueueResult NewQueue(PlexObject content, PlexAPI.Server server)
        {
            var q = new QueueResult();

            try
            {
                var ip = server.address;
                var port = server.port;
                const string protocol = @"http";

                //POST data values

                //Constants
                const string type = @"video";
                const int shuffle = 0;
                const int repeat = 0;
                const int continuous = 0;
                const int own = 1;

                //Dynamic
                var uri = $"server://{server.machineIdentifier}/com.plexapp.plugins.library{content.ApiUri}";

                //assemble URLs
                var baseUri = $"{protocol}://{ip}:{port}";
                var resource = @"playQueues";

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                        $"{baseUri}/{resource}?type={type}&shuffle={shuffle}" +
                        $"&repeat={repeat}&continuous={continuous}" +
                        $"&own={own}&uri={uri}"))
                    {
                        request.Headers.TryAddWithoutValidation("X-Plex-Client-Identifier",
                            "AB6CCCC7-5CF5-4523-826A-B969E0FFD8A0");
                        request.Headers.TryAddWithoutValidation("X-Plex-Token", "PzhwzBRtb1jQqfRypxDo");

                        var response = httpClient.SendAsync(request).Result;

                        if (response.IsSuccessStatusCode)
                            if (response.Content != null)
                            {
                                var d = response.Content.ReadAsStringAsync().Result
                                    .ParseXml<MediaContainer>();

                                q.QueueId = d.playQueueID;
                                q.QueueSuccess = true;
                                q.QueueObject = d;
                                q.QueueUri = $"/playQueues/{d.playQueueID}?own=1&window=200";
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"QueueAddError");
            }

            return q;
        }
    }
}