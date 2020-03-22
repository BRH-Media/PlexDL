using System.Data;

namespace PlexDL.Common.Globals
{
    public static class GlobalTables
    {
        public static DataTable TitlesTable { get; set; }
        public static DataTable FilteredTable { get; set; }
        public static DataTable SeriesTable { get; set; }
        public static DataTable EpisodesTable { get; set; }
        public static DataTable SectionsTable { get; set; }
        public static DataTable ContentViewTable { get; set; }
        public static DataTable TvViewTable { get; set; }
    }
}