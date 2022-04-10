namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the statistic data collected by the network source.
    /// </summary>
    public enum NetworkStatistics
    {
        /// <summary>
        /// The number of packets received.
        /// </summary>
        ReceivedPackets = 0,
        /// <summary>
        /// The number of packets lost.
        /// </summary>
        LostPackets,
        /// <summary>
        /// The number of requests to resend packets.
        /// </summary>
        ResendsRequested,
        /// <summary>
        /// The number of resent packets received.
        /// </summary>
        ResendsReceived,
        /// <summary>
        /// The total number of packets recovered by error correction.
        /// </summary>
        RecoveredPacketsByErrorCorrection,
        /// <summary>
        /// The total number of packets recovered by retransmission.
        /// </summary>
        RecoveredPacketsByRetransmission,
        /// <summary>
        /// The total number of packets returned to user, including recovered packets.
        /// </summary>
        OutPackets,
        /// <summary>
        /// The 10-second average receiving rate.
        /// </summary>
        ReceivingRate,
        /// <summary>
        /// The average bandwidth of the clip.
        /// </summary>
        AverageBandwidth,
        /// <summary>
        /// The total number of bytes received.
        /// </summary>
        BytesReceived,
        /// <summary>
        /// The type of control protocol used to receive the data. The value indicates a member of the PVS.MediaPlayer.NetworkProtocol enumeration.
        /// </summary>
        Protocol,
        /// <summary>
        /// The type of transport used to receive the data (0 = UDP, 1 = TCP).
        /// </summary>
        TransportType,
        /// <summary>
        /// The status of cache for a media file or entry (0 = unavailable, 1 = active writing, 2 = active complete).
        /// </summary>
        CacheState,
        /// <summary>
        /// The current link bandwidth, in bits per second.
        /// </summary>
        LinkBandwidth,
        /// <summary>
        /// The current bitrate of the content.
        /// </summary>
        ContentBitrate,
        /// <summary>
        /// The negotiated speed factor used in data transmission.
        /// </summary>
        SpeedFactor,
        /// <summary>
        /// The playout buffer size, in milliseconds.
        /// </summary>
        BufferSize,
        /// <summary>
        /// The percentage of the playout buffer filled during buffering. The value is an integer in the range 0–100.
        /// </summary>
        BufferProgress,
        /// <summary>
        /// The number of ticks since the last bandwidth switch.
        /// </summary>
        LastBandwidthSwitch,
        /// <summary>
        /// The start of the seekable range, in 100-nanosecond units,
        /// </summary>
        SeekRangeStart,
        /// <summary>
        /// The end of the seekable range, in 100-nanosecond units.
        /// </summary>
        SeekRangeEnd,
        /// <summary>
        /// The number of times buffering has occurred, including the initial buffering.
        /// </summary>
        BufferingCount,
        /// <summary>
        /// The number of packets that had incorrect signatures.
        /// </summary>
        IncorrectlySignedPackets,
        /// <summary>
        /// Boolean value indicating whether the current session is signed.
        /// </summary>
        SignedSession,
        /// <summary>
        /// The current maximum bitrate of the content.
        /// </summary>
        MaxBitrate,
        /// <summary>
        /// The reception quality.
        /// </summary>
        ReceptionQuality,
        /// <summary>
        /// The total number of recovered packets.
        /// </summary>
        RecoverdPackets,
        /// <summary>
        /// Boolean value indicating whether the content has a variable bitrate.
        /// </summary>
        VariableBitrate,
        /// <summary>
        /// The percentage of the content that has been downloaded. The value is an integer in the range 0–100.
        /// </summary>
        DownloadProgress
        //UNPREDEFINEDPROTOCOLNAME_ID
    }
}