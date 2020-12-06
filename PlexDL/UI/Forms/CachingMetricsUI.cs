using PlexDL.Common.Caching;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using UIHelpers;

// ReSharper disable InconsistentNaming

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class CachingMetricsUI : DoubleBufferedForm
    {
        public CachingMetrics Metrics { get; set; } = null;

        public CachingMetricsUI()
        {
            InitializeComponent();
        }

        private DataTable GetMetrics()
        {
            try
            {
                //this is where all caching metrics data will be loaded
                var metricsTable = new DataTable(@"CachingMetrics");

                //create the columns
                var colMetricName = new DataColumn(@"Type", typeof(string));
                var colMetricSize = new DataColumn(@"Size", typeof(string));
                var colMetricCount = new DataColumn(@"RecordCount", typeof(int));

                //add the columns
                metricsTable.Columns.AddRange(new[]
                {
                    colMetricName,
                    colMetricSize,
                    colMetricCount
                });

                //~begin adding metrics information~
                //Server Lists
                metricsTable.Rows.Add(@"Server Lists", Metrics.ServerListsSize(), Metrics.SERVER_LISTS);

                //XML Documents
                metricsTable.Rows.Add(@"XML Data", Metrics.ApiXmlSize(), Metrics.API_DOCUMENTS);

                //Thumbnail Images
                metricsTable.Rows.Add(@"Images", Metrics.ThumbSize(), Metrics.THUMBNAIL_IMAGES);

                //Total
                metricsTable.Rows.Add(@"Total", Metrics.TotalCacheSize(), Metrics.TOTAL_CACHED);

                //return constructed table
                return metricsTable;
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"GetCachingMetricsTableError");
            }

            //default
            return null;
        }

        private void UpdateCachingDirectory()
        {
            try
            {
                //the directory must exist
                if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                {
                    lblCachingDirectoryValue.Text = CachingFileDir.RootCacheDirectory;
                    lblCachingDirectoryValue.ForeColor = Color.Black;
                }
                else
                {
                    lblCachingDirectoryValue.Text = @"Unknown";
                    lblCachingDirectoryValue.ForeColor = Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CachingMetricsDirUpdateError");
            }
        }

        private void InitialSetup()
        {
            try
            {
                //metrics information must be valid to proceed
                if (Metrics != null)
                {
                    //caching data location
                    UpdateCachingDirectory();

                    //apply data
                    dgvMain.DataSource = GetMetrics();
                }
                else
                    UIMessages.Warning(@"Metrics information was not configured on launch; data failed to load.");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, "CacheMetricsLoadError");

                //inform the user
                UIMessages.Error("There was an error whilst loading caching metrics:\n\n" + ex,
                    @"Load Error");

                //exit the form
                Close();
            }
        }

        private void CachingMetricsUI_Load(object sender, EventArgs e)
            => InitialSetup();

        private void BtnOK_Click(object sender, EventArgs e)
            => Close();
    }
}