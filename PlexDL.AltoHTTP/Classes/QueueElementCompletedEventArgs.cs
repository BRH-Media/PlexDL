namespace PlexDL.AltoHTTP.Classes
{
    /// <summary>
    ///     Queue element completed event arguments
    /// </summary>
    public class QueueElementCompletedEventArgs
    {
        /// <summary>
        ///     Contains QueueElementCompleted event args
        /// </summary>
        /// <param name="index"></param>
        public QueueElementCompletedEventArgs(int index, QueueElement element)
        {
            Index = index;
            Element = element;
        }

        /// <summary>
        ///     The index of the completed element
        /// </summary>
        public int Index { get; }

        /// <summary>
        ///     The index of the completed element
        /// </summary>
        public QueueElement Element { get; }
    }
}