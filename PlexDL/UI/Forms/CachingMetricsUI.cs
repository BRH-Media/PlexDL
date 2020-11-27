using PlexDL.Common.Caching;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using System;
using UIHelpers;

namespace PlexDL.UI.Forms
{
    public partial class CachingMetricsUi : DoubleBufferedForm
    {
        public CachingMetricsUi()
        {
            InitializeComponent();
        }

        public CachingMetrics Metrics { get; set; } = null;

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CachingMetricsUI_Load(object sender, EventArgs e)
        {
            try
            {
                //CACHING DATA LOCATION
                lblCachingDir.Text = CachingFileDir.RootCacheDirectory;

                //SERVER LISTS
                lblNumberServerListsValue.Text = Metrics.SERVER_LISTS.ToString();
                lblSizeServerListsValue.Text = Metrics.ServerListsSize();

                //THUMBNAIL IMAGES
                lblNumberThumbsCachedValue.Text = Metrics.THUMBNAIL_IMAGES.ToString();
                lblSizeThumbsCachedValue.Text = Metrics.ThumbSize();

                //API XML DOCUMENTS
                lblNumberXmlCachedValue.Text = Metrics.API_DOCUMENTS.ToString();
                lblSizeXmlCachedValue.Text = Metrics.ApiXmlSize();

                //TOTAL CACHED
                lblTotalAmountCachedValue.Text = Metrics.TOTAL_CACHED.ToString();
                lblTotalSizeCachedValue.Text = Metrics.TotalCacheSize();
            }
            catch (Exception ex)
            {
                //log then inform user before exiting
                LoggingHelpers.RecordException(ex.Message, "CacheMetricsLoadError");
                UIMessages.Error("There was an error whilst loading caching metrics:\n\n" + ex,
                    @"Load Error");
                Close();
            }
        }
    }
}