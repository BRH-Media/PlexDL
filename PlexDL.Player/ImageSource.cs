namespace PlexDL.Player
{
    /// <summary>
    /// Specifies where to retrieve a media-related image.
    /// </summary>
    public enum ImageSource
    {
        /// <summary>
        /// No media-related image is retrieved.
        /// </summary>
        None,
        /// <summary>
        /// The media-related image is retrieved from the media file.
        /// </summary>
        MediaOnly,
        /// <summary>
        /// The media-related image is retrieved from the media file or, if not found, from the folder of the media file.
        /// </summary>
        MediaOrFolder,
        /// <summary>
        /// The media-related image is retrieved from the folder of the media file or, if not found, from the media file.
        /// </summary>
        FolderOrMedia,
        /// <summary>
        /// The media-related image is retrieved from the folder of the media file.
        /// </summary>
        FolderOnly
    }
}