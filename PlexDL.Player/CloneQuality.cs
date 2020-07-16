namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the video quality of the display clone.
    /// </summary>
    public enum CloneQuality
    {
        /// <summary>
        /// Specifies normal quality video.
        /// </summary>
        Normal,
        /// <summary>
        /// Specifies high quality video.
        /// </summary>
        High,
        /// <summary>
        /// Specifies automatic quality video: high quality video if the video image of the display clone is smaller than the original video image, otherwise normal video quality.
        /// </summary>
        Auto
    }
}