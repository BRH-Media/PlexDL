using System;
using System.Windows.Forms;

namespace GitHubUpdater.UI
{
    public partial class UpdateChannel : Form
    {
        //default is always 'Stable'
        public Enums.UpdateChannel SelectedChannel { get; set; } = Enums.UpdateChannel.Stable;

        public UpdateChannel()
        {
            InitializeComponent();
        }

        public static Enums.UpdateChannel ShowChannelSelector()
        {
            var frm = new UpdateChannel();
            frm.ShowDialog();

            //SelectedChannel is updated on RadioButton change
            return frm.SelectedChannel;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SelectedChannel = Enums.UpdateChannel.Unknown;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TlpMain_Paint(object sender, PaintEventArgs e)
        {
            //do nothing; useless method.
        }

        private void RadStableChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (radStableChannel.Checked)
                SelectedChannel = Enums.UpdateChannel.Stable;
        }

        private void RadDeveloperChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (radDeveloperChannel.Checked)
                SelectedChannel = Enums.UpdateChannel.Development;
        }
    }
}