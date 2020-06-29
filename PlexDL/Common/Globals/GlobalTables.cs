using PlexDL.Common.Enums;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PlexDL.Common.Globals
{
    public static class GlobalTables
    {
        public static DataTable TitlesTable { get; set; }
        public static DataTable FilteredTable { get; set; }
        public static DataTable SeasonsTable { get; set; }
        public static DataTable EpisodesTable { get; set; }
        public static DataTable SectionsTable { get; set; }
        public static DataTable AlbumsTable { get; set; }
        public static DataTable TracksTable { get; set; }

        public static DataTable ReturnCorrectTable(bool directTable = false)
        {
            switch (GlobalStaticVars.CurrentContentType)
            {
                case ContentType.Movies:
                    return DecideFiltered();

                case ContentType.Music:
                    if (directTable)
                        return TracksTable;
                    else
                        return DecideFiltered();

                case ContentType.TvShows:
                    if (directTable)
                        return EpisodesTable;
                    else
                        return DecideFiltered();
            }

            return null; //fallback
        }

        public static DataTable DecideFiltered()
        {
            if (Flags.IsFiltered)
                return FilteredTable;
            else
                return TitlesTable;
        }

        public static int GetTableIndexFromDgv(DataGridView dgv, DataTable table = null)
        {
            int val = 0;

            Logging.LoggingHelpers.RecordGeneralEntry($"Table-to-Grid match has been requested on '{dgv.Name}'");

            if (table == null)
            {
                table = ReturnCorrectTable();
                Logging.LoggingHelpers.RecordGeneralEntry("Table-to-Grid match was not given a table; defaulting to the standard table selector.");
            }

            var selRow = ((DataRowView)dgv.SelectedRows[0].DataBoundItem).Row.ItemArray;

            foreach (DataRow r in table.Rows)
            {
                var m = 0;
                var i = table.Rows.IndexOf(r);
                foreach (var o in selRow)
                    if (r.ItemArray.Contains(o))
                        m++;
                if (m == selRow.Length)
                {
                    val = i;
                    break;
                }
            }

            return val;
        }
    }
}