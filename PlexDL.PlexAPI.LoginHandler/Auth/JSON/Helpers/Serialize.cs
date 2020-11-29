using Newtonsoft.Json;

namespace PlexDL.PlexAPI.LoginHandler.Auth.JSON.Helpers
{
    public static class Serialize
    {
        public static string ToJson(this PlexAuth self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}