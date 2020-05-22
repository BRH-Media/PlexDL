using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.PlexAPI;
using PlexDL.WaitWindow;
using System;
using System.IO;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class PlexLogin : Form
    {
        public bool Success { get; set; } = false;
        public string AccountToken { get; set; } = "";

        private string rememberMeFile { get; set; } = "plex.account";

        public PlexLogin()
        {
            InitializeComponent();
        }

        private void PlexLogin_Load(object sender, EventArgs e)
        {
            LoadRememberMe();
        }

        private bool ValidRememberMe(bool deleteOnInvalid = true)
        {
            bool valid = false;
            if (RememberMeFileExists())
            {
                var login = CachedPlexLogin.FromFile(rememberMeFile);
                //check if the MD5 checksum is still valid (small yet weak security measure; most users won't care).
                if (login.VerifyThis())
                {
                    valid = true;
                }
                else
                {
                    //delete the file if it's incorrect and the flag is set
                    if (deleteOnInvalid)
                        File.Delete(rememberMeFile);
                    //log the event
                    LoggingHelpers.RecordGenericEntry("Couldn't verify 'Remember Me' PlexLogin data: hashes didn't match.");
                }
            }
            return valid;
        }

        private bool RememberMeFileExists()
        {
            return File.Exists(rememberMeFile);
        }

        private void LoadRememberMe()
        {
            try
            {
                if (File.Exists(rememberMeFile))
                {
                    var login = CachedPlexLogin.FromFile(rememberMeFile);
                    //check if the MD5 checksum is still valid (small yet weak security measure; most users won't care).
                    if (login.VerifyThis())
                    {
                        //set the UI with values from the file
                        txtUsername.Text = login.Username;
                        txtPassword.Text = login.Password;

                        //check the "Remember Me" box
                        chkRememberMe.Checked = true;
                    }
                    else
                    {
                        //delete the file if it's incorrect
                        File.Delete(rememberMeFile);
                        //log the event
                        LoggingHelpers.RecordGenericEntry("Couldn't load 'Remember Me' PlexLogin data: hashes didn't match.");
                    }
                }
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, "RememberMeError");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLogin_Click(null, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoginWorker(object sender, WaitWindowEventArgs e)
        {
            try
            {
                var plex = new MyPlex();
                var user = plex.Authenticate(txtUsername.Text, txtPassword.Text);
                string token = user.authenticationToken;
                e.Result = token;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "PlexLoginWorkerError");
                e.Result = null;
            }
        }

        private void btnShowHidePwd_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                btnShowHidePwd.Text = "Hide";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                btnShowHidePwd.Text = "Show";
            }
        }

        private void RememberMe(bool status)
        {
            try
            {
                if (status)
                {
                    var username = txtUsername.Text;
                    var password = txtPassword.Text;
                    var login = new CachedPlexLogin(username, password);
                    login.ToFile(rememberMeFile);
                }
            }
            catch (Exception ex)
            {
                //log and ignore the error
                LoggingHelpers.RecordException(ex.Message, "RememberMeError");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    string token = (string)WaitWindow.WaitWindow.Show(LoginWorker, "Logging you in");
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        // valid account tokens are always 20 characters in length
                        if (token.Length == 20)
                        {
                            RememberMe(chkRememberMe.Checked);
                            MessageBox.Show("Successfully authenticated your Plex.tv account. You can now load servers and relays from Plex.tv", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AccountToken = token;
                            Success = true;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Received an invalid token from the server", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect username/password", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter your Plex.tv username and password correctly", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to login to Plex.tv; an error occurred.\n\n" + ex, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "PlexLoginError");
                Success = false;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}