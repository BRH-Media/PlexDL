namespace PlexDL.Common.Structures
{
    public class StreamInfo
    {
        public StreamLinks Links { get; set; } = new StreamLinks();
        public string Container { get; set; } = "";
        public long ByteLength { get; set; } = 0;
        public long ContentDuration { get; set; } = 0;
        public string DownloadPath { get; set; } = "";
        public string FileName { get; set; } = "";
        public string ContentTitle { get; set; } = "";
        public string ContentThumbnailUri { get; set; } = "";
    }
}