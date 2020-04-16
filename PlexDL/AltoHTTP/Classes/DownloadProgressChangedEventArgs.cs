namespace PlexDL.AltoHTTP.Classes
{
    /// <summary>
    ///     Download progress changed event arguments
    /// </summary>
    public class DownloadProgressChangedEventArgs
    {
        /// <summary>
        ///     Creates instance of the class
        /// </summary>
        /// <param name="progress">Current download progress</param>
        /// <param name="speed">Current download speed</param>
        public DownloadProgressChangedEventArgs(double progress, int speed, double bytesGet)
        {
            Progress = progress;
            Speed = speed;
            BytesReceived = bytesGet;
        }

        /// <summary>
        ///     Gets the current progress
        /// </summary>
        public double Progress { get; }

        /// <summary>
        ///     Gets the current speed
        /// </summary>
        public int Speed { get; }

        public double BytesReceived { get; }
    }
}