using PlexDL.Common.Structures.AppOptions.Display.Grids;

namespace PlexDL.Common.Structures.AppOptions.Display
{
    public class GridViewDisplays
    {
        public ContentDisplay ContentView { get; set; } = new ContentDisplay();
        public TVDisplay TVView { get; set; } = new TVDisplay();
        public SeriesDisplay SeriesView { get; set; } = new SeriesDisplay();
        public EpisodesDisplay EpisodesView { get; set; } = new EpisodesDisplay();
        public LibraryDisplay LibraryView { get; set; } = new LibraryDisplay();
    }
}