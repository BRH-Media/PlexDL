using PlexDL.PlexAPI.LoginHandler;
using RestSharp;

namespace PlexDL.PlexAPI.LoginHandler
{
    public static class Get
    {
        public static string DownloadJson(string url, Method method = Method.GET)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Plex-Product", PlexDefs.Product);
            request.AddHeader("X-Plex-Version", PlexDefs.Version);
            request.AddHeader("X-Plex-Client-Identifier", PlexDefs.ClientID);
            request.AddHeader("X-Plex-Model", PlexDefs.Model);
            IRestResponse response = client.Execute(request);

            // if string with JSON data is not empty, deserialize it to class and return its instance
            return !string.IsNullOrEmpty(response.Content) ? response.Content : string.Empty;
        }
    }
}