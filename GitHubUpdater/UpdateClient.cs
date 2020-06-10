using RestSharp;
using System;

namespace GitHubUpdater
{
    public class UpdateClient
    {
        public string Author { get; set; } = "";
        public string RepositoryName { get; set; } = "";
        public string ApiUrl => $"repos/{Author}/{RepositoryName}/releases/latest";
        private string BaseUrl => "http://api.github.com/";

        protected RestClient GetRestClient()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(BaseUrl)
            };
            return client;
        }

        protected virtual string GetBaseUrl()
        {
            return BaseUrl;
        }
    }
}