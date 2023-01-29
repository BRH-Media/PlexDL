using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Security;
using RestSharp;

namespace PlexDL.MyPlex
{
    public abstract class PlexRest
    {
        //Plex official API (not tied to a PMS)
        private const string BASE_URL = "https://my.plexapp.com";

        protected RestClient GetRestClient()
        {
            var client = new RestClient(GetBaseUrl());
            client.Options.UserAgent = NetGlobals.GlobalUserAgent;

            //if a timeout is specified, we need to apply it to the client
            if (NetGlobals.Timeout > 0)
                client.Options.MaxTimeout = NetGlobals.Timeout * 1000;

            return client;
        }

        protected virtual string GetBaseUrl()
        {
            return BASE_URL;
        }

        protected RestRequest AddPlexHeaders(RestRequest request)
        {
            request.AddHeader("X-Plex-Platform", "Windows");
            request.AddHeader("X-Plex-Platform-Version", "7");
            request.AddHeader("X-Plex-Provides", "player");
            request.AddHeader("X-Plex-Client-Identifier", GuidHandler.GetGlobalGuid().ToString());
            request.AddHeader("X-Plex-Product", "PlexWMC");
            request.AddHeader("X-Plex-Version", "0");

            return request;
        }

        public T Execute<T>(RestRequest request, RestClient client) where T : new()
        {
            client.UseXml();
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
                throw response.ErrorException;

            //Console.WriteLine(response.ResponseUri);
            return response.Data;
        }

        public T Execute<T>(RestRequest request, User user) where T : new()
        {
            var client = GetRestClient();

            request = AddPlexHeaders(request);

            request.AddParameter("X-Plex-Token", user.authenticationToken);

            return Execute<T>(request, client);
        }
    }
}