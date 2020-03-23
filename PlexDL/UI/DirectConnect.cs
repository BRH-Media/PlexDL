using PlexDL.Common;
using PlexDL.Common.Structures;
using PlexDL.WaitWindow;
using System;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class DirectConnect : Form
    {
        public bool ConnectionStarted;

        public DirectConnect()
        {
            InitializeComponent();
        }

        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

        public static bool WebSiteIsAvailable(string Url)
        {
            var Message = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.Timeout = 4500;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += (Message.Length > 0 ? "\n" : "") + ex.Message;
            }

            return Message.Length == 0;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!Methods.ValidateIPv4(txtServerIP.Text))
            {
                MessageBox.Show(@"Invalid IP Address", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(txtServerPort.Text, out var r))
            {
                MessageBox.Show(@"Port must be numeric", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Convert.ToInt32(txtServerPort.Text) <= 65535)
                {
                    ConnectionInfo.PlexAddress = txtServerIP.Text;
                    ConnectionInfo.PlexPort = Convert.ToInt32(txtServerPort.Text);
                    var uri = "http://" + ConnectionInfo.PlexAddress + ":" + ConnectionInfo.PlexPort +
                              "/?X-Plex-Token=" + ConnectionInfo.PlexAccountToken;
                    var available = (bool)WaitWindow.WaitWindow.Show(CheckWebWorker, "Checking Connection", uri);
                    if (available)
                    {
                        ConnectionStarted = true;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        //MessageBox.Show(uri);
                        MessageBox.Show(
                            "Could not connect; the web server either returned an incorrect response, or the client could not establish a connection.\n\nYou can exit the dialog by the button in the top-right.",
                            @"Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(@"Port out of range", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void CheckWebWorker(object sender, WaitWindowEventArgs e)
        {
            var uri = (string)e.Arguments[0];
            e.Result = WebSiteIsAvailable(uri);
        }

        private void DirectConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            //nothing
        }

        private void DirectConnect_Load(object sender, EventArgs e)
        {
            ConnectionStarted = false;
        }
    }
}