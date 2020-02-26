namespace PlexDL.Common.Structures.AppOptions.Caching
{
    public class CachePaths
    {
        public string ServerCachePath { get; set; } = @"cache\%TOKEN%";
        public string ThumbCachePath { get; set; } = @"cache\%TOKEN%\%SERVER%\thumb";
        public string XMLCachePath { get; set; } = @"cache\%TOKEN%\%SERVER%\xml";
    }
}