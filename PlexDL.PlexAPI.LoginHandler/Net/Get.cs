using PlexDL.AltoHTTP.Common.Net;
using PlexDL.PlexAPI.LoginHandler.Globals;
using RestSharp;

namespace PlexDL.PlexAPI.LoginHandler.Net
{
    public static class Get
    {
        public static string DownloadJson(string url, Method method = Method.Get)
        {
            //setup the request and the client with the global user agent
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = method
            };

            //if a timeout is specified, we need to apply it to the client
            if (NetGlobals.Timeout > 0)
                client.Options.MaxTimeout = NetGlobals.Timeout * 1000;

            //add required Plex headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Plex-Product", PlexDefinitions.Product);
            request.AddHeader("X-Plex-Version", PlexDefinitions.Version);
            request.AddHeader("X-Plex-Client-Identifier", PlexDefinitions.ClientId);
            request.AddHeader("X-Plex-Model", PlexDefinitions.Model);

            //execute the request and store the response from the server
            var response = client.Execute(request);

            // if string with JSON data is not empty, deserialize it to class and return its instance
            return !string.IsNullOrWhiteSpace(response.Content)
                ? response.Content
                : string.Empty;
        }
    }
}