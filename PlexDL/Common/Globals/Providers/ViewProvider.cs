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

        public static void ClearAllViews()
        {
            //WARNING! This may result in errors, use only when data needs to be 100% cleared (like on disconnect).
            MoviesViewTable = null;
            ArtistViewTable = null;
            AlbumViewTable = null;
            TracksViewTable = null;
            TvViewTable = null;
            SectionsViewTable = null;
            SeasonsViewTable = null;
            EpisodesViewTable = null;
            FilteredViewTable = null;
        }
    }
}