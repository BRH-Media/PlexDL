using System.ComponentModel;
using System.Windows.Forms;
using libbrhscgui.Components;

namespace PlexDL.UI.Forms
{
    partial class DirectConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectConnect));
            this.txtServerIP = new libbrhscgui.Components.WaterMarkTextBox();
            this.txtServerPort = new libbrhscgui.Components.WaterMarkTextBox();
            this.gbConnectionInformation = new System.Windows.Forms.GroupBox();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.chkForceHttps = new System.Windows.Forms.CheckBox();
            this.txtToken = new libbrhscgui.Components.WaterMarkTextBox();
            this.chkToken = new System.Windows.Forms.CheckBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbConnectionInformation.SuspendLayout();
            this.tlpControls.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServerIP
            // 
            this.txtServerIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtServerIP.Location = new System.Drawing.Point(3, 3);
            this.txtServerIP.MaxLength = 128;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(280, 20);
            this.txtServerIP.TabIndex = 0;
            this.txtServerIP.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerIP.WaterMarkText = "Address";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerPort.Location = new System.Drawing.Point(3, 29);
            this.txtServerPort.MaxLength = 5;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(280, 20);
            this.txtServerPort.TabIndex = 1;
            this.txtServerPort.Text = "32400";
            this.txtServerPort.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerPort.WaterMarkText = "Port";
            // 
            // gbConnectionInformation
            // 
            this.tlpMain.SetColumnSpan(this.gbConnectionInformation, 2);
            this.gbConnectionInformation.Controls.Add(this.tlpControls);
            this.gbConnectionInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConnectionInformation.Location = new System.Drawing.Point(3, 0);
            this.gbConnectionInformation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gbConnectionInformation.Name = "gbConnectionInformation";
            this.gbConnectionInformation.Size = new System.Drawing.Size(292, 117);
            this.gbConnectionInformation.TabIndex = 15;
            this.gbConnectionInformation.TabStop = false;
            this.gbConnectionInformation.Text = "Connection Information";
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 1;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.Controls.Add(this.chkForceHttps, 0, 3);
            this.tlpControls.Controls.Add(this.txtServerIP, 0, 0);
            this.tlpControls.Controls.Add(this.txtToken, 0, 4);
            this.tlpControls.Controls.Add(this.txtServerPort, 0, 1);
            this.tlpControls.Controls.Add(this.chkToken, 0, 4);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(3, 16);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 5;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpControls.Size = new System.Drawing.Size(286, 98);
            this.tlpControls.TabIndex = 4;
            // 
            // chkForceHttps
            // 
            this.chkForceHttps.AutoSize = true;
            this.chkForceHttps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkForceHttps.Location = new System.Drawing.Point(3, 55);
            this.chkForceHttps.Name = "chkForceHttps";
            this.chkForceHttps.Size = new System.Drawing.Size(280, 17);
            this.chkForceHttps.TabIndex = 4;
            this.chkForceHttps.Text = "Force HTTPS";
            this.chkForceHttps.UseVisualStyleBackColor = true;
            // 
            // txtToken
            // 
            this.txtToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtToken.Location = new System.Drawing.Point(3, 101);
            this.txtToken.MaxLength = 20;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(280, 20);
            this.txtToken.TabIndex = 3;
            this.txtToken.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtToken.WaterMarkText = "Enter Token";
            // 
            // chkToken
            // 
            this.chkToken.AutoSize = true;
            this.chkToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkToken.Location = new System.Drawing.Point(3, 78);
            this.chkToken.Name = "chkToken";
            this.chkToken.Size = new System.Drawing.Size(280, 17);
            this.chkToken.TabIndex = 2;
            this.chkToken.Text = "Use a different token";
            this.chkToken.UseVisualStyleBackColor = true;
            this.chkToken.CheckedChanged += new System.EventHandler(this.chkToken_CheckedChanged);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.btnConnect, 1, 1);
            this.tlpMain.Controls.Add(this.gbConnectionInformation, 0, 0);
            this.tlpMain.Controls.Add(this.btnCancel, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(298, 146);
            this.tlpMain.TabIndex = 18;
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(152, 120);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(143, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(3, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DirectConnect
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(298, 146);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DirectConnect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Direct Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectConnect_FormClosing);
            this.Load += new System.EventHandler(this.DirectConnect_Load);
            this.gbConnectionInformation.ResumeLayout(false);
            this.tlpControls.ResumeLayout(false);
            this.tlpControls.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private WaterMarkTextBox txtServerPort;
        private WaterMarkTextBox txtServerIP;
        private GroupBox gbConnectionInformation;
        private CheckBox chkToken;
        private WaterMarkTextBox txtToken;
        private TableLayoutPanel tlpMain;
        private Button btnConnect;
        private Button btnCancel;
        private TableLayoutPanel tlpControls;
        private CheckBox chkForceHttps;
    }
}