using PlexDL.PlexAPI.LoginHandler.Auth.JSON;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

namespace PlexDL.PlexAPI.LoginHandler.UI
{
    public partial class LoginWindow : Form
    {
        public PlexAuth PlexRequestPin { get; set; } = null;

        public bool Success { get; set; }

        public PlexAuth Result { get; set; }

        public string ChangeToBrowser = "Press 'OK' once you've\nlogged in with your\nbrowser";

        public string TalkingToPlex = @"Talking to Plex.tv";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            //default instruction
            lblInstructions.Text = ChangeToBrowser;

            //cycling dots animation
            tmrDotChange.Start();

            //auto-checking cycle (checks every 1.5s)
            tmrLoginDetection.Start();

            //start browser with the login page
            LaunchBrowser();
        }

        private void DotUpdate()
        {
            var dots = pbMain.Text;
            var len = dots.Length;
            const int lim = 3;
            if (len < lim)
            {
                dots += '●';
            }
            else
            {
                dots = "";
            }
            pbMain.Text = dots;
        }

        private void TmrDotChange_Tick(object sender, EventArgs e)
        {
            DotUpdate();
        }

        private async void TmrLoginDetection_Tick(object sender, EventArgs e)
        {
            try
            {
                //Try and grab the new token.
                //~Run this on a background thread (avoids UI lockup)
                var newPin = await Task.Run(() => PlexAuthHandler.FromPinEndpoint(PlexRequestPin));

                //it's only successful if a token was actually provided
                if (newPin != null)
                    if (!string.IsNullOrEmpty(newPin.AuthToken))
                    {
                        //disable the UI button to stop the user from
                        //attempting to start two checks
                        btnOK.Enabled = false;

                        tmrLoginDetection.Stop();

                        //specify this to avoid confusion with the UI handler on the other end
                        Success = true;

                        //apply the result
                        Result = newPin;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
            }
            catch
            {
                //if any errors occurs, disable the auto-checker
                //and revert to manual user input (via the 'OK' button)
                tmrLoginDetection.Stop();
            }
        }

        private void LaunchBrowser()
        {
            //the interface the user will login to
            var endpoint = PlexRequestPin.LoginInterfaceUrl;

            //show in default browser
            Process.Start(endpoint);
        }

        private async void LoginAction()
        {
            try
            {
                //immediately stop the auto-checker to avoid double checks
                tmrLoginDetection.Stop();

                //change UI accordingly
                lblInstructions.Text = TalkingToPlex;
                btnOK.Enabled = false; //so the user can't click it twice

                //try and grab the new token
                var newPin = await Task.Run(() => PlexAuthHandler.FromPinEndpoint(PlexRequestPin));

                //it's only successful if a token was actually provided
                if (newPin != null)
                    Success = !string.IsNullOrEmpty(newPin.AuthToken);

                //apply the result
                Result = newPin;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                UIMessages.Error($"An error occurred whilst logging into Plex.tv:\n\n{ex}");
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            LoginAction();
        }

        private void LnkRelaunch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchBrowser();
        }
    }
}