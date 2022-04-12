using PlexDL.Common.Shodan.Globals;
using System;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Common.Shodan.UI
{
    public partial class TokenFinderSettings : Form
    {
        public TokenFinderSettings()
        {
            InitializeComponent();
        }

        private void TokenFinderSettings_Load(object sender, EventArgs e)
        {
            //validation
            if (!string.IsNullOrWhiteSpace(Strings.ShodanApiKey))
            {
                //apply the key to the text box
                txtApiKey.Text = Strings.ShodanApiKey;
            }

            //apply focus
            ActiveControl = txtApiKey;

            //disable selection
            txtApiKey.SelectionLength = 0;
            txtApiKey.SelectionStart = 0;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //validation
            if (!string.IsNullOrWhiteSpace(txtApiKey.Text))
            {
                //API key validation
                if (txtApiKey.Text.Length == 32)
                {
                    //override key
                    if (!ApiKeyManager.StoreNewApiKey(txtApiKey.Text))
                    {
                        //alert user
                        UIMessages.Error("Could not save settings:\n\n- Failed to save API key");
                    }

                    //apply global
                    Strings.ShodanApiKey = txtApiKey.Text;

                    //alert user
                    UIMessages.Info(@"Successfully saved settings");

                    //apply result
                    DialogResult = DialogResult.OK;

                    //close the form
                    Close();
                }
                else
                {
                    //alert user
                    UIMessages.Error("Could not save settings:\n\n- Invalid API key");
                }
            }
            else
            {
                //alert user
                UIMessages.Error("Could not save settings:\n\n- Invalid API key");
            }
        }
    }
}