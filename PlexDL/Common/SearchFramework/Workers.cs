using PlexDL.Common.Enums;
using PlexDL.WaitWindow;
using System;
using System.Data;
using System.Linq;
using UIHelpers;

namespace PlexDL.Common.SearchFramework
{
    public static class Workers
    {
        public static void GetSearchEnum(object sender, WaitWindowEventArgs e)
        {
            var table = (DataTable)e.Arguments[3];
            var column = (string)e.Arguments[2];
            var searchRule = (SearchRule)e.Arguments[1];
            var searchKey = (string)e.Arguments[0];
            //UIMessages.Info(SearchQuery.SqlSearchFromRule(column, searchKey, searchRule));
            var rowCollection = table.Select(SearchQuery.SqlSearchFromRule(column, searchKey, searchRule));
            e.Result = rowCollection;
        }

        public static DataTable GetSearchTable(DataRow[] rowCollection)
        {
            return rowCollection.CopyToDataTable();
        }

        public static DataTable GetFilteredTable(SearchData data, bool silent = true)
        {
            //UIMessages.Info(data.SearchTable.Rows.Count.ToString());
            //UIMessages.Info(data.SearchTable.Rows[0].ItemArray.Length.ToString());
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
                    UIMessages.Info(@"No Results Found for '" + data.SearchTerm + @"'");
            }

            //UIMessages.Info("Filtered Table:" + filteredTable.Rows.Count + "\nTitles Table:" + titlesTable.Rows.Count);
            return tblFiltered;
        }
    }
}