using MetroSet_UI.Forms;
using PlexDL.Common.Structures;
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

        public DirectConnect()
        {
            InitializeComponent();
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
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(uri);
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
    }
}