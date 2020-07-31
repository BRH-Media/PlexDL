namespace PlexDL.Player
{
    /// <summary>
    /// Specifies the state of the progress indicator in the Windows taskbar.
    /// </summary>
    public enum TaskbarProgressState
    {
        /// <summary>
        /// No progress indicator is displayed in the taskbar button.
        /// </summary>
        NoProgress = 0,

        /// <summary>
        /// A pulsing green indicator is displayed in the taskbar button.
        /// </summary>
        Indeterminate = 0x1,

        /// <summary>
        /// A green progress indicator is displayed in the taskbar button.
        /// </summary>
        Normal = 0x2,

        /// <summary>
        /// A red progress indicator is displayed in the taskbar button.
        /// </summary>
        Error = 0x4,

        /// <summary>
        /// A yellow progress indicator is displayed in the taskbar button.
        /// </summary>
        Paused = 0x8
    }
}