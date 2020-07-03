using PlexDL.PlexAPI.LoginHandler.Auth;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.PlexAPI.LoginHandler
{
    public partial class LoginWindow : Form
    {
        public PlexPins PlexRequestPin { get; set; } = null;

        public bool Success { get; set; } = false;

        public PlexPins Result { get; set; } = null;

        public string ChangeToBrowser = @"Press 'OK' once you've
logged in with your
browser";

        public string TalkingToPlex = @"Talking to Plex.tv";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void TmrDotChange_Tick(object sender, EventArgs e)
        {
            var dots = pbMain.Text;
            var len = dots.Length;
            var lim = 3;
            if (len < lim)
            {
                dots += '.';
            }
            else
            {
                dots = "";
            }
            pbMain.Text = dots;
        }

        private void LaunchBrowser()
        {
            var endp = PlexRequestPin.LoginInterfaceUrl;
            //show in default browser
            Process.Start(endp);
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            tmrDotChange.Start();
            LaunchBrowser();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //change UI accordingly
                lblInstructions.Text = TalkingToPlex;

                //try and grab the new token
                var newPin = PlexRequestPin?.FromPinEndpoint();

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
                UIMessages.Error($"An error occured whilst logging into Plex.tv:\n\n{ex}");
            }
        }

        private void LnkRelaunch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchBrowser();
        }
    }
}