using Newtonsoft.Json;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using RestSharp;
using System;
using System.IO;
using System.Linq;
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
                var token = server.accessToken;
                var ip = server.address;
                var port = server.port;
                var protocol = @"http";

                //POST data values
                var type = @"video";
                var shuffle = 0;
                var repeat = 0;
                var continuous = 0;
                var own = 1;
                var uri = $"server://{server.machineIdentifier}/com.plexapp.plugins.library{content.ApiUri}";

                //assemble URLs
                var baseUri = $"{protocol}://{ip}:{port}";
                var resource = @"playQueues";

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{baseUri}/{resource}?type={type}&shuffle={shuffle}" +
                                                                                        $"&repeat={repeat}&continuous={continuous}" +
                                                                                        $"&own={own}&uri={uri}"))
                    {
                        request.Headers.TryAddWithoutValidation("X-Plex-Client-Identifier", "AB6CCCC7-5CF5-4523-826A-B969E0FFD8A0");
                        request.Headers.TryAddWithoutValidation("X-Plex-Token", "PzhwzBRtb1jQqfRypxDo");

                        var response = httpClient.SendAsync(request).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            if (response.Content != null)
                            {
                                var d = response.Content.ReadAsStringAsync().Result
                                    .ParseXML<PlayQueue.MediaContainer>();

                                q.QueueId = d.playQueueID;
                                q.QueueSuccess = true;
                                q.QueueObject = d;
                                q.QueueUri = $"/playQueues/{d.playQueueID}?own=1&window=200";
                            }
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

        private static void LogRequest(RestClient client, IRestRequest request, IRestResponse response)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = client.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            File.WriteAllText(@"qDebug_Request.log",
                $"Request completed\n\nRequest:\n{JsonConvert.SerializeObject(requestToLog, Formatting.Indented)}\n\nResponse:\n{JsonConvert.SerializeObject(responseToLog, Formatting.Indented)}");
        }
    }
}