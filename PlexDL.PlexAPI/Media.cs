using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class Media
    {
        public int id { get; set; }
        public long duration { get; set; }
        public int bitrate { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public float aspectRatio { get; set; }
        public int audioChannels { get; set; }
        public string audioCodec { get; set; }
        public string videoCodec { get; set; }
        public string videoResolution { get; set; }
        public string container { get; set; }
        public string videoFrameRate { get; set; }
        public List<Part> parts { get; set; }
    }
}