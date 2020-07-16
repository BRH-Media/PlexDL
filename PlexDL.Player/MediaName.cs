namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the part of the file name of the playing media to be obtained.
    /// </summary>
    public enum MediaName
    {
        /// <summary>
        /// The file name without path and extension of the playing media.
        /// </summary>
        FileNameWithoutExtension,
        /// <summary>
        /// The file name and extension without path of the playing media.
        /// </summary>
        FileName,
        /// <summary>
        /// The file name with path and extension of the playing media.
        /// </summary>
        FullPath,
        /// <summary>
        /// The extension of the file name of the playing media.
        /// </summary>
        Extension,
        /// <summary>
        /// The path (directory) of the file name of the playing media.
        /// </summary>
        DirectoryName,
        /// <summary>
        /// The root path (root directory) of the file name of the playing media.
        /// </summary>
        PathRoot
    }
}