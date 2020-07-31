using Newtonsoft.Json;
using SharpCaster.JsonConverters;
using SharpCaster.Models.Enums;
using SharpCaster.Models.MediaStatus;
using System.Collections.Generic;

namespace SharpCaster.Models.Metadata
{
    //Fields: https://developers.google.com/cast/docs/reference/chrome/chrome.cast.media.MovieMediaMetadata
    public class MovieMediaMetadata : IMetadata
    {
        public MovieMediaMetadata()
        {
            metadataType = MetadataTypeEnum.MOVIE;
        }

        public List<ChromecastImage> images { get; set; }

        [JsonConverter(typeof(MetadataTypeEnumConverter))]
        public MetadataTypeEnum metadataType { get; set; }

        public string releaseDate { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
    }
}