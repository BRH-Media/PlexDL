namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the full screen mode of the player.
    /// </summary>
    public enum FullScreenMode
    {
        // the order of these items should not be changed.

        /// <summary>
        /// The display of the player is shown full screen on the screen containing the display.
        /// </summary>
        Display,

        /// <summary>
        /// The (parent) control that contains the display of the player is shown full screen on the screen containing the parent control.
        /// </summary>
        Parent,

        /// <summary>
        /// The form that contains the display of the player is shown full screen on the screen containing the (largest part of) the form.
        /// </summary>
        Form,

        /// <summary>
        /// The display of the player is shown full screen on the system's virtual screen (all screens).
        /// </summary>
        Display_AllScreens,

        /// <summary>
        /// The (parent) control that contains the display of the player is shown full screen on the system's virtual screen (all screens).
        /// </summary>
        Parent_AllScreens,

        /// <summary>
        /// The form containing the display of the player is shown full screen on the system's virtual screen (all screens).
        /// </summary>
        Form_AllScreens
    }
}