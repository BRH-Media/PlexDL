using PlexDL.Common.Logging;
using PlexDL.Common.Renderers;
using PlexDL.UI;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.SearchFramework
{
    public static class Search
    {
        private static void RenderWithNoStruct(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var dataToRender = (DataTable)e.Arguments[1];
            var gridToRender = (DataGridView)e.Arguments[0];
            if (gridToRender.InvokeRequired)
            {
                gridToRender.BeginInvoke((MethodInvoker)delegate
                {
                    gridToRender.DataSource = dataToRender;
                });
            }
            else
                gridToRender.DataSource = dataToRender;
        }

        private static void RenderWithStruct(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var gridToRender = (DataGridView)e.Arguments[0];
            var renderInfo = (RenderStruct)e.Arguments[1];
            GenericRenderer.RenderView(renderInfo, gridToRender);
        }

        private static void RenderResult(DataGridView dgvTarget, RenderStruct info)
        {
            string message = "Preparing results";
            if (dgvTarget != null && info != null)
                if (info.Data != null)
                    WaitWindow.WaitWindow.Show(RenderWithStruct, message, new object[] { dgvTarget, info });
        }

        private static void RenderResult(DataGridView dgvTarget, DataTable data)
        {
            string message = "Preparing results";
            if (dgvTarget != null && data != null)
                WaitWindow.WaitWindow.Show(RenderWithNoStruct, message, new object[] { dgvTarget, data });
        }

        public static bool RunTitleSearch(DataGridView dgvTarget, DataTable tableToSearch, RenderStruct dgvRenderInfo, bool copyToGlobalTables = false)
        {
            try
            {
                if (dgvTarget.Rows.Count > 0)
                {
                    var start = new SearchOptions
                    {
                        SearchCollection = tableToSearch
                    };
                    var result = SearchForm.ShowSearch(start);
                    if (!string.IsNullOrEmpty(result.SearchTerm) && !string.IsNullOrEmpty(result.SearchColumn))
                    {
                        SearchData data = new SearchData()
                        {
                            SearchColumn = result.SearchColumn,
                            SearchTerm = result.SearchTerm,
                            SearchRule = result.SearchRule,
                            SearchTable = tableToSearch
                        };

                        DataTable filteredTable = Workers.GetFilteredTable(data, false);

                        if (filteredTable != null)
                        {
                            if (dgvRenderInfo == null)
                                RenderResult(dgvTarget, filteredTable);
                            else
                            if (dgvRenderInfo.Data != null)
                            {
                                dgvRenderInfo.Data = filteredTable;
                                RenderResult(dgvTarget, dgvRenderInfo);
                            }
                            if (copyToGlobalTables)
                                Globals.GlobalTables.FilteredTable = filteredTable;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
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
            return RunTitleSearch(dgvTarget, tableToSearch, null, copyToGlobalTables);
        }

        public static bool RunTitleSearch(DataGridView dgvTarget, RenderStruct dgvRenderInfo, bool copyToGlobalTables = false)
        {
            if (dgvTarget != null && dgvRenderInfo != null)
                if (dgvRenderInfo.Data != null)
                    return RunTitleSearch(dgvTarget, dgvRenderInfo.Data, dgvRenderInfo, copyToGlobalTables);
            return false;
        }
    }
}