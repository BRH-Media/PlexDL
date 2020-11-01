namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the area of the screen to copy.
    /// </summary>
    public enum CopyMode
    {
        /// <summary>
        /// The (visible part of the) video on the player's display window.
        /// </summary>
        Video,
        /// <summary>
        /// The player's display window.
        /// </summary>
        Display,
        /// <summary>
        /// The (parent) control that contains the player's display window.
        /// </summary>
        Parent,
        /// <summary>
        /// The form that contains the player's display window.
        /// </summary>
        Form,
        /// <summary>
        /// The (entire) screen that contains the player's display window.
        /// </summary>
        Screen
    }
}