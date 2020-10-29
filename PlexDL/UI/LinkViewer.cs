using PlexDL.Common.Components;
using PlexDL.Common.Logging;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class LinkViewer : DoubleBufferedForm
    {
        public LinkViewer()
        {
            InitializeComponent();
        }

        private bool CopyState { get; set; }
        public string Link { get; set; } = @"";

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // update the button text
                btnCopy.Text = @"Copied!";

                // change the state for the timer
                CopyState = true;

                // save it to the clipboard, but only if the link is valid
                // i.e. not whitespace/null
                if (!string.IsNullOrWhiteSpace(Link)) Clipboard.SetText(Link);

                // restart the text change timer
                tmrBtnTxtUpdate.Stop();
                tmrBtnTxtUpdate.Start();
            }
            catch (Exception ex)
            {
                // log the error and then ignore it
                LoggingHelpers.RecordException(ex.Message, @"LinkCopyError");
            }
        }

        private void TmrBtnTxtUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!CopyState) return;

                tmrBtnTxtUpdate.Stop();
                btnCopy.Text = @"Copy";
                CopyState = false;
            }
            catch (Exception)
            {
                // stop timer and ignore
                tmrBtnTxtUpdate.Stop();
            }
        }

        private void LinkViewer_Load(object sender, EventArgs e)
        {
            txtLink.Text = Link;
        }
    }
}