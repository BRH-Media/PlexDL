using PlexDL.Common;
using PlexDL.Common.Structures;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class DirectConnect : Form
    {
        //public bool ConnectionStarted { get; set; } = false;
        public bool Success { get; set; } = false;

        //specifies whether PlexDL should fill txtServerIP.Text with "127.0.0.1"
        public bool LoadLocalLink { get; set; } = false;

        public DirectConnect()
        {
            InitializeComponent();
        }

        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

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
                    //MessageBox.Show(uri);
                    if (WebCheck.TestUrl(uri).ConnectionSuccess)
                    {
                        //ConnectionStarted = true;
                        Success = true;
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

        private void DirectConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            //nothing
        }

        private void DirectConnect_Load(object sender, EventArgs e)
        {
            if (LoadLocalLink)
            {
                txtServerIP.Text = @"127.0.0.1";
                txtServerIP.ReadOnly = true;
                Text = @"Local Machine Connection";
            }
        }
    }
}