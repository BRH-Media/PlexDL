using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.Forms.GridView;
using PlexDL.UI.Forms;
using PlexDL.WaitWindow;
using System;
using System.Data;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

namespace PlexDL.Common.SearchFramework
{
    /// <summary>
    /// Provides methods used to filter data and render it to a grid
    /// </summary>
    public static class Search
    {
        private static void RenderWithNoStruct(object sender, WaitWindowEventArgs e)
        {
            //there must always be a correct argument count
            if (e.Arguments.Count == 2)
            {
                //GUI grid that will be re-rendered
                var gridToRender = (DataGridView)e.Arguments[0];

                //the raw data that will be filtered
                var dataToRender = (DataTable)e.Arguments[1];

                if (gridToRender.InvokeRequired)
                    gridToRender.BeginInvoke((MethodInvoker)delegate { gridToRender.DataSource = dataToRender; });
                else
                    gridToRender.DataSource = dataToRender;

                e.Result = dataToRender;
            }
        }

        private static void RenderWithStruct(object sender, WaitWindowEventArgs e)
        {
            //there must always be a correct argument count
            if (e.Arguments.Count == 2)
            {
                //GUI grid that will be re-rendered
                var gridToRender = (DataGridView)e.Arguments[0];

                //the information used to dictate how the GUI grid will be re-rendered
                var renderInfo = (GenericRenderStruct)e.Arguments[1];

                //the result is a returned filtered view table
                e.Result = GenericViewRenderer.RenderView(renderInfo, gridToRender);
            }
        }

        private static DataTable RenderResult(DataGridView dgvTarget, GenericRenderStruct info)
        {
            const string message = "Preparing results";
            if (dgvTarget == null || info == null)
                return null;
            if (info.Data != null)
                return (DataTable)WaitWindow.WaitWindow.Show(RenderWithStruct, message, dgvTarget, info);

            //default
            return null;
        }

        private static DataTable RenderResult(DataGridView dgvTarget, DataTable data)
        {
            const string message = "Preparing results";
            if (dgvTarget != null && data != null)
                return (DataTable)WaitWindow.WaitWindow.Show(RenderWithNoStruct, message, dgvTarget, data);

            //default
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dgvTarget"></param>
        /// <param name="dgvRenderInfo"></param>
        /// <param name="searchContext"></param>
        /// <param name="copyToGlobalTables"></param>
        /// <returns></returns>
        public static bool RunTitleSearch(DataGridView dgvTarget, GenericRenderStruct dgvRenderInfo, SearchData searchContext,
            bool copyToGlobalTables = false)
        {
            try
            {
                //filter the data down using the search context first
                var filteredTable = Workers.GetFilteredTable(searchContext, false);

                //new table to store the filtered view information (not data)
                DataTable filteredView = null;

                //the filtered data cannot be null; this indicates a search failure
                if (filteredTable == null)
                    return false;

                //the rendering information being null will trigger different outcomes
                if (dgvRenderInfo == null)

                    //a new view is generated for the grid and is assigned to filteredView
                    filteredView = RenderResult(dgvTarget, filteredTable);

                //if it isn't null, and the data is contains isn't null
                else if (dgvRenderInfo.Data != null)
                {
                    //the new rendering data is assigned to the rendering information struct
                    dgvRenderInfo.Data = filteredTable;

                    //a new view is generated for the grid and is assigned to filteredView
                    filteredView = RenderResult(dgvTarget, dgvRenderInfo);
                }

                //don't continue and copy to globals if the flag is disabled
                if (!copyToGlobalTables)
                    return true;

                //assign the raw filtered data
                DataProvider.FilteredProvider.SetRawTable(filteredTable);

                //assign the filtered data view
                DataProvider.FilteredProvider.SetViewTable(filteredView);

                //report successful outcome
                return true;
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "SearchError");

                //report the error to the user
                UIMessages.Error(ex.ToString());

                //error occurred; return false (search didn't succeed)
                return false;
            }
        }

        /// <summary>
        /// Run a search and automatically filter and render the provided grid
        /// </summary>
        /// <param name="dgvTarget">The grid to render</param>
        /// <param name="searchContext">Search contextual information used to provide data to the framework; this approach is generally not recommended</param>
        /// <param name="copyToGlobalTables">Specifies whether the search framework should replace the data in the global registry</param>
        /// <returns></returns>
        public static bool RunTitleSearch(DataGridView dgvTarget, SearchData searchContext,
            bool copyToGlobalTables = false)
            => RunTitleSearch(dgvTarget, new GenericRenderStruct { Data = searchContext.SearchTable }, searchContext, copyToGlobalTables);

        /// <summary>
        /// Run a search and automatically filter and render the provided grid
        /// </summary>
        /// <param name="dgvTarget">The grid to render</param>
        /// <param name="dgvRenderInfo">The rendering information used to provide search parameters and filtering preferences</param>
        /// <param name="copyToGlobalTables">Specifies whether the search framework should replace the data in the global registry</param>
        /// <returns></returns>
        public static bool RunTitleSearch(DataGridView dgvTarget, GenericRenderStruct dgvRenderInfo,
            bool copyToGlobalTables = false)
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
                    var result = SearchForm.ShowSearch(start);
                    if (!string.IsNullOrEmpty(result.SearchTerm) && !string.IsNullOrEmpty(result.SearchColumn))
                    {
                        var data = new SearchData
                        {
                            SearchColumn = result.SearchColumn,
                            SearchTerm = result.SearchTerm,
                            SearchRule = result.SearchRule,
                            SearchTable = dgvRenderInfo.Data
                        };

                        return RunTitleSearch(dgvTarget, dgvRenderInfo, data, copyToGlobalTables);
                    }

                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "SearchError");
                UIMessages.Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Run a search and automatically filter and render the provided grid
        /// </summary>
        /// <param name="dgvTarget">The grid to render</param>
        /// <param name="tableToSearch">The data to search (original, untouched data)</param>
        /// <param name="copyToGlobalTables">Specifies whether the search framework should replace the data in the global registry</param>
        /// <returns></returns>
        public static bool RunTitleSearch(DataGridView dgvTarget, DataTable tableToSearch,
            bool copyToGlobalTables = false)
            => RunTitleSearch(dgvTarget, new GenericRenderStruct { Data = tableToSearch }, copyToGlobalTables);
    }
}