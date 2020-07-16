namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the blending (opacity) of display overlays on display clones or screen copies.
    /// </summary>
    public enum OverlayBlend
    {
        /// <summary>
        /// The display overlays are not blended.
        /// </summary>
        None,
        /// <summary>
        /// The display overlays are blended opaque.
        /// </summary>
        Opaque,
        /// <summary>
        /// The display overlays are blended transparent.
        /// </summary>
        Transparent
    }
}