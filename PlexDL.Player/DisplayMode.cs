namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the size and location of the video image on the display of the player.
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// Size: original size.
        /// Location: topleft of the display of the player.
        /// Display resize: shrink: no, grow: no.
        /// </summary>
        Normal,

        /// <summary>
        /// Size: original size.
        /// Location: center of the display of the player.
        /// Display resize: shrink: no, grow: no.
        /// </summary>
        Center,

        /// <summary>
        /// Size: same size as the display of the player.
        /// Location: topleft of the display of the player.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        Stretch,

        /// <summary>
        /// Size: the largest possible size within the display of the player while maintaining the aspect ratio.
        /// Location: topleft of the display of the player.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        Zoom,

        /// <summary>
        /// Size: the largest possible size within the display of the player while maintaining the aspect ratio.
        /// Location: center of the display of the player.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        ZoomCenter,

        /// <summary>
        /// Size: same size as the display of the player while maintaining the aspect ratio, but possibly with horizontal or vertical image cropping.
        /// Location: center of the display of the player.
        /// Display resize: shrink: yes, grow: yes.
        /// </summary>
        CoverCenter,

        /// <summary>
        /// Size: original size or the largest possible size within the display of the player while maintaining the aspect ratio.
        /// Location: topleft of the display of the player.
        /// Display resize: shrink: yes, grow: if smaller than original size.
        /// </summary>
        SizeToFit,

        /// <summary>
        /// Size: original size or the largest possible size within the display of the player while maintaining the aspect ratio.
        /// Location: center of the display of the player.
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