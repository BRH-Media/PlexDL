using System.ComponentModel;
using System.Windows.Forms;
using libbrhscgui.Components;

namespace PlexDL.UI
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServerIP = new libbrhscgui.Components.WaterMarkTextBox();
            this.txtServerPort = new libbrhscgui.Components.WaterMarkTextBox();
            this.gbConnectionInformation = new System.Windows.Forms.GroupBox();
            this.txtToken = new libbrhscgui.Components.WaterMarkTextBox();
            this.chkToken = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.gbConnectionInformation.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(107, 0);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(103, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtServerIP.Location = new System.Drawing.Point(6, 19);
            this.txtServerIP.MaxLength = 128;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(198, 20);
            this.txtServerIP.TabIndex = 0;
            this.txtServerIP.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerIP.WaterMarkText = "Address";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerPort.Location = new System.Drawing.Point(6, 45);
            this.txtServerPort.MaxLength = 5;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(198, 20);
            this.txtServerPort.TabIndex = 1;
            this.txtServerPort.Text = "32400";
            this.txtServerPort.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerPort.WaterMarkText = "Port";
            // 
            // gbConnectionInformation
            // 
            this.gbConnectionInformation.Controls.Add(this.txtToken);
            this.gbConnectionInformation.Controls.Add(this.chkToken);
            this.gbConnectionInformation.Controls.Add(this.txtServerIP);
            this.gbConnectionInformation.Controls.Add(this.txtServerPort);
            this.gbConnectionInformation.Location = new System.Drawing.Point(12, 12);
            this.gbConnectionInformation.Name = "gbConnectionInformation";
            this.gbConnectionInformation.Size = new System.Drawing.Size(210, 118);
            this.gbConnectionInformation.TabIndex = 15;
            this.gbConnectionInformation.TabStop = false;
            this.gbConnectionInformation.Text = "Connection Information";
            // 
            // txtToken
            // 
            this.txtToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtToken.Location = new System.Drawing.Point(6, 92);
            this.txtToken.MaxLength = 20;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(198, 20);
            this.txtToken.TabIndex = 3;
            this.txtToken.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtToken.WaterMarkText = "Enter Token";
            // 
            // chkToken
            // 
            this.chkToken.AutoSize = true;
            this.chkToken.Location = new System.Drawing.Point(6, 71);
            this.chkToken.Name = "chkToken";
            this.chkToken.Size = new System.Drawing.Size(125, 17);
            this.chkToken.TabIndex = 2;
            this.chkToken.Text = "Use a different token";
            this.chkToken.UseVisualStyleBackColor = true;
            this.chkToken.CheckedChanged += new System.EventHandler(this.chkToken_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnCancel);
            this.pnlControls.Controls.Add(this.btnConnect);
            this.pnlControls.Location = new System.Drawing.Point(12, 136);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(210, 23);
            this.pnlControls.TabIndex = 17;
            // 
            // DirectConnect
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(234, 165);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.gbConnectionInformation);
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
            this.gbConnectionInformation.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnConnect;
        private WaterMarkTextBox txtServerPort;
        private WaterMarkTextBox txtServerIP;
        private GroupBox gbConnectionInformation;
        private CheckBox chkToken;
        private WaterMarkTextBox txtToken;
        private Button btnCancel;
        private Panel pnlControls;
    }
}