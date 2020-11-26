using PlexDL.AltoHTTP.Classes.Events.EventArgs;

namespace PlexDL.AltoHTTP.Classes.Events.EventHandlers
{
    /// <summary>
    ///     ProgressChanged event handler
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    public delegate void ProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);
}