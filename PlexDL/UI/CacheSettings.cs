using PlexDL.Common;
using System;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class CacheSettings : Form
    {
        public CacheSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            settingsGrid.SelectedObject = GlobalStaticVars.Settings;
        }
    }
}