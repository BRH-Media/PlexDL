namespace PlexDL.Common.Structures.AppOptions.Caching
{
    public class CachingSettings
    {
        public CacheMode Mode { get; set; } = new CacheMode();
        public CachePaths Paths { get; set; } = new CachePaths();
    }
}