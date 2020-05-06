using System.Data;
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
                return DecideFiltered();
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
            Logging.LoggingHelpers.RecordGenericEntry("Table-to-Grid match has been requested on '" + dgv.Name + "', using field '" + key + "'.");
            var sel = dgv.SelectedRows[0];
            if (table == null)
            {
                table = ReturnCorrectTable();
                Logging.LoggingHelpers.RecordGenericEntry("Table-to-Grid match was not given a table; defaulting to the standard table selector.");
            }
            bool fieldFound = false;
            foreach (DataRow r in table.Rows)
                if (dgv.Columns.Contains(key))
                {
                    fieldFound = true;
                    var titleValue = sel.Cells[key].Value.ToString();
                    if (r[key].ToString() == titleValue)
                    {
                        int idx = table.Rows.IndexOf(r);
                        Logging.LoggingHelpers.RecordGenericEntry("Table-To-Grid match has located the requested index: " + idx);
                        return idx;
                    }
                }

            //failsafe if nothing is discovered
            if (!fieldFound)
                Logging.LoggingHelpers.RecordException("Matching '" + dgv.Name + "' to '" + table.TableName +
                    "' with field '" + key + "', has failed as the field was not found in the provided grid. " +
                    "Index 0 has instead been reported.", "DTIndexGrabZero");
            else
                Logging.LoggingHelpers.RecordException("Matching '" + dgv.Name + "' to '" + table.TableName +
                    "' with field '" + key + "', has failed. Index 0 has instead been reported.", "DTIndexGrabZero");
            return 0;
        }
    }
}