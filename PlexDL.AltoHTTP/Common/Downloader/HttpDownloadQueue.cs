using PlexDL.AltoHTTP.Common.Events.EventArgs;
using PlexDL.AltoHTTP.Common.Events.EventHandlers;
using PlexDL.AltoHTTP.Enums;
using PlexDL.AltoHTTP.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PlexDL.AltoHTTP.Common.Downloader
{
    /// <summary>
    ///     Provides methods to create and process download queue
    /// </summary>
    public class HttpDownloadQueue : IQueue, IDisposable
    {
        private HttpDownloader _downloader;
        private readonly List<HttpDownloadQueueElement> _elements;
        private HttpDownloadQueueElement _currentElement;
        private double _progress;
        private bool _queuePaused, _startEventRaised;

        /// <summary>
        ///     Occurs when queue element's progress is changed
        /// </summary>
        public event EventHandler QueueProgressChanged;

        /// <summary>
        ///     Occurs when the queue is completely completed
        /// </summary>
        public event EventHandler QueueCompleted;

        /// <summary>
        ///     Occurs when the queue element is completed
        /// </summary>
        public event QueueElementCompletedEventHandler QueueElementCompleted;

        /// <summary>
        ///     Occurs when the queue has been started
        /// </summary>
        public event EventHandler QueueElementStartedDownloading;

        /// <summary>
        ///     Creates a queue and initializes resources
        /// </summary>
        public HttpDownloadQueue()
        {
            _downloader = null;
            _elements = new List<HttpDownloadQueueElement>();
            CurrentDownloadSpeed = 0;
            _queuePaused = true;
        }

        /// <summary>
        ///     Destructor for the object
        /// </summary>
        ~HttpDownloadQueue()
        {
            Cancel();
        }

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _progress = e.Progress;
            CurrentProgress = _progress;
            CurrentDownloadSpeed = e.Speed;
            BytesReceived = e.BytesReceived;
        }

        private void Downloader_DownloadCompleted(object sender, System.EventArgs e)
        {
            QueueElementCompleted?.Invoke(this, new QueueElementCompletedEventArgs(CurrentIndex, _currentElement));
            for (var i = 0; i < _elements.Count; i++)
                if (_elements[i].Equals(_currentElement))
                {
                    _elements[i] = new HttpDownloadQueueElement
                    {
                        Id = _elements[i].Id,
                        Url = _elements[i].Url,
                        Destination = _elements[i].Destination,
                        Completed = true
                    };
                    break;
                }

            CreateNextDownload();
        }

        private void Downloader_DownloadError(object sender, System.EventArgs e)
        {
            QueueElementCompleted?.Invoke(this, new QueueElementCompletedEventArgs(CurrentIndex, _currentElement));
            for (var i = 0; i < _elements.Count; i++)
                if (_elements[i].Equals(_currentElement))
                {
                    _elements[i] = new HttpDownloadQueueElement
                    {
                        Id = _elements[i].Id,
                        Url = _elements[i].Url,
                        Destination = _elements[i].Destination,
                        Completed = true
                    };
                    break;
                }

            var active = Form.ActiveForm;

            if (active != null && active.InvokeRequired)
                active.BeginInvoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Download error occurred. Please check your connection, and that the content requested is available for download.",
                        "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            else
                MessageBox.Show("Download error occurred. Please check your connection, and that the content requested is available for download.",
                    "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            QueueCompleted?.Invoke(this, new System.EventArgs());
        }

        /// <summary>
        ///     Gets the number of elements in the queue
        /// </summary>
        public int QueueLength => _elements.Count;

        /// <summary>
        ///     Gets the index number of the element that is currently processing
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                for (var i = 0; i < _elements.Count; i++)
                    if (!_elements[i].Completed)
                        return i;

                return -1;
            }
        }

        /// <summary>
        ///     Gets the download progress of the current download process
        /// </summary>
        public double CurrentProgress
        {
            get => _progress;
            private set
            {
                _progress = value;
                if (QueueProgressChanged != null && CurrentIndex >= 0 && !_queuePaused)
                    QueueProgressChanged(this, System.EventArgs.Empty);

                if (QueueElementStartedDownloading == null || !(_progress > 0) || _startEventRaised) return;

                QueueElementStartedDownloading(this, System.EventArgs.Empty);
                _startEventRaised = true;
            }
        }

        public double BytesReceived
        {
            get;
            set;
        }

        public double CurrentContentLength
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets/sets the download speed of the queue
        ///     <br></br>WARNING: Set this once and do not change during the process
        /// </summary>
        public long MaxSpeedInBytes { get; set; } = 0;

        /// <summary>
        ///     Gets the download speed of the current download progress
        /// </summary>
        public double CurrentDownloadSpeed { get; private set; }

        /// <summary>
        ///     Gets the Range header value of the current download
        /// </summary>
        public bool CurrentAcceptRange => _downloader.AcceptRange;

        /// <summary>
        ///     Gets the State of the current download
        /// </summary>
        public DownloadState State => _downloader.State;

        /// <summary>
        ///     Adds new download elements into the queue
        /// </summary>
        /// <param name="url">The URL source that contains the object will be downloaded</param>
        /// <param name="destPath">The destination path to save the downloaded file</param>
        public void Add(string url, string destPath)
        {
            _elements.Add(new HttpDownloadQueueElement
            {
                Id = Guid.NewGuid().ToString(),
                Url = url,
                Destination = destPath
            });
        }

        /// <summary>
        ///     Deletes the queue element at the given index
        /// </summary>
        /// <param name="index">The index of the element that will be deleted</param>
        public void Delete(int index)
        {
            if (_elements[index].Equals(_currentElement) && _downloader != null)
            {
                _downloader.Cancel();
                _currentElement = new HttpDownloadQueueElement
                {
                    Url = ""
                };
            }

            _elements.RemoveAt(index);
            if (!_queuePaused)
                CreateNextDownload();
        }

        /// <summary>
        ///     Deletes all elements in the queue
        /// </summary>
        public void Clear()
        {
            Cancel();
        }

        /// <summary>
        ///     Starts the queue async
        /// </summary>
        public void StartAsync()
        {
            CreateNextDownload();
        }

        /// <summary>
        ///     Stops and deletes all elements in the queue
        /// </summary>
        public void Cancel()
        {
            _downloader?.Cancel();
            Thread.Sleep(100);
            _elements.Clear();
            _queuePaused = true;
        }

        /// <summary>
        ///     The queue process resumes
        /// </summary>
        public void ResumeAsync()
        {
            if (string.IsNullOrEmpty(_currentElement.Url))
            {
                CreateNextDownload();
                return;
            }

            _downloader.ResumeAsync();
            _queuePaused = false;
        }

        /// <summary>
        ///     The queue process pauses
        /// </summary>
        public void Pause()
        {
            _downloader.Pause();
            _queuePaused = true;
        }

        /// <summary>
        ///     Removes all resources used
        /// </summary>
        public void Dispose()
        {
            Cancel();
        }

        private void CreateNextDownload()
        {
            var elt = FirstNotCompletedElement;
            if (string.IsNullOrEmpty(elt.Url)) return;
            _downloader = new HttpDownloader(elt.Url, elt.Destination, MaxSpeedInBytes);
            _downloader.DownloadCompleted += Downloader_DownloadCompleted;
            _downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
            _downloader.DownloadError += Downloader_DownloadError;
            _downloader.StartAsync();
            _currentElement = elt;
            _queuePaused = false;
            _startEventRaised = false;
            //do nothing until we know the total size
            while (_downloader.ContentSize == 0) { }
            CurrentContentLength = _downloader.ContentSize;
        }

        private HttpDownloadQueueElement FirstNotCompletedElement
        {
            get
            {
                for (var i = 0; i < _elements.Count; i++)
                    if (!_elements[i].Completed)
                        return _elements[i];

                QueueCompleted?.Invoke(this, new System.EventArgs());
                return new HttpDownloadQueueElement();
            }
        }
    }
}