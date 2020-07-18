using PlexDL.Common.Enums;
using PlexDL.Common.Logging;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PlexDL.Common.Globals.Providers
{
    public static class TableProvider
    {
        public static DataTable TitlesTable { get; set; }
        public static DataTable FilteredTable { get; set; }
        public static DataTable SeasonsTable { get; set; }
        public static DataTable EpisodesTable { get; set; }
        public static DataTable SectionsTable { get; set; }
        public static DataTable AlbumsTable { get; set; }
        public static DataTable TracksTable { get; set; }

        public static bool TitlesTableFilled => TableFilled(TitlesTable);
        public static bool FilteredTableFilled => TableFilled(FilteredTable);
        public static bool ActiveTableFilled => TableFilled(ReturnCorrectTable());

        private static bool TableFilled(DataTable table)
        {
            //check if the table isn't null. This check precedes the count check,
            //because if the table is null you can't check its properties, i.e. its row count.
            if (table == null) return false;

            //it isn't null, but does it have rows? If no, then it's invalid. If yes, then the table's valid.
            return table.Rows.Count > 0;
        }

        public static void ClearAllTables()
        {
            //WARNING! This may result in errors, use only when data needs to be 100% cleared (like on disconnect).
            TitlesTable = null;
            FilteredTable = null;
            SeasonsTable = null;
            EpisodesTable = null;
            SectionsTable = null;
            AlbumsTable = null;
            TracksTable = null;
        }

        public static DataTable ReturnCorrectTable(bool directTable = false)
        {
            switch (ObjectProvider.CurrentContentType)
            {
                case ContentType.Movies:
                    return DecideFiltered();

                case ContentType.Music:
                    return directTable ? TracksTable : DecideFiltered();

                case ContentType.TvShows:
                    return directTable ? EpisodesTable : DecideFiltered();
            }

            return null; //fallback
        }

        public static DataTable DecideFiltered()
        {
            return Flags.IsFiltered ? FilteredTable : TitlesTable;
        }

        public static int GetTableIndexFromDgv(DataGridView dgv, DataTable table = null)
        {
            var val = 0;

            LoggingHelpers.RecordGeneralEntry($"Table-to-Grid match has been requested on '{dgv.Name}'");

            if (table == null)
            {
                table = ReturnCorrectTable();
                LoggingHelpers.RecordGeneralEntry("Table-to-Grid match was not given a table; defaulting to the standard table selector.");
            }

            var selRow = ((DataRowView)dgv.SelectedRows[0].DataBoundItem).Row.ItemArray;

            foreach (DataRow r in table.Rows)
            {
                var i = table.Rows.IndexOf(r);
                var m = selRow.Count(o => r.ItemArray.Contains(o));

                //ALL column values must match, so the match count will be equal to the cell length.
                if (m != selRow.Length) continue;

                val = i;
                break;
            }

            return val;
        }
    }
}