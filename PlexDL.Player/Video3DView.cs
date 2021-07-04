namespace PlexDL.Player
{
    /// <summary>
    /// Specifies how to display a side-by-side 3D stereoscopic video.
    /// </summary>
    public enum Video3DView
    {
        /// <summary>
        /// Displays the normal video image. The aspect ratio of the video is restored if one of the other Video3DView options was active.
        /// </summary>
        NormalImage,
        /// <summary>
        /// Displays the left half of the video. The aspect ratio of the video may also be adjusted.
        /// </summary>
        LeftImage,
        /// <summary>
        /// Displays the right half of the video. The video aspect ratio may also be adjusted.
        /// </summary>
        RightImage,
        /// <summary>
        /// Displays the top half of the video. The video aspect ratio may also be adjusted.
        /// </summary>
        TopImage,
        /// <summary>
        /// Displays the bottom half of the video. The video aspect ratio may also be adjusted.
        /// </summary>
        BottomImage
    }
}