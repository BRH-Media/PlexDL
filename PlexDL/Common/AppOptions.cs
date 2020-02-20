using PlexDL.Common.Structures;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlexDL.Common
{
    public class AppOptions
    {
        public ConnectionInformation ConnectionInfo { get; set; } = new ConnectionInformation();
        public LoggingSettings Logging { get; set; } = new LoggingSettings();
        public GridViewDisplays DataDisplay { get; set; } = new GridViewDisplays();
        public PlayerSettings Player { get; set; } = new PlayerSettings();
        public GenericAppSettings Generic { get; set; } = new GenericAppSettings();
        public CachingSettings CacheSettings { get; set; } = new CachingSettings();
    }

    public class GenericAppSettings
    {
        public int DefaultStringLength { get; set; } = 64;
        public bool ShowConnectionSuccess { get; set; } = false;
        public int DownloadLayoutDefinition { get; set; } = 0;
        public string DownloadDirectory { get; set; } = "";
        public int AnimationSpeed { get; set; } = 10;
    }

    public class CacheMode
    {
        public bool EnableServerCaching { get; set; } = true;
        public bool EnableThumbCaching { get; set; } = true;
        public bool EnableXMLCaching { get; set; } = true;
    }

    public class CachePaths
    {
        public string XMLCachePath { get; set; } = @"cache\%TOKEN%\%SERVER%\xml";
        public string ThumbCachePath { get; set; } = @"cache\%TOKEN%\%SERVER%\thumb";
        public string ServerCachePath { get; set; } = @"cache\%TOKEN%";
    }

    public class CachingSettings
    {
        public CacheMode Mode { get; set; } = new CacheMode();
        public CachePaths Paths { get; set; } = new CachePaths();
    }

    public class LoggingSettings
    {
        public bool EnableXMLTransactionLogDel { get; set; } = true;
        public bool EnableExceptionLogDel { get; set; } = true;
        public bool EnableGenericLogDel { get; set; } = true;
    }

    public class GridViewDisplays
    {
        public ContentDisplay ContentView { get; set; } = new ContentDisplay();
        public SeriesDisplay SeriesView { get; set; } = new SeriesDisplay();
        public EpisodesDisplay EpisodesView { get; set; } = new EpisodesDisplay();
        public LibraryDisplay LibraryView { get; set; } = new LibraryDisplay();
    }

    public class LibraryDisplay
    {
        public List<string> LibraryDisplayColumns { get; set; } = new List<string>() { "title", "type" };
        public List<string> LibraryDisplayCaption { get; set; } = new List<string>() { "Title", "Type" };
    }

    public class ContentDisplay
    {
        public List<string> ContentDisplayColumns { get; set; } = new List<string>() { "title", "studio", "year", "contentRating" };
        public List<string> ContentDisplayCaption { get; set; } = new List<string>() { "Title", "Studio", "Year", "Rating" };
    }

    public class SeriesDisplay
    {
        public List<string> SeriesDisplayColumns { get; set; } = new List<string>() { "title", "studio", "year", "contentRating" };
        public List<string> SeriesDisplayCaption { get; set; } = new List<string>() { "Title", "Studio", "Year", "Rating" };
    }

    public class EpisodesDisplay
    {
        public List<string> EpisodesDisplayColumns { get; set; } = new List<string>() { "title", "year", "contentRating" };
        public List<string> EpisodesDisplayCaption { get; set; } = new List<string>() { "Title", "Year", "Rating" };
    }

    public class PlayerKeyBindings
    {
        public Keys PlayPause { get; set; } = Keys.Space;
        public Keys SkipForward { get; set; } = Keys.Right;
        public Keys SkipBackward { get; set; } = Keys.Left;
        public Keys NextTitle { get; set; } = Keys.Up;
        public Keys PrevTitle { get; set; } = Keys.Down;
    }

    public class PlayerSettings
    {
        public string VLCMediaPlayerArgs { get; set; } = @"%FILE% --meta-title=%TITLE%";
        public string VLCMediaPlayerPath { get; set; } = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
        public decimal SkipForwardInterval { get; set; } = 30;
        public decimal SkipBackwardInterval { get; set; } = 10;
        public bool PlayNextTitleAutomatically { get; set; } = false;
        public PlayerKeyBindings KeyBindings { get; set; } = new PlayerKeyBindings();
        public int PlaybackEngine { get; set; } = PlaybackMode.PVSPlayer;
        public bool ShowFSMessage { get; set; } = true;
    }

    public static class PlaybackMode
    {
        public const int PVSPlayer = 0;
        public const int VLCPlayer = 1;
        public const int Browser = 2;
    }
}