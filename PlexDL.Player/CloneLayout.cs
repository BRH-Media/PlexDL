namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the size and location of the video image of the display clone.
    /// </summary>
    public enum CloneLayout
    {
        /// <summary>
        /// The video image is stretched across the display of the clone.
        /// </summary>
        Stretch,
        /// <summary>
        /// The video image is maximally enlarged and centered within the display of the clone while maintaining the aspect ratio.
        /// </summary>
        Zoom,
        /// <summary>
        /// The video image completely fills the display of the clone while maintaining the aspect ratio, but possibly with horizontal or vertical image cropping.
        /// </summary>
        Cover
    }
}