using RestSharp;
using RestSharp.Authenticators;
using System;

namespace PlexDL.PlexAPI
{
    public abstract class PlexRest
    {
        private const string BaseUrl = "https://my.plexapp.com";

        protected RestClient GetRestClient()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(GetBaseUrl());
            return client;
        }

        protected virtual string GetBaseUrl()
        {
            return BaseUrl;
        }

        protected RestRequest AddPlexHeaders(RestRequest request)
        {
            request.AddHeader("X-Plex-Platform", "Windows");
            request.AddHeader("X-Plex-Platform-Version", "7");
            request.AddHeader("X-Plex-Provides", "player");
            request.AddHeader("X-Plex-Client-Identifier", "AB6CCCC7-5CF5-4523-826A-B969E0FFD8A0");
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

        public T Execute<T>(RestRequest request, string username, string password) where T : new()
        {
            var client = GetRestClient();

            request = AddPlexHeaders(request);

            client.Authenticator = new HttpBasicAuthenticator(username, password);

            return Execute<T>(request, client);
        }

        public string Execute(RestRequest request, RestClient client)
        {
            var response = client.Execute(request);

            if (response.ErrorException != null)
                throw response.ErrorException;

            return response.Content;
        }

        public string Execute(RestRequest request, User user)
        {
            var client = GetRestClient();

            request = AddPlexHeaders(request);

            request.AddParameter("X-Plex-Token", user.authenticationToken);

            return Execute(request, client);
        }

        public string Execute(RestRequest request, string username, string password)
        {
            var client = GetRestClient();

            request = AddPlexHeaders(request);

            client.Authenticator = new HttpBasicAuthenticator(username, password);

            return Execute(request, client);
        }
    }
}