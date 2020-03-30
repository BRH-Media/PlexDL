using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class Authenticate 
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAccountToken = new System.Windows.Forms.TextBox();
            this.gbToken = new System.Windows.Forms.GroupBox();
            this.gbToken.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConnect.Location = new System.Drawing.Point(12, 64);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(216, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Login";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtAccountToken
            // 
            this.txtAccountToken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtAccountToken.Location = new System.Drawing.Point(6, 19);
            this.txtAccountToken.MaxLength = 20;
            this.txtAccountToken.Name = "txtAccountToken";
            this.txtAccountToken.Size = new System.Drawing.Size(204, 20);
            this.txtAccountToken.TabIndex = 0;
            // 
            // gbToken
            // 
            this.gbToken.Controls.Add(this.txtAccountToken);
            this.gbToken.Location = new System.Drawing.Point(12, 12);
            this.gbToken.Name = "gbToken";
            this.gbToken.Size = new System.Drawing.Size(216, 46);
            this.gbToken.TabIndex = 32;
            this.gbToken.TabStop = false;
            this.gbToken.Text = "Account Token";
            // 
            // Authenticate
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(241, 96);
            this.Controls.Add(this.gbToken);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Authenticate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plex Authentication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConnect_FormClosing);
            this.Load += new System.EventHandler(this.FrmConnect_Load);
            this.gbToken.ResumeLayout(false);
            this.gbToken.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnConnect;
        private TextBox txtAccountToken;
        private GroupBox gbToken;
    }
}