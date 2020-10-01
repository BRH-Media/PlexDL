namespace GitHubUpdater.Enums
{
    /// <summary>
    /// Represents whether GitHubUpdater should poll for 'pre-release' (Development) or 'release' (Stable)
    /// </summary>
    public enum UpdateChannel
    {
        /// <summary>
        /// Formal release builds
        /// </summary>
        Stable,

        /// <summary>
        /// Pre-release builds
        /// </summary>
        Development,

        /// <summary>
        /// Returned if the user didn't select a channel, etc.
        /// </summary>
        Unknown
    }
}