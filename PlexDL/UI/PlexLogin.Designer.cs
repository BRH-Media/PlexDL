namespace PlexDL.UI
{
    partial class PlexLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlexLogin));
            this.gbDisclaimer = new System.Windows.Forms.GroupBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.gbCredentials = new System.Windows.Forms.GroupBox();
            this.btnShowHidePwd = new System.Windows.Forms.Button();
            this.txtPassword = new libbrhscgui.Components.WaterMarkTextBox();
            this.txtUsername = new libbrhscgui.Components.WaterMarkTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.gbDisclaimer.SuspendLayout();
            this.gbCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDisclaimer
            // 
            this.gbDisclaimer.Controls.Add(this.lblDisclaimer);
            this.gbDisclaimer.Location = new System.Drawing.Point(12, 12);
            this.gbDisclaimer.Name = "gbDisclaimer";
            this.gbDisclaimer.Size = new System.Drawing.Size(314, 63);
            this.gbDisclaimer.TabIndex = 0;
            this.gbDisclaimer.TabStop = false;
            this.gbDisclaimer.Text = "Disclaimer";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDisclaimer.Location = new System.Drawing.Point(3, 16);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(308, 44);
            this.lblDisclaimer.TabIndex = 0;
            this.lblDisclaimer.Text = "Your details will only be sent to Plex.tv, and are not forwarded or stored anywhe" +
    "re else. This information will not be locally cached unless you select \'Remember" +
    " Me\'.";
            // 
            // gbCredentials
            // 
            this.gbCredentials.Controls.Add(this.btnShowHidePwd);
            this.gbCredentials.Controls.Add(this.txtPassword);
            this.gbCredentials.Controls.Add(this.txtUsername);
            this.gbCredentials.Location = new System.Drawing.Point(12, 81);
            this.gbCredentials.Name = "gbCredentials";
            this.gbCredentials.Size = new System.Drawing.Size(314, 72);
            this.gbCredentials.TabIndex = 1;
            this.gbCredentials.TabStop = false;
            this.gbCredentials.Text = "Credentials";
            // 
            // btnShowHidePwd
            // 
            this.btnShowHidePwd.Location = new System.Drawing.Point(242, 45);
            this.btnShowHidePwd.Name = "btnShowHidePwd";
            this.btnShowHidePwd.Size = new System.Drawing.Size(66, 20);
            this.btnShowHidePwd.TabIndex = 2;
            this.btnShowHidePwd.Text = "Show";
            this.btnShowHidePwd.UseVisualStyleBackColor = true;
            this.btnShowHidePwd.Click += new System.EventHandler(this.btnShowHidePwd_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPassword.Location = new System.Drawing.Point(6, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(230, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtPassword.WaterMarkText = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtUsername.Location = new System.Drawing.Point(6, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(302, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtUsername.WaterMarkText = "Username";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(155, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(171, 180);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(155, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Location = new System.Drawing.Point(12, 159);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(95, 17);
            this.chkRememberMe.TabIndex = 1;
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // PlexLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 217);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbCredentials);
            this.Controls.Add(this.gbDisclaimer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlexLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login to Plex.tv";
            this.Load += new System.EventHandler(this.PlexLogin_Load);
            this.gbDisclaimer.ResumeLayout(false);
            this.gbCredentials.ResumeLayout(false);
            this.gbCredentials.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDisclaimer;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.GroupBox gbCredentials;
        private libbrhscgui.Components.WaterMarkTextBox txtUsername;
        private libbrhscgui.Components.WaterMarkTextBox txtPassword;
        private System.Windows.Forms.Button btnShowHidePwd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkRememberMe;
    }
}