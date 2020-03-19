namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the area of the screen to copy.
    /// </summary>
    public enum ScreenCopyMode
    {
        /// <summary>
        /// The (visible part of the) video on the display of the player (custom copy: external items that are displayed on top of the video are not copied).
        /// </summary>
        Video,

        /// <summary>
        /// The display of the player (custom copy: external items that are displayed on top of the display are not copied).
        /// </summary>
        Display,

        /// <summary>
        /// The (parent) control that contains the display of the player.
        /// </summary>
        Parent,

        /// <summary>
        /// The display area of the form that contains the display of the player.
        /// </summary>
        Form,

        /// <summary>
        /// The (entire) screen that contains the display of the player.
        /// </summary>
        Screen
    }
}