using PlexDL.AltoHTTP.Classes;
using PlexDL.AltoHTTP.Enums;
using System;

namespace PlexDL.AltoHTTP.Interfaces
{
    internal interface IDownloader
    {
        event EventHandler DownloadCancelled;

        event EventHandler DownloadCompleted;

        event ProgressChangedEventHandler DownloadProgressChanged;

        long ContentSize { get; }
        long BytesReceived { get; }
        double Progress { get; }
        int SpeedInBytes { get; }
        string FileURL { get; }
        string DestPath { get; }
        bool AcceptRange { get; }
        DownloadState State { get; }

        void StartAsync();

        void Pause();

        void ResumeAsync();

        void Cancel();
    }
}