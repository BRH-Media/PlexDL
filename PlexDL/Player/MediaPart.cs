namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the part of the playing media whose duration is to be obtained.
    /// </summary>
    public enum MediaPart
    {
        /// <summary>
        /// The total duration of the playing media.
        /// </summary>
        BeginToEnd,

        /// <summary>
        /// The duration of the playing media from its natural beginning to the current position.
        /// </summary>
        FromBegin,

        /// <summary>
        /// The duration of the playing media from its start time to the current position.
        /// </summary>
        FromStart,

        /// <summary>
        /// The duration of the playing media from the current position to its natural end time.
        /// </summary>
        ToEnd,

        /// <summary>
        /// The duration of the playing media from the current position to its stop time.
        /// </summary>
        ToStop,

        /// <summary>
        /// The duration of the playing media from its start time to its stop time.
        /// </summary>
        StartToStop,
    }
}