using PlexDL.PlexAPI.LoginHandler.Auth.JSON.Helpers;
using PlexDL.PlexAPI.LoginHandler.Globals;
using PlexDL.PlexAPI.LoginHandler.Net;

namespace PlexDL.PlexAPI.LoginHandler.Auth.JSON
{
    public static class PlexAuthHandler
    {
        public static PlexAuth NewPlexAuthPin()
        {
            var j = Get.DownloadJson(PlexEndpoints.PlexPinsEndpoint, RestSharp.Method.Post);
            return JsonValidation.IsValidJson(j) ? PlexAuth.FromJson(j) : null;
        }

        public static PlexAuth FromPinEndpoint(PlexAuth auth)
        {
            var j = Get.DownloadJson(auth.PinEndpointUrl);
            return JsonValidation.IsValidJson(j) ? PlexAuth.FromJson(j) : null;
        }
    }
}