using inet;
using PlexDL.Common.BarcodeHandler.QRCode.OnlineProvider;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using PlexDL.WaitWindow;
using System;
using System.Windows.Forms;

// ReSharper disable InvertIf
// ReSharper disable InconsistentNaming

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

        public bool QRCode { get; set; }

        /// <summary>
        /// Show a media link based on a PlexObject
        /// </summary>
        /// <param name="media">The PlexObject which contains the link</param>
        /// <param name="viewMode">View mode will toggle the 'download' GET parameter</param>
        /// <param name="qrCode"></param>
        public static void ShowLinkViewer(PlexObject media, bool viewMode = true, bool qrCode = true)
            => ShowLinkViewer(viewMode
                ? media.StreamInformation.Links.View
                : media.StreamInformation.Links.Download, qrCode);

        /// <summary>
        /// Show a generic HTTP link
        /// </summary>
        /// <param name="link">The link to display</param>
        /// <param name="qrCode"></param>
        public static void ShowLinkViewer(string link, bool qrCode = true)

            //create new LinkViewer form and display it
            => new LinkViewer { Link = link, QRCode = qrCode }.ShowDialog();

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
                if (!string.IsNullOrWhiteSpace(Link))
                    Clipboard.SetText(Link);

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

        private void GenerateQRCode(object sender, WaitWindowEventArgs e)
            => GenerateQRCode(false);

        private void GenerateQRCode(bool waitWindow = true)
        {
            //check if multi-threaded
            if (waitWindow)

                //multi-threaded waitwindow
                WaitWindow.WaitWindow.Show(GenerateQRCode, @"Generating code");
            else
            {
                try
                {
                    //code generation handler
                    var codeImage = new QROnlineProvider(Link);

                    //generate code
                    if (codeImage.Fetch())

                        //apply new image if not null
                        if (codeImage.CodeImage != null)

                            picQRCode.BackgroundImage = codeImage.CodeImage;
                }
                catch (Exception ex)
                {
                    //log error
                    LoggingHelpers.RecordException(ex.Message, @"GenerateQRCodeError");
                }
            }
        }

        private void LinkViewer_Load(object sender, EventArgs e)
        {
            //set the link
            txtLink.Text = Link;

            //growth factor
            const double growth = 3.019d;

            //new height
            var newHeight = (int)Math.Round(Height * growth);

            //is QR code functionality enabled and do we have an internet connection?
            if (QRCode && Internet.IsConnected)
            {
                //change form size
                Height = newHeight;

                //generate code
                GenerateQRCode();

                //enable picture visibility
                picQRCode.Visible = true;
            }

            //make sure it's not highlighted on start
            txtLink.SelectionStart = 0;
            txtLink.SelectionLength = 0;
        }
    }
}