namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the reason that media has stopped playing.
    /// </summary>
    public enum StopReason
    {
        /// <summary>
        /// The media has stopped because it has reached its natural end or stop position.
        /// </summary>
        Finished,
        /// <summary>
        /// The media has been stopped by the player to play other media.
        /// </summary>
        AutoStop,
        /// <summary>
        /// The media has been stopped using the player's stop method.
        /// </summary>
        UserStop,
        /// <summary>
        /// The media has stopped because an error has occurred.
        /// </summary>
        Error
    }
}