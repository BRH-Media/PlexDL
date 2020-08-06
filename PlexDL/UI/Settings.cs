using PlexDL.Common;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Settings : Form
    {
        //this does not change...even when the original object does. This is a snapshot to revert
        //changes made in this form.
        private ApplicationOptions Snapshot { get; } = ObjectProvider.Settings.DeepClone();

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            settingsGrid.SelectedObject = ObjectProvider.Settings;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //revert changes by assigning the snapshot taken at the start to the main setting
            //provider.
            ObjectProvider.Settings = Snapshot;

            //exit and close with a return of 'Cancel'
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}