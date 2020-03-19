namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the amount of noise reduction during seeking with the position slider of the player.
    /// </summary>
    public enum SilentSeek
    {
        /// <summary>
        /// The audio output is not muted during seeking.
        /// </summary>
        Never,

        /// <summary>
        /// The audio output is only muted during seeking when the slider is moved.
        /// </summary>
        OnMoving,

        /// <summary>
        /// The audio output is always muted during seeking.
        /// </summary>
        Always
    }
}