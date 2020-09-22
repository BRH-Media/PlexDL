using PlexDL.Common.Enums;
using PlexDL.Common.Logging;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PlexDL.Common.Globals.Providers
{
    public static class TableManager
    {
        public static bool ActiveTableFilled => TableFilled(ReturnCorrectTable());

        private static bool TableFilled(DataTable table)
        {
            //check if the table isn't null. This check precedes the count check,
            //because if the table is null you can't check its properties, i.e. its row count.
            if (table == null) return false;

            //it isn't null, but does it have rows? If no, then it's invalid. If yes, then the table's valid.
            return table.Rows.Count > 0;
        }

        public static DataTable ReturnCorrectTable(bool directTable = false)
        {
            switch (ObjectProvider.CurrentContentType)
            {
                case ContentType.Movies:
                    return DecideFiltered();

                case ContentType.Music:
                    return directTable ? DataProvider.TracksProvider.GetRawTable() : DecideFiltered();

                case ContentType.TvShows:
                    return directTable ? DataProvider.EpisodesProvider.GetRawTable() : DecideFiltered();
            }

            return null; //fallback
        }

        public static DataTable DecideFiltered()
        {
            return Flags.IsFiltered ? DataProvider.FilteredProvider.GetRawTable() : DataProvider.TitlesProvider.GetRawTable();
        }

        public static int GetTableIndexFromDgv(DataGridView dgv, DataTable table = null)
        {
            //MessageBox.Show(@"Hello!");
            LoggingHelpers.RecordGeneralEntry($"Table-to-Grid match has been requested on '{dgv.Name}'");

            //if the table's null we can't do anything with it, because there's no data to work with.
            if (table == null)
            {
                //set the table object to the default table, log it, and keep going.
                table = ReturnCorrectTable();
                LoggingHelpers.RecordGeneralEntry(
                    "Table-to-Grid match was not given a table; defaulting to the standard table selector.");
            }

            var selRow = ((DataRowView)dgv.SelectedRows[0].DataBoundItem).Row
                .ItemArray; //array of cell values from the selected row
            var val = 0; //value to return back to the caller

            //go through every row in the TABLE
            foreach (DataRow r in table.Rows)
            {
                //get the index number of the row we're currently processing
                //NOTE: This is the index inside of the TABLE NOT THE GRID
                var i = table.Rows.IndexOf(r);

                //represents the total matches so far (how many cells in the current
                //TABLE row match the selected GRID row)
                var m = 0;

                //cycle through each cell value in the currently selected GRID row
                foreach (var o in selRow)
                    //does the TABLE row contain the current cell value of the GRID row?
                    if (r.ItemArray.Contains(o))
                        m++; //increment the match counter
                    else
                        break; //if one wrong match is encountered, break immediately. This will drastically improve lookup speeds.

                //ALL column values must match, so the match count will be equal to the cell length.
                if (m != selRow.Length) continue;

                val = i; //set the return value to the current TABLE row index

                break; //ONLY triggered if the match counter equals the selected row's cell count.
            }

            return val;
        }
    }
}