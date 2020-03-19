using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using PlexDL.AltoHTTP.Enums;
using PlexDL.AltoHTTP.Interfaces;

namespace PlexDL.AltoHTTP.Classes
{
    /// <summary>
    /// Provides methods to create and process download queue
    /// </summary>
    public class DownloadQueue : IQueue, IDisposable
    {
        #region Variables

        private HttpDownloader downloader;
        private readonly List<QueueElement> elements;
        private QueueElement currentElement;
        private double progress;
        private int downloadSpeed;
        private bool queuePaused, startEventRaised;

        /// <summary>
        /// Occurs when queue element's progress is changed
        /// </summary>
        public event EventHandler QueueProgressChanged;

        /// <summary>
        /// Occurs when the queue is completely completed
        /// </summary>
        public event EventHandler QueueCompleted;

        /// <summary>
        /// Occurs when the queue element is completed
        /// </summary>
        public event QueueElementCompletedEventHandler QueueElementCompleted;

        /// <summary>
        /// Occurs when the queue has been started
        /// </summary>
        public event EventHandler QueueElementStartedDownloading;

        #endregion Variables

        #region Constructor + Destructor

        /// <summary>
        /// Creates a queue and initializes resources
        /// </summary>
        public DownloadQueue()
        {
            downloader = null;
            elements = new List<QueueElement>();
            downloadSpeed = 0;
            queuePaused = true;
        }

        /// <summary>
        /// Destructor for the object
        /// </summary>
        ~DownloadQueue()
        {
            Cancel();
        }

        #endregion Constructor + Destructor

        #region Events

        private void Downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress = e.Progress;
            CurrentProgress = progress;
            downloadSpeed = e.Speed;
        }

        private void Downloader_DownloadCompleted(object sender, EventArgs e)
        {
            QueueElementCompleted?.Invoke(this, new QueueElementCompletedEventArgs(CurrentIndex, currentElement));
            for (var i = 0; i < elements.Count; i++)
                if (elements[i].Equals(currentElement))
                {
                    elements[i] = new QueueElement()
                    {
                        Id = elements[i].Id, Url = elements[i].Url, Destination = elements[i].Destination, Completed = true
                    };
                    break;
                }

            CreateNextDownload();
        }

        private void Downloader_DownloadError(object sender, EventArgs e)
        {
            QueueElementCompleted?.Invoke(this, new QueueElementCompletedEventArgs(CurrentIndex, currentElement));
            for (var i = 0; i < elements.Count; i++)
                if (elements[i].Equals(currentElement))
                {
                    elements[i] = new QueueElement()
                    {
                        Id = elements[i].Id, Url = elements[i].Url, Destination = elements[i].Destination, Completed = true
                    };
                    break;
                }

            var active = Form.ActiveForm;

            if (active.InvokeRequired)
                active.BeginInvoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Download error occurred. Please check your connection, and that the content requested is available for download.",
                        "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            else
                MessageBox.Show("Download error occurred. Please check your connection, and that the content requested is available for download.",
                    "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            QueueCompleted?.Invoke(this, new EventArgs());
        }

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the number of elements in the queue
        /// </summary>
        public int QueueLength => elements.Count;

        /// <summary>
        /// Gets the index number of the element that is currently processing
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                for (var i = 0; i < elements.Count; i++)
                    if (!elements[i].Completed)
                        return i;

                return -1;
            }
        }

        /// <summary>
        /// Gets the download progress of the current download process
        /// </summary>
        public double CurrentProgress
        {
            get => progress;
            private set
            {
                value = progress;
                if (QueueProgressChanged != null && CurrentIndex >= 0 && !queuePaused)
                    QueueProgressChanged(this, EventArgs.Empty);
                if (QueueElementStartedDownloading != null && progress > 0 && !startEventRaised)
                {
                    QueueElementStartedDownloading(this, EventArgs.Empty);
                    startEventRaised = true;
                }
            }
        }

        /// <summary>
        /// Gets the download speed of the current download progress
        /// </summary>
        public int CurrentDownloadSpeed => downloadSpeed;

        /// <summary>
        /// Gets the Range header value of the current download
        /// </summary>
        public bool CurrentAcceptRange => downloader.AcceptRange;

        /// <summary>
        /// Gets the State of the current download
        /// </summary>
        public DownloadState State => downloader.State;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds new download elements into the queue
        /// </summary>
        /// <param name="url">The URL source that contains the object will be downloaded</param>
        /// <param name="destPath">The destination path to save the downloaded file</param>
        public void Add(string url, string destPath)
        {
            elements.Add(new QueueElement()
            {
                Id = Guid.NewGuid().ToString(), Url = url, Destination = destPath
            });
        }

        /// <summary>
        /// Deletes the queue element at the given index
        /// </summary>
        /// <param name="index">The index of the element that will be deleted</param>
        public void Delete(int index)
        {
            if (elements[index].Equals(currentElement) && downloader != null)
            {
                downloader.Cancel();
                currentElement = new QueueElement()
                {
                    Url = ""
                };
            }

            elements.RemoveAt(index);
            if (!queuePaused)
                CreateNextDownload();
        }

        /// <summary>
        /// Deletes all elements in the queue
        /// </summary>
        public void Clear()
        {
            Cancel();
        }

        /// <summary>
        /// Starts the queue async
        /// </summary>
        public void StartAsync()
        {
            CreateNextDownload();
        }

        /// <summary>
        /// Stops and deletes all elements in the queue
        /// </summary>
        public void Cancel()
        {
            if (downloader != null)
                downloader.Cancel();
            Thread.Sleep(100);
            elements.Clear();
            queuePaused = true;
        }

        /// <summary>
        /// The queue process resumes
        /// </summary>
        public void ResumeAsync()
        {
            if (string.IsNullOrEmpty(currentElement.Url))
            {
                CreateNextDownload();
                return;
            }

            downloader.ResumeAsync();
            queuePaused = false;
        }

        /// <summary>
        /// The queue process pauses
        /// </summary>
        public void Pause()
        {
            downloader.Pause();
            queuePaused = true;
        }

        /// <summary>
        /// Removes all resources used
        /// </summary>
        public void Dispose()
        {
            Cancel();
        }

        #endregion Methods

        #region Helper Methods

        private void CreateNextDownload()
        {
            var elt = FirstNotCompletedElement;
            if (string.IsNullOrEmpty(elt.Url)) return;
            downloader = new HttpDownloader(elt.Url, elt.Destination);
            downloader.DownloadCompleted += Downloader_DownloadCompleted;
            downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
            downloader.DownloadError += Downloader_DownloadError;
            downloader.StartAsync();
            currentElement = elt;
            queuePaused = false;
            startEventRaised = false;
        }

        private QueueElement FirstNotCompletedElement
        {
            get
            {
                for (var i = 0; i < elements.Count; i++)
                    if (!elements[i].Completed)
                        return elements[i];

                QueueCompleted?.Invoke(this, new EventArgs());
                return new QueueElement();
            }
        }

        #endregion Helper Methods
    }
}