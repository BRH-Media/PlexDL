using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI.Forms
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
            this.gbToken = new System.Windows.Forms.GroupBox();
            this.txtAccountToken = new libbrhscgui.Components.WaterMarkTextBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbToken.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(3, 56);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(214, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Login";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // gbToken
            // 
            this.gbToken.Controls.Add(this.txtAccountToken);
            this.gbToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbToken.Location = new System.Drawing.Point(3, 3);
            this.gbToken.Name = "gbToken";
            this.gbToken.Size = new System.Drawing.Size(214, 47);
            this.gbToken.TabIndex = 32;
            this.gbToken.TabStop = false;
            this.gbToken.Text = "Account Token";
            // 
            // txtAccountToken
            // 
            this.txtAccountToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAccountToken.Location = new System.Drawing.Point(6, 19);
            this.txtAccountToken.MaxLength = 20;
            this.txtAccountToken.Name = "txtAccountToken";
            this.txtAccountToken.Size = new System.Drawing.Size(204, 20);
            this.txtAccountToken.TabIndex = 0;
            this.txtAccountToken.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtAccountToken.WaterMarkText = "Plex.tv Token";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.gbToken, 0, 0);
            this.tlpMain.Controls.Add(this.btnConnect, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(220, 82);
            this.tlpMain.TabIndex = 33;
            // 
            // Authenticate
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(220, 82);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Authenticate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plex Authentication";
            this.Load += new System.EventHandler(this.FrmConnect_Load);
            this.gbToken.ResumeLayout(false);
            this.gbToken.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnConnect;
        private GroupBox gbToken;
        private libbrhscgui.Components.WaterMarkTextBox txtAccountToken;
        private TableLayoutPanel tlpMain;
    }
}