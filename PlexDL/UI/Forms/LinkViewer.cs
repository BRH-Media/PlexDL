using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Windows.Forms;

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class LinkViewer : DoubleBufferedForm
    {
        public LinkViewer()
        {
            InitializeComponent();
        }

        private bool CopyState { get; set; }

        /// <summary>
        /// The link that will be displayed
        /// </summary>
        public string Link { get; set; } = @"";

        /// <summary>
        /// Show a media link based on a PlexObject
        /// </summary>
        /// <param name="media">The PlexObject which contains the link</param>
        /// <param name="viewMode">View mode will toggle the 'download' GET parameter</param>
        public static void ShowLinkViewer(PlexObject media, bool viewMode = true)
            => ShowLinkViewer(viewMode
                ? media.StreamInformation.Links.View
                : media.StreamInformation.Links.Download);

        /// <summary>
        /// Show a generic HTTP link
        /// </summary>
        /// <param name="link">The link to display</param>
        public static void ShowLinkViewer(string link)
            => new LinkViewer { Link = link }.ShowDialog();

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                //update the button text
                btnCopy.Text = @"Copied!";

                //change the state for the timer
                CopyState = true;

                //save it to the clipboard, but only if the link is valid
                //i.e. not whitespace/null
                if (!string.IsNullOrWhiteSpace(Link)) Clipboard.SetText(Link);

                //restart the text change timer
                tmrBtnTxtUpdate.Stop();
                tmrBtnTxtUpdate.Start();
            }
            catch (Exception ex)
            {
                //log the error and then ignore it
                LoggingHelpers.RecordException(ex.Message, @"LinkCopyError");
            }
        }

        private void TmrBtnTxtUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                //only triggered if the 'Copy' button has been pressed
                if (!CopyState)
                    return;

                //stop the timer
                tmrBtnTxtUpdate.Stop();

                //reset button text
                btnCopy.Text = @"Copy";

                //reset flag
                CopyState = false;
            }
            catch (Exception)
            {
                //stop timer and ignore
                tmrBtnTxtUpdate.Stop();
            }
        }

        private void LinkViewer_Load(object sender, EventArgs e)
        {
            //set the link
            txtLink.Text = Link;

            //make sure it's not highlighted on start
            txtLink.SelectionStart = 0;
            txtLink.SelectionLength = 0;
        }
    }
}