namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the size and position of a video image overlay.
    /// </summary>
    public enum ImagePlacement
    {
        /// <summary>
        /// The image is placed in the same size and position as the video.
        /// </summary>
        Stretch,
        /// <summary>
        /// The image is placed in the center of the video as large as possible while maintaining the aspect ratio.
        /// </summary>
        Zoom,
        /// <summary>
        /// The image is placed in the center of the video while maintaining the aspect ratio.
        /// </summary>
        Center,
        /// <summary>
        /// The image covers the entire video while maintaining the aspect ratio, possibly with horizontal or vertical cropping of the image.
        /// </summary>
        Cover,
        /// <summary>
        /// The image is placed in a small size in the top left corner of the video.
        /// </summary>
        TopLeftSmall,
        /// <summary>
        /// The image is placed in a medium size in the top left corner of the video.
        /// </summary>
        TopLeftMedium,
        /// <summary>
        /// The image is placed in a large size in the top left corner of the video.
        /// </summary>
        TopLeftLarge,
        /// <summary>
        /// The image is placed in a small size in the top right corner of the video.
        /// </summary>
        TopRightSmall,
        /// <summary>
        /// The image is placed in a medium size in the top right corner of the video.
        /// </summary>
        TopRightMedium,
        /// <summary>
        /// The image is placed in a large size in the top right corner of the video.
        /// </summary>
        TopRightLarge,
        /// <summary>
        /// The image is placed in a small size in the lower left corner of the video.
        /// </summary>
        BottomLeftSmall,
        /// <summary>
        /// The image is placed in a medium size in the lower left corner of the video.
        /// </summary>
        BottomLeftMedium,
        /// <summary>
        /// The image is placed in a large size in the lower left corner of the video.
        /// </summary>
        BottomLeftLarge,
        /// <summary>
        /// The bitmap is placed in a small size in the lower right corner of the video.
        /// </summary>
        BottomRightSmall,
        /// <summary>
        /// The image is placed in a medium size in the lower right corner of the video.
        /// </summary>
        BottomRightMedium,
        /// <summary>
        /// The bitmap is placed in a large size in the lower right corner of the video.
        /// </summary>
        BottomRightLarge,
        /// <summary>
        /// The image size and position are set according to the bounds parameter of the method.
        /// </summary>
        Custom
    }
}