namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the display mode of the taskbar progress indicator.
    /// </summary>
    public enum TaskbarProgressMode
    {
        /// <summary>
        /// The taskbar progress indicator shows the progress of the playing media from Player.Media.StartTime to Player.Media.StopTime.
        /// </summary>
        Progress,

        /// <summary>
        /// The taskbar progress indicator shows the progress of the playing media from the natural beginning of the media to the natural end of the media.
        /// </summary>
        Track
    }
}