using System;
using System.Windows.Forms;
using PlexDL.Common.Globals;

namespace PlexDL.UI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            settingsGrid.SelectedObject = GlobalStaticVars.Settings;
        }
    }
}