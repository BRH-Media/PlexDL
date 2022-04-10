namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the size and location of the video image on the player's display window.
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// Size: original size.
        /// Location: topleft of the player's display window.
        /// Display resize: shrink: no, grow: no.
        /// </summary>
        Original,
        /// <summary>
        /// Size: original size.
        /// Location: center of the player's display window.
        /// Display resize: shrink: no, grow: no.
        /// </summary>
        Center,
        /// <summary>
        /// Size: same size as the player's display window.
        /// Location: topleft of the player's display window.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        Stretch,
        /// <summary>
        /// Size: the largest possible size on the player's display window while maintaining the aspect ratio.
        /// Location: topleft of the player's display window.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        Zoom,
        /// <summary>
        /// Size: the largest possible size on player's display window while maintaining the aspect ratio.
        /// Location: center of the player's display window.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        ZoomCenter,
        /// <summary>
        /// Size: same size as the player's display window while maintaining the aspect ratio, but possibly with horizontal or vertical image cropping.
        /// Location: center of the player's display window.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        CoverCenter,
        /// <summary>
        /// Size: original size or the largest possible size on player's display window while maintaining the aspect ratio.
        /// Location: topleft of the player's display window.
        /// Display resize: shrink: yes, grow: if smaller than original size.
        /// </summary>
        SizeToFit,
        /// <summary>
        /// Size: original size or the largest possible size on the player's display window while maintaining the aspect ratio.
        /// Location: center of the player's display window.
        /// Display resize: shrink: yes, grow: if smaller than original size.
        /// </summary>
        SizeToFitCenter,
        /// <summary>
        /// Size: set manually.
        /// Location: set manually.
        /// Display resize: shrink: no, grow: no.
        /// </summary>
        Manual
    }
}