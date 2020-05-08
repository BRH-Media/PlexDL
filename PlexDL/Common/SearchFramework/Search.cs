using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.UI;
using PlexDL.WaitWindow;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.SearchFramework
{
    public static class Search
    {
        private static void RenderWithNoStruct(object sender, WaitWindowEventArgs e)
        {
            var dataToRender = (DataTable)e.Arguments[1];
            var gridToRender = (DataGridView)e.Arguments[0];
            if (gridToRender.InvokeRequired)
                gridToRender.BeginInvoke((MethodInvoker)delegate
                {
                    gridToRender.DataSource = dataToRender;
                });
            else
                gridToRender.DataSource = dataToRender;
            e.Result = dataToRender;
        }

        private static void RenderWithStruct(object sender, WaitWindowEventArgs e)
        {
            var gridToRender = (DataGridView)e.Arguments[0];
            var renderInfo = (RenderStruct)e.Arguments[1];
            e.Result = GenericRenderer.RenderView(renderInfo, gridToRender);
        }

        private static DataTable RenderResult(DataGridView dgvTarget, RenderStruct info)
        {
            var message = "Preparing results";
            if (dgvTarget != null && info != null)
                if (info.Data != null)
                    return (DataTable)WaitWindow.WaitWindow.Show(RenderWithStruct, message, dgvTarget, info);
            return null;
        }

        private static DataTable RenderResult(DataGridView dgvTarget, DataTable data)
        {
            var message = "Preparing results";
            if (dgvTarget != null && data != null)
                return (DataTable)WaitWindow.WaitWindow.Show(RenderWithNoStruct, message, dgvTarget, data);
            return null;
        }

        public static bool RunTitleSearch(DataGridView dgvTarget, RenderStruct dgvRenderInfo, bool copyToGlobalTables = false)
        {
            try
            {
                if (dgvTarget.Rows.Count > 0)
                {
                    var start = new SearchOptions
                    {
                        SearchCollection = dgvRenderInfo.Data,
                        ColumnCollection = dgvRenderInfo.WantedColumns
                    };
                    var result = SearchForm.ShowSearch(start, dgvRenderInfo.WantedColumns);
                    if (!string.IsNullOrEmpty(result.SearchTerm) && !string.IsNullOrEmpty(result.SearchColumn))
                    {
                        var data = new SearchData
                        {
                            SearchColumn = result.SearchColumn,
                            SearchTerm = result.SearchTerm,
                            SearchRule = result.SearchRule,
                            SearchTable = dgvRenderInfo.Data
                        };

                        //MessageBox.Show(data.SearchRule.ToString());

                        DataTable filteredTable = Workers.GetFilteredTable(data, false);
                        DataTable filteredView = null;
                        //MessageBox.Show(filteredTable.Rows.Count.ToString());

                        if (filteredTable == null)
                            return false;
                        if (dgvRenderInfo == null)
                        {
                            filteredView = RenderResult(dgvTarget, filteredTable);
                        }
                        else if (dgvRenderInfo.Data != null)
                        {
                            dgvRenderInfo.Data = filteredTable;
                            filteredView = RenderResult(dgvTarget, dgvRenderInfo);
                        }

                        if (copyToGlobalTables)
                        {
                            GlobalTables.FilteredTable = filteredTable;
                            GlobalViews.FilteredViewTable = filteredView;
                        }
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool RunTitleSearch(DataGridView dgvTarget, DataTable tableToSearch, bool copyToGlobalTables = false)
        {
            return RunTitleSearch(dgvTarget, new RenderStruct { Data = tableToSearch }, copyToGlobalTables);
        }
    }
}