﻿using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI.Forms
{
    public partial class DirectConnect : DoubleBufferedForm
    {
        public DirectConnect()
        {
            InitializeComponent();
        }

        //public bool ConnectionStarted { get; set; } = false;
        public bool Success { get; set; }

        //mode specification
        public DirectConnectMode Mode { get; set; } = DirectConnectMode.Normal;

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
                    //HTTP or HTTPS
                    var protocol = chkForceHttps.Checked ? @"https" : @"http";

                    //server information
                    ConnectionInfo.PlexAddress = txtServerIP.Text;
                    ConnectionInfo.PlexPort = Convert.ToInt32(txtServerPort.Text);

                    //URI to test
                    var uri = chkToken.Checked
                        ? $"{protocol}://{ConnectionInfo.PlexAddress}:{ConnectionInfo.PlexPort}/?X-Plex-Token={txtToken.Text}"
                        : $"{protocol}://{ConnectionInfo.PlexAddress}:{ConnectionInfo.PlexPort}/?X-Plex-Token={ConnectionInfo.PlexAccountToken}";
                    //UIMessages.Info(uri);

                    var testUrl = WebCheck.TestUrl(uri);
                    if (testUrl.ConnectionSuccess)
                    {
                        //if the user entered a different token then we need to apply that change
                        if (chkToken.Checked)
                            ConnectionInfo.PlexAccountToken = txtToken.Text;

                        //set the Force HTTPS value
                        Flags.IsHttps = chkForceHttps.Checked;

                        //finalise the result
                        Success = true;
                        DialogResult = DialogResult.OK;

                        //close the form
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
            //GUI status
            txtToken.Visible = false;
            txtToken.Enabled = false;

            //realign the form
            Height = 185;
        }

        private void DiffToken()
        {
            //GUI status
            txtToken.Visible = true;
            txtToken.Enabled = true;

            //realign the form
            Height = 212;
        }

        private void DirectConnect_Load(object sender, EventArgs e)
        {
            //load given values into UI
            txtServerIP.Text = ConnectionInfo.PlexAddress;
            txtServerPort.Text = ConnectionInfo.PlexPort.ToString();
            txtToken.Text = ConnectionInfo.PlexAccountToken;

            //set active text
            ActiveControl = txtServerIP;

            //setup HTTPS checkbox
            chkForceHttps.Checked = Flags.IsHttps;

            //perform UI setup
            ModeSetup();
            CheckDiffToken();
        }

        private void ModeSetup()
        {
            try
            {
                switch (Mode)
                {
                    case DirectConnectMode.LocalLink:

                        //setup with locally-hosted Plex server values
                        txtServerIP.Text = @"127.0.0.1";
                        txtServerIP.ReadOnly = true;
                        Text = @"Local Machine Connection";
                        break;

                    case DirectConnectMode.ModifiedDetails:
                        Text = @"Modified Connection";
                        break;

                    case DirectConnectMode.Normal:
                    default:
                        break;
                }
            }
            catch
            {
                //nothing
            }
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