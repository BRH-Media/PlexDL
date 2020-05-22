using PlexDL.Common.Structures;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Authenticate : Form
    {
        public bool connectionStarted;

        public Authenticate()
        {
            InitializeComponent();
        }

        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();
        public bool Success { get; set; } = false;

        private void FrmConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
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

                connectionStarted = true;
                DialogResult = DialogResult.OK;
                Success = true;
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
            txtAccountToken.Text = ConnectionInfo.PlexAccountToken;
        }
    }
}