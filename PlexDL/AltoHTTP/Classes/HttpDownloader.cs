using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PlexDL.AltoHTTP.Enums;
using PlexDL.AltoHTTP.Interfaces;

namespace PlexDL.AltoHTTP.Classes
{
    /// <summary>
    ///     Contains methods to help downloading
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        #region Variables

        /// <summary>
        ///     Occurs when the download process is completed
        /// </summary>
        public event EventHandler DownloadCompleted;

        /// <summary>
        ///     Occurs when the download process is cancelled
        /// </summary>
        public event EventHandler DownloadCancelled;

        /// <summary>
        ///     Occurs when an exception is thrown in the download
        /// </summary>
        public event EventHandler DownloadError;

        /// <summary>
        ///     Occurs when the download progress is changed
        /// </summary>
        public event ProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        ///     Fired when response headers received e.g ContentLegth, Resumeability
        /// </summary>
        public event EventHandler HeadersReceived;

        private HttpWebRequest req;
        private HttpWebResponse resp;
        private Stream str;
        private FileStream file;
        private Stopwatch stpWatch;
        private readonly AsyncOperation oprtor;
        private int bytesReceived, speedBytes;
        private double progress;
        private DownloadState state;

        #endregion Variables

        #region Properties

        /// <summary>
        ///     Gets content size of the file
        /// </summary>
        public long ContentSize { get; private set; }

        /// <summary>
        ///     Gets the total bytes count received
        /// </summary>
        public long BytesReceived => bytesReceived;

        /// <summary>
        ///     Gets the current download speed in bytes
        /// </summary>
        public int SpeedInBytes { get; private set; }

        /// <summary>
        ///     Gets the current download progress over 100
        /// </summary>
        public double Progress
        {
            get => progress;
            private set
            {
                progress = value;
                oprtor.Post(delegate
                {
                    if (DownloadProgressChanged != null)
                        DownloadProgressChanged(this, new DownloadProgressChangedEventArgs(progress, SpeedInBytes));
                }, null);
            }
        }

        /// <summary>
        ///     Get the source URL that will be downloaded when the download process is started
        /// </summary>
        public string FileURL { get; }

        /// <summary>
        ///     Gets the destination path that the file will be saved when the download process is completed
        /// </summary>
        public string DestPath { get; }

        /// <summary>
        ///     Returns true if the source supports to set ranges into the requests, if not returns false
        /// </summary>
        public bool AcceptRange { get; private set; }

        /// <summary>
        ///     Gets the value that reports the state of the download process
        /// </summary>
        public DownloadState State
        {
            get => state;
            private set
            {
                state = value;
                if (state == DownloadState.Completed && DownloadCompleted != null)
                    oprtor.Post(delegate
                    {
                        if (DownloadCompleted != null)
                            DownloadCompleted(this, EventArgs.Empty);
                    }, null);
                else if (state == DownloadState.Cancelled && DownloadCancelled != null)
                    oprtor.Post(delegate
                    {
                        if (DownloadCancelled != null)
                            DownloadCancelled(this, EventArgs.Empty);
                    }, null);
                else if (state == DownloadState.ErrorOccured && DownloadError != null)
                    oprtor.Post(delegate
                    {
                        if (DownloadError != null)
                            DownloadError(this, EventArgs.Empty);
                    }, null);
            }
        }

        #endregion Properties

        #region Constructor, Destructor, Download Procedure

        /// <summary>
        ///     Creates an instance of the HttpDownloader class
        /// </summary>
        /// <param name="url">Url source string</param>
        /// <param name="destPath">Target file path</param>
        public HttpDownloader(string url, string destPath)
        {
            Reset();
            FileURL = url;
            DestPath = destPath;
            oprtor = AsyncOperationManager.CreateOperation(null);
        }

        /// <summary>
        ///     Destructor of the class object
        /// </summary>
        ~HttpDownloader()
        {
            Cancel();
        }

        private void Download(int offset, bool overWriteFile)
        {
            #region Send Request, Get Response

            try
            {
                req = WebRequest.Create(FileURL) as HttpWebRequest;
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                req.AddRange(offset);
                req.AllowAutoRedirect = true;
                resp = req.GetResponse() as HttpWebResponse;
                str = resp.GetResponseStream();
                if (!overWriteFile)
                {
                    ContentSize = resp.ContentLength;
                    AcceptRange = GetAcceptRangeHeaderValue();
                    if (HeadersReceived != null)
                        oprtor.Post(delegate
                        {
                            HeadersReceived(this, EventArgs.Empty);
                        }, null);
                }
            }
            catch
            {
                state = DownloadState.ErrorOccured;
                State = state;
                return;
            }

            #endregion Send Request, Get Response

            if (overWriteFile)
            {
                file = File.Open(DestPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                if (File.Exists(DestPath))
                    file = new FileStream(DestPath, FileMode.Truncate, FileAccess.Write);
                else
                    file = new FileStream(DestPath, FileMode.Create, FileAccess.Write);
            }

            var bytesRead = 0;
            speedBytes = 0;
            var buffer = new byte[4096];
            stpWatch.Reset();
            stpWatch.Start();

            #region Get the data to the buffer, write it to the file

            while ((bytesRead = str.Read(buffer, 0, buffer.Length)) > 0)
            {
                if ((state == DownloadState.Cancelled) | (state == DownloadState.Paused)) break;
                state = DownloadState.Downloading;
                file.Write(buffer, 0, bytesRead);
                file.Flush();
                bytesReceived += bytesRead;
                speedBytes += bytesRead;
                Progress = progress = (double)bytesReceived * 100 / ContentSize;
                SpeedInBytes = (int)(speedBytes / 1.0 / stpWatch.Elapsed.TotalSeconds);
            }

            #endregion Get the data to the buffer, write it to the file

            stpWatch.Reset();
            CloseResources();
            Thread.Sleep(100);
            if (state == DownloadState.Downloading)
            {
                state = DownloadState.Completed;
                State = state;
            }
        }

        #endregion Constructor, Destructor, Download Procedure

        #region Start, Pause, Stop, Resume

        /// <summary>
        ///     Starts the download async
        /// </summary>
        public async void StartAsync()
        {
            if ((state != DownloadState.Started) & (state != DownloadState.Completed) & (state != DownloadState.Cancelled) &
                (state != DownloadState.ErrorOccured))
                return;

            state = DownloadState.Started;
            await Task.Run(() =>
            {
                Download(0, false);
            });
        }

        /// <summary>
        ///     Pauses the download process
        /// </summary>
        public void Pause()
        {
            if (!AcceptRange)
                throw new Exception("This download process cannot be paused because it doesn't support ranges");
            if (State == DownloadState.Downloading)
                state = DownloadState.Paused;
        }

        /// <summary>
        ///     Resumes the download, only if the download is paused
        /// </summary>
        public void ResumeAsync()
        {
            if (State != DownloadState.Paused) return;
            state = DownloadState.Started;
            Task.Run(() =>
            {
                Download(bytesReceived, true);
            });
        }

        /// <summary>
        ///     Cancels the download and deletes the uncompleted file which is saved to destination
        /// </summary>
        public void Cancel()
        {
            if ((state == DownloadState.Completed) | (state == DownloadState.Cancelled) | (state == DownloadState.ErrorOccured)) return;
            if (state == DownloadState.Paused)
            {
                Pause();
                state = DownloadState.Cancelled;
                Thread.Sleep(100);
                CloseResources();
            }

            state = DownloadState.Cancelled;
        }

        #endregion Start, Pause, Stop, Resume

        #region Helper Methods

        private void Reset()
        {
            progress = 0;
            bytesReceived = 0;
            SpeedInBytes = 0;
            stpWatch = new Stopwatch();
        }

        private void CloseResources()
        {
            if (resp != null)
                resp.Close();
            if (file != null)
                file.Close();
            if (str != null)
                str.Close();
            if (DestPath != null && (state == DownloadState.Cancelled) | (state == DownloadState.ErrorOccured))
                try
                {
                    File.Delete(DestPath);
                }
                catch
                {
                    throw new Exception("There is an error unknown. This problem may cause because of the file is in use");
                }
        }

        private bool GetAcceptRangeHeaderValue()
        {
            for (var i = 0; i < resp.Headers.Count; i++)
                if (resp.Headers.AllKeys[i].Contains("Range"))
                    return resp.Headers[i].Contains("byte");

            return false;
        }

        #endregion Helper Methods
    }
}