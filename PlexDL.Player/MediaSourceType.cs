namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the media source type.
    /// </summary>
    public enum MediaSourceType
    {
        /// <summary>
        /// Represents no media.
        /// </summary>
        None,

        /// <summary>
        /// Represents a local media file.
        /// </summary>
        File,

        /// <summary>
        /// Represents an online media file.
        /// </summary>
        FileStream,

        /// <summary>
        /// Represents a local image file.
        /// </summary>
        Image,

        /// <summary>
        /// Represents a memory byte stream.
        /// </summary>
        ByteArray,

        /// <summary>
        /// Represents a webcam device.
        /// </summary>
        Webcam,

        /// <summary>
        /// Represents a webcam device with audio input.
        /// </summary>
        WebcamWithAudio,

        /// <summary>
        /// Represents an audio input device.
        /// </summary>
        AudioInput,

        /// <summary>
        /// Represents an online live stream.
        /// </summary>
        LiveStream
    }
}