using MetroSet_UI.Forms;
using PlexDL.Common.Structures;
using PlexDL.Common;
using PlexDL.WaitWindow;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class DirectConnect : MetroSetForm
    {
        public ConnectionInformation ConnectionInfo { get; set; } = new ConnectionInformation();

        public bool connectionStarted = false;

        public DirectConnect()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
        }

        private void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        public bool WebSiteIsAvailable(string Url)
        {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += ((Message.Length > 0) ? "\n" : "") + ex.Message;
            }

            return (Message.Length == 0);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!ValidateIPv4(txtServerIP.Text))
            {
                MessageBox.Show("Invalid IP Address", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(txtServerPort.Text, out int r))
            {
                MessageBox.Show("Port must be numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Convert.ToInt32(txtServerPort.Text) <= 65535)
                {
                    ConnectionInfo.PlexAddress = txtServerIP.Text;
                    ConnectionInfo.PlexPort = Convert.ToInt32(txtServerPort.Text);
                    string uri = "http://" + ConnectionInfo.PlexAddress + ":" + ConnectionInfo.PlexPort.ToString() + "/?X-Plex-Token=" + ConnectionInfo.PlexAccountToken;
                    bool available = (bool)WaitWindow.WaitWindow.Show(CheckWebWorker, "Checking Connection", new object[] { uri });
                    if (available)
                    {
                        connectionStarted = true;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        //MessageBox.Show(uri);
                        MessageBox.Show("Could not connect; the web server either returned an incorrect response, or the client could not establish a connection.\n\nYou can exit the dialog by the button in the top-right.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Port out of range", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckWebWorker(object sender, WaitWindowEventArgs e)
        {
            string uri = (string)e.Arguments[0];
            e.Result = WebSiteIsAvailable(uri);
        }

        private void DirectConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Home.settings.Generic.AnimationSpeed > 0) && !connectionStarted)
            {
                e.Cancel = true;
                t1 = new Timer();
                t1.Interval = Home.settings.Generic.AnimationSpeed;
                t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                {
                    //resume the event - the program can be closed
                    e.Cancel = false;
                }
            }
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        private void DirectConnect_Load(object sender, EventArgs e)
        {
            connectionStarted = false;
            if (Home.settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0;      //first the opacity is 0

                t1.Interval = Home.settings.Generic.AnimationSpeed;  //we'll increase the opacity every 10ms
                t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity
                t1.Start();
            }
        }
    }
}