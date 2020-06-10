namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the area of the screen to copy.
    /// </summary>
    public enum ScreenCopyMode
    {
        /// <summary>
        /// The (visible part of the) video on the display window of the player.
        /// </summary>
        Video,

        /// <summary>
        /// The display window of the player.
        /// </summary>
        Display,

        /// <summary>
        /// The (parent) control that contains the display window of the player.
        /// </summary>
        Parent,

        /// <summary>
        /// The form that contains the display window of the player.
        /// </summary>
        Form,

        /// <summary>
        /// The (entire) screen that contains the display window of the player.
        /// </summary>
        Screen
    }
}