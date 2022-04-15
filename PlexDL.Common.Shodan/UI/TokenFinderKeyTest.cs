using System;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Common.Shodan.UI
{
    public partial class TokenFinderKeyTest : Form
    {
        public string Token { get; set; } = @"";

        public TokenFinderKeyTest()
        {
            InitializeComponent();
        }

        private void TokenFinderKeyTest_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                //alert the user
                UIMessages.Error($"API Key test loading error:\n\n{ex}");

                //exit
                Close();
            }
        }
    }
}