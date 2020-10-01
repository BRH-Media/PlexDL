namespace GitHubUpdater.Enums
{
    public enum VersionStatus
    {
        /// <summary>
        /// Current version is lower than what GitHub reports
        /// </summary>
        Outdated,

        /// <summary>
        /// Current version is level with what GitHub reports
        /// </summary>
        UpToDate,

        /// <summary>
        /// Current version is above what GitHub reports
        /// </summary>
        Bumped,

        /// <summary>
        /// An invalid or incomplete response was received from GitHub or one wasn't received at all
        /// </summary>
        Undetermined
    }
}