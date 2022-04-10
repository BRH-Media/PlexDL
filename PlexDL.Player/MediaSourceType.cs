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
        /// Represents a memory byte stream.
        /// </summary>
        ByteArray,
        /// <summary>
        /// Represents a local image file.
        /// </summary>
        Image,
        /// <summary>
        /// Represents a remote (online) media file.
        /// </summary>
        RemoteFile,
        /// <summary>
        /// Represents an online live media stream (broadcast).
        /// </summary>
        LiveStream,
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
    }
}