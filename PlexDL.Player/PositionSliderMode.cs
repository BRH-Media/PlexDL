namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the display mode of the positionslider controlled by the player.
    /// </summary>
    public enum PositionSliderMode
    {
        /// <summary>
        /// The positionslider shows the playback position of the playing media from Player.Media.StartTime to Player.Media.StopTime.
        /// </summary>
        Progress,
        /// <summary>
        /// The positionslider shows the playback position from the natural beginning of the media to the natural end of the media.
        /// </summary>
        Track
    }
}