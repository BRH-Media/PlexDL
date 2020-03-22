namespace PlexDL.UI
{
    partial class DirectConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectConnect));
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServerIP = new libbrhscgui.Components.WaterMarkTextBox();
            this.txtServerPort = new libbrhscgui.Components.WaterMarkTextBox();
            this.gbConnectionInformation = new System.Windows.Forms.GroupBox();
            this.gbConnectionInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 94);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(210, 23);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtServerIP.Location = new System.Drawing.Point(6, 19);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(198, 20);
            this.txtServerIP.TabIndex = 13;
            this.txtServerIP.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerIP.WaterMarkText = "Address";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerPort.Location = new System.Drawing.Point(6, 45);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(198, 20);
            this.txtServerPort.TabIndex = 14;
            this.txtServerPort.Text = "32400";
            this.txtServerPort.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtServerPort.WaterMarkText = "Port";
            // 
            // gbConnectionInformation
            // 
            this.gbConnectionInformation.Controls.Add(this.txtServerIP);
            this.gbConnectionInformation.Controls.Add(this.txtServerPort);
            this.gbConnectionInformation.Location = new System.Drawing.Point(12, 12);
            this.gbConnectionInformation.Name = "gbConnectionInformation";
            this.gbConnectionInformation.Size = new System.Drawing.Size(210, 73);
            this.gbConnectionInformation.TabIndex = 15;
            this.gbConnectionInformation.TabStop = false;
            this.gbConnectionInformation.Text = "Connection Information";
            // 
            // DirectConnect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(234, 127);
            this.Controls.Add(this.gbConnectionInformation);
            this.Controls.Add(this.btnConnect);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private libbrhscgui.Components.WaterMarkTextBox txtServerPort;
        private libbrhscgui.Components.WaterMarkTextBox txtServerIP;
        private System.Windows.Forms.GroupBox gbConnectionInformation;
    }
}