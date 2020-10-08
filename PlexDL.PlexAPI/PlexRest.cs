using PlexDL.Common.Security;
using RestSharp;
using System;

namespace PlexDL.MyPlex
{
    public abstract class PlexRest
    {
        //Plex official API (not tied to a PMS)
        private const string BASE_URL = "https://my.plexapp.com";

        protected RestClient GetRestClient()
        {
            var client = new RestClient { BaseUrl = new Uri(GetBaseUrl()) };
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