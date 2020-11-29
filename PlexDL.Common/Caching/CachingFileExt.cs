namespace PlexDL.Common.Caching
{
    /// <summary>
    /// Contains the definitions of caching file extensions; these are not to be hardcoded elsewhere.
    /// </summary>
    public static class CachingFileExt
    {
        /// <summary>
        /// The extension of cached XML files (known as API XML documents)
        /// </summary>
        public static string ApiXmlExt { get; set; } = @".xml";

        /// <summary>
        /// The extension of cached images (known as thumbs, thumbnails or posters)
        /// </summary>
        public static string ThumbExt { get; set; } = @".thumb";

        /// <summary>
        /// The extension of cached server lists (cached list of Server objects returned from Plex.tv)
        /// </summary>
        public static string ServerListExt { get; set; } = @".slst";
    }
}