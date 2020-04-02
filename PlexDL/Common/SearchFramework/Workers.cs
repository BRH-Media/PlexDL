using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PlexDL.WaitWindow;

namespace PlexDL.Common.SearchFramework
{
    public static class Workers
    {
        public static void GetSearchEnum(object sender, WaitWindowEventArgs e)
        {
            var table = (DataTable)e.Arguments[3];
            var column = (string)e.Arguments[2];
            var searchRule = (int)e.Arguments[1];
            var searchKey = (string)e.Arguments[0];
            var rowCollection = table.Select(SearchRuleIDs.SqlSearchFromRule(column, searchKey, searchRule));
            e.Result = rowCollection;
        }

        public static DataTable GetSearchTable(DataRow[] rowCollection)
        {
            return rowCollection.CopyToDataTable();
        }

        public static DataTable GetFilteredTable(SearchData data, bool silent = true)
        {
            //MessageBox.Show(data.SearchTable.Rows[0].ItemArray.Length.ToString());
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            DataTable tblFiltered = null;
            var rowCollection =
            (DataRow[])WaitWindow.WaitWindow.Show(GetSearchEnum, "Filtering Records", data.SearchTerm, data.SearchRule, data.SearchColumn, data.SearchTable);
            if (rowCollection.Any())
            {
                tblFiltered = GetSearchTable(rowCollection);
            }
            else
            {
                if (!silent)
                    MessageBox.Show(@"No Results Found for '" + data.SearchTerm + @"'", @"Message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }

            //MessageBox.Show("Filtered Table:" + filteredTable.Rows.Count + "\nTitles Table:" + titlesTable.Rows.Count);
            return tblFiltered;
        }
    }
}