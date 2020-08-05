using PlexDL.Common.Structures;
using System;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class Authenticate : Form
    {
        public Authenticate()
        {
            InitializeComponent();
        }

        public bool ConnectionStarted { get; set; }
        public bool Success { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!ValidToken(txtAccountToken.Text))
            {
                UIMessages.Error(
                    "Please enter a valid account token. A token must be 20 characters in length with no spaces.",
                    "Validation Error");
            }
            else
            {
                ConnectionInfo.PlexAccountToken = txtAccountToken.Text;

                ConnectionStarted = true;
                DialogResult = DialogResult.OK;
                Success = true;
                Close();
            }
        }

        private static bool ValidToken(string input)
        {
            // text is not spaces or tabs, text is not null or empty, and text is exactly 20 characters long (correct token length)
            return !string.IsNullOrWhiteSpace(input) && !string.IsNullOrEmpty(input) && input.Length == 20;
        }

        private void FrmConnect_Load(object sender, EventArgs e)
        {
            ConnectionStarted = false;
            txtAccountToken.Text = ConnectionInfo.PlexAccountToken;
        }
    }
}