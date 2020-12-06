using PlexDL.AltoHTTP.Common.Events.EventArgs;

namespace PlexDL.AltoHTTP.Common.Events.EventHandlers
{
    /// <summary>
    ///     ProgressChanged event handler
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    public delegate void ProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);
}