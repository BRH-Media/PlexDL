using PlexDL.AltoHTTP.Common.Events.EventArgs;

namespace PlexDL.AltoHTTP.Common.Events.EventHandlers
{
    /// <summary>
    ///     Passes the QueueElementCompleted event args
    /// </summary>
    /// <param name="sender">The objects which the event is occurred in</param>
    /// <param name="e">Event arguments</param>
    public delegate void QueueElementCompletedEventHandler(object sender, QueueElementCompletedEventArgs e);
}