namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the type of control protocol that is used in streaming or downloading.
    /// </summary>
    public enum NetworkProtocol
    {
        /// <summary>
        /// The protocol type has not yet been determined.
        /// </summary>
        Undefined,
        /// <summary>
        /// The protocol type is HTTP. This includes HTTPv9, WMSP, and HTTP download.
        /// </summary>
        Http,
        /// <summary>
        /// The protocol type is Real Time Streaming Protocol (RTSP).
        /// </summary>
        Rtsp,
        /// <summary>
        /// The content is read from a file. The file might be local or on a remote share.
        /// </summary>
        File,
        /// <summary>
        /// The protocol type is multicast.
        /// </summary>
        Multicast
    }
}