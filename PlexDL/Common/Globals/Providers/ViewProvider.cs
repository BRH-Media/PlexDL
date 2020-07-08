using System.Data;

namespace PlexDL.Common.Globals.Providers
{
    public static class ViewProvider
    {
        public static DataTable MoviesViewTable { get; set; }
        public static DataTable ArtistViewTable { get; set; }
        public static DataTable AlbumViewTable { get; set; }
        public static DataTable TracksViewTable { get; set; }
        public static DataTable TvViewTable { get; set; }
        public static DataTable SectionsViewTable { get; set; }
        public static DataTable SeasonsViewTable { get; set; }
        public static DataTable EpisodesViewTable { get; set; }
        public static DataTable FilteredViewTable { get; set; }
    }
}