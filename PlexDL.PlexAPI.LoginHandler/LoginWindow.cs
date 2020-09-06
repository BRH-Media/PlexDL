using PlexDL.PlexAPI.LoginHandler.Auth;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.PlexAPI.LoginHandler
{
    public partial class LoginWindow : Form
    {
        public PlexPins PlexRequestPin { get; set; } = null;

        public bool Success { get; set; }

        public PlexPins Result { get; set; }

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

        private void LaunchBrowser()
        {
            var endpoint = PlexRequestPin.LoginInterfaceUrl;
            //show in default browser
            Process.Start(endpoint);
        }

        private async void LoginAction()
        {
            try
            {
                //change UI accordingly
                lblInstructions.Text = TalkingToPlex;
                btnOK.Enabled = false; //so the user can't click it twice

                //try and grab the new token
                var newPin = await Task.Run(() => PlexRequestPin?.FromPinEndpoint()); //run this on a background thread (avoids UI lockup)

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

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            tmrDotChange.Start();
            LaunchBrowser();
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