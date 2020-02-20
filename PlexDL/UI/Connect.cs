using PlexDL.Common;
using PlexDL.Common.Structures;
using System;
using System.Windows.Forms;
using MetroSet_UI.Forms;

namespace PlexDL.UI
{
    public partial class Connect : MetroSetForm
    {
        public ConnectionInformation ConnectionInfo { get; set; } = new ConnectionInformation();

        public AppOptions settings;

        public Timer t1 = new Timer();

        public bool connectionStarted = false;

        public Connect()
        {
            this.styleMain = Home.styleMain;
            this.styleMain.MetroForm = this;
            InitializeComponent();
        }

        private void frmConnect_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //if (!verifyText(txtPlexIP.Text))
            //{
            //MessageBox.Show("Please enter a valid IP Address/Hostname", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            // else if (!(System.Text.RegularExpressions.Regex.IsMatch(txtPlexPort.Text, @"^\d+$")))
            //{
            //MessageBox.Show("Please enter a valid TCP Port", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }
            if (!verifyText(txtAccountToken.Text))
            {
                MessageBox.Show("Please enter a valid account token. A token must be 20 characters in length with no spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //ConnectionInfo.PlexAddress = txtPlexIP.Text;
                //ConnectionInfo.PlexPort = Convert.ToInt32(txtPlexPort.Text);
                ConnectionInfo.PlexAccountToken = txtAccountToken.Text;
                ConnectionInfo.RelaysOnly = chkRelays.Checked;
                connectionStarted = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool verifyText(string input)
        {
            int len = input.Length;
            int spacecount = 0;
            foreach (char c in input)
            {
                if (c == ' ')
                {
                    spacecount++;
                }
            }

            if ((spacecount > 0) || (input == "") || (len != 20))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {
            connectionStarted = false;
            settings = Home.settings;
            //txtPlexIP.Text = ConnectionInfo.PlexAddress;
            //txtPlexPort.Text = ConnectionInfo.PlexPort.ToString();
            txtAccountToken.Text = ConnectionInfo.PlexAccountToken;
            chkRelays.Checked = ConnectionInfo.RelaysOnly;
        }

        private void materialDivider1_Click(object sender, EventArgs e)
        {
        }

        private void lblAccountToken_Click(object sender, EventArgs e)
        {
        }

        private void txtAccountToken_Click(object sender, EventArgs e)
        {
        }
    }
}