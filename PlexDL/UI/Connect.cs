using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Connect : MetroSetForm
    {
        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

        public ApplicationOptions settings;

        public Timer t1 = new Timer();

        public bool connectionStarted = false;

        public Connect()
        {
            InitializeComponent();
            styleMain = GlobalStaticVars.GlobalStyle;
            styleMain.MetroForm = this;
            cbxConnectionMode.SelectedIndex = 0;
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop(); //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0) //check if opacity is 0
            {
                t1.Stop(); //if it is, we stop the timer
                Close(); //and we try to close the form
            }
            else
            {
                Opacity -= 0.05;
            }
        }

        private void FrmConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settings.Generic.AnimationSpeed > 0 && !connectionStarted)
            {
                e.Cancel = true;
                t1 = new Timer
                {
                    Interval = settings.Generic.AnimationSpeed
                };
                t1.Tick += new EventHandler(FadeOut); //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                //resume the event - the program can be closed
                    e.Cancel = false;
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!VerifyText(txtAccountToken.Text))
            {
                MessageBox.Show("Please enter a valid account token. A token must be 20 characters in length with no spaces.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConnectionInfo.PlexAccountToken = txtAccountToken.Text;
                if (cbxConnectionMode.SelectedIndex == 1)
                {
                    ConnectionInfo.RelaysOnly = true;
                    ConnectionInfo.DirectOnly = false;
                }
                else if (cbxConnectionMode.SelectedIndex == 2)
                {
                    ConnectionInfo.RelaysOnly = false;
                    ConnectionInfo.DirectOnly = true;
                }
                else if (cbxConnectionMode.SelectedIndex == 0)
                {
                    ConnectionInfo.RelaysOnly = false;
                    ConnectionInfo.DirectOnly = false;
                }

                connectionStarted = true;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool VerifyText(string input)
        {
            // text is not spaces or tabs, text is not null or emprty, and text is exactly 20 characters long (correct token length)
            return !string.IsNullOrWhiteSpace(input) && !string.IsNullOrEmpty(input) && input.Length == 20;
        }

        private void FrmConnect_Load(object sender, EventArgs e)
        {
            connectionStarted = false;
            settings = Home.Settings;
            if (settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0; //first the opacity is 0

                t1.Interval = settings.Generic.AnimationSpeed; //we'll increase the opacity every 10ms
                t1.Tick += new EventHandler(FadeIn); //this calls the function that changes opacity
                t1.Start();
            }

            txtAccountToken.Text = ConnectionInfo.PlexAccountToken;
            if (ConnectionInfo.RelaysOnly)
                cbxConnectionMode.SelectedIndex = 1;
        }
    }
}