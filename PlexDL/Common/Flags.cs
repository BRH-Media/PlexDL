namespace PlexDL.Common
{
    public static class Flags
    {
        public static bool IsDebug { get; set; } = false;
        public static bool IsConnected { get; set; } = false;
        public static bool IsInitialFill { get; set; } = true;
        public static bool IsLibraryFilled { get; set; } = false;
        public static bool IsFiltered { get; set; } = false;
        public static bool IsTVShow { get; set; } = false;
        public static bool IsContentSortingEnabled { get; set; } = true;
        public static bool IsDownloadQueueCancelled { get; set; } = false;
        public static bool IsDownloadRunning { get; set; } = false;
        public static bool IsDownloadPaused { get; set; } = false;
        public static bool IsEngineRunning { get; set; } = false;
        public static bool IsMsgAlreadyShown { get; set; } = false;
        public static bool IsDownloadAllEpisodes { get; set; } = false;
    }
}