﻿using PlexDL.AltoHTTP.Enums;
using PlexDL.AltoHTTP.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DownloadProgressChangedEventArgs = PlexDL.AltoHTTP.Common.Events.EventArgs.DownloadProgressChangedEventArgs;
using ProgressChangedEventHandler = PlexDL.AltoHTTP.Common.Events.EventHandlers.ProgressChangedEventHandler;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault

namespace PlexDL.AltoHTTP.Common.Downloader
{
    /// <summary>
    ///     Contains methods to help downloading
    /// </summary>
    public class HttpDownloader : IDownloader
    {
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
        ///     Fired when response headers received e.g ContentLength, Resumeability
        /// </summary>
        public event EventHandler HeadersReceived;

        private HttpWebRequest _req;
        private HttpWebResponse _resp;
        private Stream _str;
        private FileStream _file;
        private Stopwatch _stpWatch;
        private readonly AsyncOperation _oprtor;

        private long _speedBytes;
        private long _progress;
        private DownloadState _state;

        /// <summary>
        ///     Gets content size of the file
        /// </summary>
        public long ContentSize { get; private set; }

        /// <summary>
        ///     Gets the total bytes count received
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        ///     Gets the current download speed in bytes
        /// </summary>
        public long SpeedInBytes { get; private set; }

        /// <summary>
        ///     Gets or sets the maximum download speed in bytes/s
        /// </summary>
        public long MaxSpeedInBytes { get; }

        /// <summary>
        ///     Gets the current download progress over 100
        /// </summary>
        public long Progress
        {
            get => _progress;
            private set
            {
                _progress = value;
                _oprtor.Post(delegate
                {
                    DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(_progress, SpeedInBytes, BytesReceived));
                }, null);
            }
        }

        /// <summary>
        ///     Get the source URL that will be downloaded when the download process is started
        /// </summary>
        public string FileUrl { get; }

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
            get => _state;
            private set
            {
                _state = value;
                switch (_state)
                {
                    case DownloadState.Completed when DownloadCompleted != null:
                        _oprtor.Post(delegate
                        {
                            DownloadCompleted?.Invoke(this, EventArgs.Empty);
                        }, null);
                        break;

                    case DownloadState.Cancelled when DownloadCancelled != null:
                        _oprtor.Post(delegate
                        {
                            DownloadCancelled?.Invoke(this, EventArgs.Empty);
                        }, null);
                        break;

                    case DownloadState.ErrorOccurred when DownloadError != null:
                        _oprtor.Post(delegate
                        {
                            DownloadError?.Invoke(this, EventArgs.Empty);
                        }, null);
                        break;
                }
            }
        }

        /// <summary>
        ///     Creates an instance of the HttpDownloader class
        /// </summary>
        /// <param name="url">Url source string</param>
        /// <param name="destPath">Target file path</param>
        /// <param name="maxSpeed">Maximum speed throttle (bytes/s; 0 means no throttle)</param>
        public HttpDownloader(string url, string destPath, long maxSpeed = 0)
        {
            Reset();
            FileUrl = url;
            DestPath = destPath;
            MaxSpeedInBytes = maxSpeed;
            _oprtor = AsyncOperationManager.CreateOperation(null);
        }

        /// <summary>
        ///     Destructor of the class object
        /// </summary>
        ~HttpDownloader()
        {
            Cancel();
        }

        private void Download(double offset, bool overWriteFile)
        {
            try
            {
                _req = WebRequest.Create(FileUrl) as HttpWebRequest;
                if (_req != null)
                {
                    _req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                    _req.AddRange((long)offset);
                    _req.KeepAlive = true;
                    _req.Proxy = new WebProxy();
                    _req.AllowAutoRedirect = true;
                    _resp = _req.GetResponse() as HttpWebResponse;
                }

                _str = MaxSpeedInBytes > 0 ? new ThrottledStream(_resp?.GetResponseStream(), MaxSpeedInBytes) : _resp?.GetResponseStream();
                if (!overWriteFile)
                {
                    if (_resp != null)
                        ContentSize = _resp.ContentLength;
                    AcceptRange = GetAcceptRangeHeaderValue();
                    if (HeadersReceived != null)
                        _oprtor.Post(delegate
                        {
                            HeadersReceived(this, EventArgs.Empty);
                        }, null);
                }

                if (overWriteFile)
                {
                    _file = File.Open(DestPath, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    _file = File.Exists(DestPath) ? new FileStream(DestPath, FileMode.Truncate, FileAccess.Write) : new FileStream(DestPath, FileMode.Create, FileAccess.Write);
                }

                long bytesRead;
                _speedBytes = 0;
                var buffer = new byte[4096];
                _stpWatch.Reset();
                _stpWatch.Start();

                while (_str != null && (bytesRead = _str.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if ((_state == DownloadState.Cancelled) | (_state == DownloadState.Paused)) break;
                    _state = DownloadState.Downloading;
                    _file.Write(buffer, 0, (int)bytesRead);
                    _file.Flush();
                    BytesReceived += bytesRead;
                    _speedBytes += bytesRead;

                    //CALCULATIONS FOR DOWNLOAD PROGRESS
                    Progress = _progress = (BytesReceived / ContentSize) * 100;
                    SpeedInBytes = (long)(_speedBytes / 1.0 / _stpWatch.Elapsed.TotalSeconds);
                }

                _resp?.Close();

                _stpWatch.Reset();
                CloseResources();
                Thread.Sleep(100);
                if (_state != DownloadState.Downloading)
                    return;
                _state = DownloadState.Completed;
                State = _state;
            }
            catch
            {
                _state = DownloadState.ErrorOccurred;
                State = _state;
            }
        }

        /// <summary>
        ///     Starts the download async
        /// </summary>
        public async void StartAsync()
        {
            if ((_state != DownloadState.Started) & (_state != DownloadState.Completed) & (_state != DownloadState.Cancelled) &
                (_state != DownloadState.ErrorOccurred))
                return;

            _state = DownloadState.Started;
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
                _state = DownloadState.Paused;
        }

        /// <summary>
        ///     Resumes the download, only if the download is paused
        /// </summary>
        public void ResumeAsync()
        {
            if (State != DownloadState.Paused) return;
            _state = DownloadState.Started;
            Task.Run(() =>
            {
                Download(BytesReceived, true);
            });
        }

        /// <summary>
        ///     Cancels the download and deletes the uncompleted file which is saved to destination
        /// </summary>
        public void Cancel()
        {
            if ((_state == DownloadState.Completed) | (_state == DownloadState.Cancelled) | (_state == DownloadState.ErrorOccurred)) return;
            if (_state == DownloadState.Paused)
            {
                Pause();
                _state = DownloadState.Cancelled;
                Thread.Sleep(100);
                CloseResources();
            }

            _state = DownloadState.Cancelled;
        }

        private void Reset()
        {
            _progress = 0;
            BytesReceived = 0;
            SpeedInBytes = 0;
            _stpWatch = new Stopwatch();
        }

        private void CloseResources()
        {
            _resp?.Close();
            _file?.Close();
            _str?.Close();
            if (DestPath != null && (_state == DownloadState.Cancelled) | (_state == DownloadState.ErrorOccurred))
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
            for (var i = 0; i < _resp.Headers.Count; i++)
                if (_resp.Headers.AllKeys[i].Contains("Range"))
                    return _resp.Headers[i].Contains("byte");

            return false;
        }
    }
}