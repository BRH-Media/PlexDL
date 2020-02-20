namespace PlexDL.AltoHttp
{
    /// <summary>
    /// Download progress changed event arguments
    /// </summary>
    public class DownloadProgressChangedEventArgs
    {
        private double progress;
        private int speed;

        /// <summary>
        /// Creates instance of the class
        /// </summary>
        /// <param name="progress">Current download progress</param>
        /// <param name="speed">Current ownload speed</param>
        public DownloadProgressChangedEventArgs(double progress, int speed)
        {
            this.progress = progress;
            this.speed = speed;
        }

        /// <summary>
        /// Gets the current progress
        /// </summary>
        public double Progress
        {
            get { return progress; }
        }

        /// <summary>
        /// Gets the current speed
        /// </summary>
        public int Speed
        {
            get { return speed; }
        }
    }
}