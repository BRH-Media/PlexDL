namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the video output quality of webcams.
    /// </summary>
    public enum WebcamQuality
    {
        /// <summary>
        /// Represents the webcam's default video output format.
        /// </summary>
        Default,

        /// <summary>
        /// Represents a video output format with the highest possible resolution at a minimum frame rate of 15 fps.
        /// </summary>
        High,

        /// <summary>
        /// Represents a video output format with the lowest possible resolution with a minimum height of 100 pixels at a minimum frame rate of 15 fps.
        /// </summary>
        Low,

        /// <summary>
        /// Represents a video output format with the highest possible resolution regardless of the frame rate.
        /// </summary>
        Photo
    }
}