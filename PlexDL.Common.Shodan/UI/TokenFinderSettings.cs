using PlexDL.Common.Shodan.Globals;
using PlexDL.WaitWindow;
using Shodan.Client;
using Shodan.Exceptions;
using System;
using System.Net;
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //apply the result
            DialogResult = DialogResult.Cancel;

            //close the form
            Close();
        }

        private void ApiTest(object sender, WaitWindowEventArgs e)
            => ApiTest(false);

        private void ApiTest(bool multiThreaded = true)
        {
            try
            {
                //wait window?
                if (multiThreaded)
                {
                    //show the window
                    WaitWindow.WaitWindow.Show(ApiTest, @"Testing API key");
                }
                else
                {
                    //validation
                    if (!string.IsNullOrWhiteSpace(txtApiKey.Text) && txtApiKey.Text.Length == 32)
                    {
                        //validation
                        if (!txtApiKey.Text.Contains(" ") && !txtApiKey.Text.Contains("\t"))
                        {
                            //setup Shodan client
                            var client = new ClientFactory(txtApiKey.Text).GetFullClient();

                            //execute the query!
                            var result = client.AccountProfile().GetAwaiter().GetResult();

                            //validation
                            if (result != null)
                            {
                                //alert user
                                UIMessages.Info($"API test successful\n\nYou have {result.Credits} credits remaining");
                            }
                            else
                            {
                                //alert user
                                UIMessages.Error("API test failed:\n\nInvalid key");
                            }
                        }
                        else
                        {
                            //alert user
                            UIMessages.Error("API test failed:\n\nInvalid key");
                        }
                    }
                    else
                    {
                        //alert user
                        UIMessages.Error("API test failed:\n\nInvalid key");
                    }
                }
            }
            catch (ShodanException ex)
            {
                if (ex.Response.StatusCode != HttpStatusCode.Unauthorized)
                {
                    UIMessages.Error(!string.IsNullOrWhiteSpace(ex.Error) ? ex.Error : @"Shodan API error occurred");
                }
                else
                {
                    //alert user
                    UIMessages.Error("API test failed:\n\nInvalid key");
                }
            }
            catch (Exception ex)
            {
                //alert the user
                UIMessages.Error($"API test error:\n\n{ex}");
            }
        }

        private void BtnApiTest_Click(object sender, EventArgs e)
        {
            //perform the test
            ApiTest();
        }
    }
}