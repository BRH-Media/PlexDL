using PlexDL.AltoHTTP.Common.Events.EventHandlers;
using PlexDL.AltoHTTP.Enums;
using System;

namespace PlexDL.AltoHTTP.Interfaces
{
    internal interface IDownloader
    {
        long ContentSize { get; }
        long BytesReceived { get; }
        double Progress { get; }
        long SpeedInBytes { get; }
        string FileUrl { get; }
        string DestPath { get; }
        bool AcceptRange { get; }
        DownloadState State { get; }

        event EventHandler DownloadCancelled;

        event EventHandler DownloadCompleted;

        event ProgressChangedEventHandler DownloadProgressChanged;

        void StartAsync();

        void Pause();

        void ResumeAsync();

        void Cancel();
    }
}