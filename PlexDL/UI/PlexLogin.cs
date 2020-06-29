using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Security;
using PlexDL.Common.Structures;
using PlexDL.PlexAPI;
using PlexDL.WaitWindow;
using System;
using System.IO;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class PlexLogin : Form
    {
        public bool Success { get; set; }
        public string AccountToken { get; set; } = "";
        private string RememberMeFile { get; } = ".plex_account";
        private string RememberMePath { get; }

        public PlexLogin()
        {
            InitializeComponent();

            //assemble RememberMePath (you can't initialise a property with another property)
            RememberMePath = $"{GlobalStaticVars.PlexDlAppData}\\{RememberMeFile}";
        }

        private void PlexLogin_Load(object sender, EventArgs e)
        {
            LoadRememberMe();
        }

        private bool RememberMeFileExists()
        {
            var barePath = GlobalStaticVars.PlexDlAppData;
            if (!System.IO.Directory.Exists(barePath))
                System.IO.Directory.CreateDirectory(barePath);
            return File.Exists(RememberMePath);
        }

        private void LoadRememberMe()
        {
            try
            {
                if (!RememberMeFileExists()) return;

                var login = CachedPlexLogin.FromFile(RememberMePath);
                //check if the MD5 checksum is still valid (small yet weak security measure; most users won't care).
                if (!login.VerifyThis()) return;

                //setup decryption processing
                var user = new ProtectedString(login.Username, StringProtectionMode.Decrypt);
                var pass = new ProtectedString(login.Password, StringProtectionMode.Decrypt);

                //set the UI with decrypted values from the file
                txtUsername.Text = user.ProcessedValue;
                txtPassword.Text = pass.ProcessedValue;

                //check the "Remember Me" box
                chkRememberMe.Checked = true;
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, "RememberMeError");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Enter) return base.ProcessCmdKey(ref msg, keyData);
            BtnLogin_Click(null, null);
            return true;
        }

        private void LoginWorker(object sender, WaitWindowEventArgs e)
        {
            try
            {
                var plex = new MyPlex();
                var user = plex.Authenticate(txtUsername.Text, txtPassword.Text);
                var token = user.authenticationToken;
                e.Result = token;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "PlexLoginWorkerError");
                e.Result = null;
            }
        }

        private void BtnShowHidePwd_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                btnShowHidePwd.Text = @"Hide";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                btnShowHidePwd.Text = @"Show";
            }
        }

        private void RememberMe(bool status)
        {
            try
            {
                if (status)
                {
                    var protectedUsername = new ProtectedString(txtUsername.Text, StringProtectionMode.Encrypt);
                    var protectedPassword = new ProtectedString(txtPassword.Text, StringProtectionMode.Encrypt);
                    var username = protectedUsername.ProcessedValue;
                    var password = protectedPassword.ProcessedValue;
                    var login = new CachedPlexLogin(username, password);
                    login.ToFile(RememberMePath);
                }
                else
                {
                    if (RememberMeFileExists())
                        File.Delete(RememberMePath);
                }
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, "RememberMeError");
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    var token = (string)WaitWindow.WaitWindow.Show(LoginWorker, "Logging you in");

                    if (token != null)
                    {
                        if (token != string.Empty)
                        {
                            // valid account tokens are always 20 characters in length
                            if (token.Length == 20)
                            {
                                RememberMe(chkRememberMe.Checked);

                                UIMessages.Info(
                                    @"Successfully authenticated your Plex.tv account. You can now load servers and relays from Plex.tv",
                                    @"Success");
                                AccountToken = token;
                                Success = true;
                                DialogResult = DialogResult.OK;
                                Close();
                            }
                            else
                            {
                                UIMessages.Error(@"Received an invalid token from the Plex.tv authorisation server",
                                    @"Authentication Error");
                            }
                        }
                        else
                            UIMessages.Error(@"Incorrect username/password",
                                @"Authentication Error");
                    }
                    else
                        UIMessages.Error(@"Server gave a null reply. Maybe an API error occurred?",
                                @"Authentication Error");
                }
                else
                {
                    UIMessages.Error(@"Please enter your Plex.tv username and password correctly",
                        @"Validation Error");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error("Failed to login to Plex.tv; an error occurred.\n\n" + ex,
                    @"Connection Error");
                LoggingHelpers.RecordException(ex.Message, "PlexLoginError");
                Success = false;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}