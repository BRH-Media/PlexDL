using PlexDL.AltoHTTP.Classes.Events.EventArgs;

namespace PlexDL.AltoHTTP.Classes.Events.EventHandlers
{
    /// <summary>
    ///     Passes the QueueElementCompleted event args
    /// </summary>
    /// <param name="sender">The objects which the event is occured in</param>
    /// <param name="e">Event arguments</param>
    public delegate void QueueElementCompletedEventHandler(object sender, QueueElementCompletedEventArgs e);
}