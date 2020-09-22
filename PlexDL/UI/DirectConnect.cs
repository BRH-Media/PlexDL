using PlexDL.Common;
using PlexDL.Common.Structures;
using System;
using System.Drawing;
using System.Windows.Forms;
using PlexDL.Common.Structures.AppOptions;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class DirectConnect : Form
    {
        public DirectConnect()
        {
            InitializeComponent();
        }

        //public bool ConnectionStarted { get; set; } = false;
        public bool Success { get; set; }

        //specifies whether PlexDL should fill txtServerIP.Text with "127.0.0.1"
        public bool LoadLocalLink { get; set; } = false;

        //force "Use a different token" check status
        public bool DifferentToken
        {
            get => chkToken.Checked;
            set => chkToken.Checked = value;
        }

        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

        private static bool VerifyToken(string token)
        {
            return token.Length == 20 && !string.IsNullOrWhiteSpace(token);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServerIP.Text))
            {
                UIMessages.Error(@"Incorrectly formatted address. Please enter your server address correctly.",
                    @"Validation Error");
            }
            else if (!int.TryParse(txtServerPort.Text, out _))
            {
                UIMessages.Error(@"Incorrectly formatted port. Please enter your port correctly.",
                    @"Validation Error");
            }
            else if (!VerifyToken(txtToken.Text) && chkToken.Checked)
            {
                UIMessages.Error(@"Incorrectly formatted token. Please enter your token correctly.",
                    @"Validation Error");
            }
            else
            {
                if (Convert.ToInt32(txtServerPort.Text) <= 65535)
                {
                    ConnectionInfo.PlexAddress = txtServerIP.Text;
                    ConnectionInfo.PlexPort = Convert.ToInt32(txtServerPort.Text);
                    var uri = chkToken.Checked
                        ? $"http://{ConnectionInfo.PlexAddress}:{ConnectionInfo.PlexPort}/?X-Plex-Token={txtToken.Text}"
                        : $"http://{ConnectionInfo.PlexAddress}:{ConnectionInfo.PlexPort}/?X-Plex-Token={ConnectionInfo.PlexAccountToken}";
                    //UIMessages.Info(uri);
                    var testUrl = WebCheck.TestUrl(uri);
                    if (testUrl.ConnectionSuccess)
                    {
                        //if the user entered a different token then we need to apply that change
                        if (chkToken.Checked)
                            ConnectionInfo.PlexAccountToken = txtToken.Text;

                        Success = true;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        if (testUrl.LastException != null && testUrl.StatusCode != "Undetermined")
                            UIMessages.Error(
                                $"Could not connect: {testUrl.StatusCode}\n\nYou can exit the dialog by clicking 'Cancel'.",
                                @"Network Error");
                        else
                            UIMessages.Error(
                                "Could not connect; the web server either returned an incorrect response, or the client could not establish a connection.\n\nYou can exit the dialog by clicking 'Cancel'.",
                                @"Network Error");
                    }
                }
                else
                {
                    UIMessages.Error(@"Port out of range", @"Validation Error");
                }
            }
        }

        private void DirectConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            //nothing
        }

        private void CheckDiffToken()
        {
            if (chkToken.Checked)
                DiffToken();
            else
                NoDiffToken();
        }

        private void NoDiffToken()
        {
            Height = 178;
            pnlControls.Location = new Point(pnlControls.Location.X, 110);
            gbConnectionInformation.Height = 92;
        }

        private void DiffToken()
        {
            Height = 204;
            pnlControls.Location = new Point(pnlControls.Location.X, 136);
            gbConnectionInformation.Height = 118;
        }

        private void DirectConnect_Load(object sender, EventArgs e)
        {
            //load given values into UI
            txtServerIP.Text = ConnectionInfo.PlexAddress;
            txtServerPort.Text = ConnectionInfo.PlexPort.ToString();
            txtToken.Text = ConnectionInfo.PlexAccountToken;

            if (LoadLocalLink)
            {
                txtServerIP.Text = @"127.0.0.1";
                txtServerIP.ReadOnly = true;
                Text = @"Local Machine Connection";
            }

            CheckDiffToken();
        }

        private void chkToken_CheckedChanged(object sender, EventArgs e)
        {
            CheckDiffToken();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}