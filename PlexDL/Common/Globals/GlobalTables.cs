using System.Data;
using System.Windows.Forms;

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

        public static DataTable ReturnCorrectTable(bool directTable = false)
        {
            if (Flags.IsTVShow)
            {
                if (directTable)
                    return EpisodesTable;
                else
                    return DecideFiltered();
            }
            else
            {
                return DecideFiltered();
            }
        }

        public static DataTable DecideFiltered()
        {
            if (Flags.IsFiltered)
                return FilteredTable;
            else
                return TitlesTable;
        }

        public static int GetTableIndexFromDgv(DataGridView dgv, DataTable table = null, string key = "title")
        {
            var sel = dgv.SelectedRows[0];
            if (table == null)
                table = ReturnCorrectTable();
            foreach (DataRow r in table.Rows)
                if (dgv.Columns.Contains(key))
                {
                    var titleValue = sel.Cells[key].Value.ToString();
                    if (r[key].ToString() == titleValue) return table.Rows.IndexOf(r);
                }

            //failsafe if nothing is discovered
            return 0;
        }
    }
}