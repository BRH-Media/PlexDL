using Newtonsoft.Json;

namespace PlexDL.PlexAPI.LoginHandler.Auth.JSON
{
    public partial class Location
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("postal_code")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PostalCode { get; set; }

        [JsonProperty("subdivisions")]
        public string Subdivisions { get; set; }

        [JsonProperty("coordinates")]
        public string Coordinates { get; set; }
    }
}