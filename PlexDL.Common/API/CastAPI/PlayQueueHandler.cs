using PlexDL.Common.API.CastAPI.PlayQueue;
using PlexDL.Common.Logging;
using PlexDL.Common.Security;
using PlexDL.Common.Structures.Plex;
using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using UIHelpers;

namespace PlexDL.Common.API.CastAPI
{
    public static class PlayQueueHandler
    {
        public static QueueResult NewQueue(PlexObject content, MyPlex.Server server)
        {
            //return object
            var q = new QueueResult();

            try
            {
                //connection details for the server
                var ip = server.address;
                var port = server.port;
                const string protocol = @"http";

                //Constants
                const string type = @"VIDEO";
                const int shuffle = 0;
                const int repeat = 0;
                const int continuous = 0;
                const int own = 1;

                //Dynamic
                var uri = $"server://{server.machineIdentifier}/com.plexapp.plugins.library{content.ApiUri}";

                //assemble URLs
                var baseUri = $"{protocol}://{ip}:{port}";
                const string resource = @"playQueues";

                //HTTP request client
                using (var httpClient = new HttpClient())
                {
                    //new HTTP post request
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                        $"{baseUri}/{resource}?type={type}&shuffle={shuffle}" +
                        $"&repeat={repeat}&continuous={continuous}" +
                        $"&own={own}&uri={uri}"))
                    {
                        //add Plex client ID (unique GUID)
                        request.Headers.TryAddWithoutValidation("X-Plex-Client-Identifier",
                            GuidHandler.GetGlobalGuid().ToString());

                        //Plex authentication token
                        request.Headers.TryAddWithoutValidation("X-Plex-Token", server.accessToken);

                        //send the request and store the response
                        var response = httpClient.SendAsync(request).Result;

                        //did we succeed?
                        if (response.IsSuccessStatusCode)

                            //is the content valid?
                            if (response.Content != null)
                            {
                                //get the XML response string
                                var r = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                                //XML class processor
                                var deserialiser = new XmlSerializer(typeof(MediaContainer));

                                //XML string reader
                                var stringReader = new StringReader(r);

                                //deserialised MediaContainer
                                var result = (MediaContainer)deserialiser.Deserialize(stringReader);

                                //close streams
                                stringReader.Close();

                                //setup QueueResult variables
                                q.QueueId = result.playQueueID.ToString();
                                q.QueueSuccess = true;
                                q.QueueObject = result;
                                q.QueueUri = $"/playQueues/{result.playQueueID}?own=1&window=200";

                                //UIMessages.Info(d.playQueueID);
                                //UIMessages.Info(q.QueueId);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"QueueAddError");

                //alert the user
                UIMessages.Error($"Error obtaining a new PlayQueue:\n\n{ex}");
            }

            //return QueueResult
            return q;
        }
    }
}