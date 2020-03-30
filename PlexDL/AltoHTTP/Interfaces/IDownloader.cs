using System;
using PlexDL.AltoHTTP.Classes;
using PlexDL.AltoHTTP.Enums;

namespace PlexDL.AltoHTTP.Interfaces
{
    internal interface IDownloader
    {
        long ContentSize { get; }
        long BytesReceived { get; }
        double Progress { get; }
        int SpeedInBytes { get; }
        string FileURL { get; }
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