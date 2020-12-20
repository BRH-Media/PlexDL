using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

// ReSharper disable InvertIf

namespace PlexDL.Common.Renderers.Forms.GridView
{
    /// <summary>
    /// Holder class for the data renderer(s)
    /// </summary>
    public static class GenericViewRenderer
    {
        /// <summary>
        /// Renders data into a DataGridView based on the specified RenderStruct
        /// </summary>
        /// <param name="info"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DataTable RenderView(GenericRenderStruct info, DataGridView target)
        {
            try
            {
                //check if the data we need was set (problems will arise otherwise)
                if (info != null && target != null)

                    //check if the two data pairs are equal length (we don't want indexing errors)
                    if (info.WantedColumns.Count == info.WantedCaption.Count)

                        //ensure the count is valid
                        if (info.WantedColumns.Count > 0)
                        {
                            //store the columns we want vs. the columns available (so we don't error out)
                            var currentColumns = new List<string>();
                            var wantedColumns = info.WantedColumns;
                            var wantedCaption = info.WantedCaption;

                            //data storage
                            var viewBind = new DataView(info.Data);

                            //check if appropriate columns are part of the table; then we can verify and add them to the view.
                            foreach (DataColumn c in info.Data.Columns)

                                //check if the column name exists in our wanted columns
                                if (wantedColumns.Contains(c.ColumnName))
                                {
                                    //it does, so grab its index, grab its associated caption, then add both to the available columns (Current Columns)
                                    var index = wantedColumns.IndexOf(c.ColumnName);
                                    var caption = wantedCaption[index];
                                    currentColumns.Add(c.ColumnName);

                                    //set the column's caption to the one we just got from the index
                                    c.Caption = caption;
                                }

                            //match the order of the current columns with the wanted columns (because things get added in a different way)
                            currentColumns = Methods.OrderMatch(wantedColumns, currentColumns);

                            //create a new binding DataView based on the columns we want and have available
                            var dgvBind = viewBind.ToTable(false, currentColumns.ToArray());

                            //do the bind
                            DoDataBind(target, dgvBind, info);

                            //return the processed data (data with the wanted columns) in a DataTable
                            return dgvBind;
                        }
                        else

                            //do the bind with the default data (unfiltered)
                            DoDataBind(target, info.Data, info);

                //null check to control the output
                return info != null
                    ? info.Data
                    : new DataTable();
            }
            catch (Exception ex)
            {
                //log and do nothing
                LoggingHelpers.RecordException(ex.Message, @"GenericViewRendererProcessingError");
            }

            //default result
            return new DataTable();
        }

        private static void DoDataBind(DataGridView target, DataTable bindData, GenericRenderStruct info)
        {
            try
            {
                //check if the DataGridView needs to be invoked first
                if (target.InvokeRequired)

                    //invoke the DataGridView so we don't thread-lock
                    target.BeginInvoke((MethodInvoker)delegate { DoDataBind(target, bindData, info); });
                else
                {
                    //we don't need to invoke, so just continue without it.
                    //bind the data to the grid ("render" the data)
                    target.DataSource = bindData;

                    //set the captions
                    Methods.SetHeaderText(target, info.Data);

                    //re-render the control
                    target.Refresh();
                }
            }
            catch (Exception ex)
            {
                //log and do nothing
                LoggingHelpers.RecordException(ex.Message, @"GenericViewRendererBindError");
            }
        }
    }
}