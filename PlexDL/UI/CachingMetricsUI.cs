using System;
using System.Windows.Forms;
using PlexDL.Common.Caching;
using PlexDL.Common.Logging;

namespace PlexDL.UI
{
    public partial class CachingMetricsUI : Form
    {
        public CachingMetrics Metrics { get; set; } = null;

        public CachingMetricsUI()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CachingMetricsUI_Load(object sender, EventArgs e)
        {
            try
            {
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
                MessageBox.Show("There was an error whilst loading caching metrics:\n\n" + ex, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}