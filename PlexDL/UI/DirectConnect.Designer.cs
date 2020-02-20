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
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new MetroSet_UI.Controls.MetroSetButton();
            this.styleMain = new MetroSet_UI.StyleManager();
            this.txtServerIP = new MetroSet_UI.Controls.MetroSetTextBox();
            this.txtServerPort = new MetroSet_UI.Controls.MetroSetTextBox();
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.DisabledForeColor = System.Drawing.Color.Gray;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnConnect.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnConnect.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.btnConnect.HoverTextColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(18, 144);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.NormalTextColor = System.Drawing.Color.White;
            this.btnConnect.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnConnect.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnConnect.PressTextColor = System.Drawing.Color.White;
            this.btnConnect.Size = new System.Drawing.Size(197, 36);
            this.btnConnect.Style = MetroSet_UI.Design.Style.Light;
            this.btnConnect.StyleManager = this.styleMain;
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.ThemeAuthor = null;
            this.btnConnect.ThemeName = null;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
            // 
            // txtServerIP
            // 
            this.txtServerIP.AutoCompleteCustomSource = null;
            this.txtServerIP.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtServerIP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtServerIP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtServerIP.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtServerIP.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtServerIP.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.txtServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtServerIP.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtServerIP.Image = null;
            this.txtServerIP.Lines = null;
            this.txtServerIP.Location = new System.Drawing.Point(18, 82);
            this.txtServerIP.MaxLength = 15;
            this.txtServerIP.Multiline = false;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = false;
            this.txtServerIP.Size = new System.Drawing.Size(197, 25);
            this.txtServerIP.Style = MetroSet_UI.Design.Style.Light;
            this.txtServerIP.StyleManager = this.styleMain;
            this.txtServerIP.TabIndex = 10;
            this.txtServerIP.TabStop = false;
            this.txtServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtServerIP.ThemeAuthor = null;
            this.txtServerIP.ThemeName = null;
            this.txtServerIP.UseSystemPasswordChar = false;
            this.txtServerIP.WatermarkText = "Server Address (IPv4)";
            // 
            // txtServerPort
            // 
            this.txtServerPort.AutoCompleteCustomSource = null;
            this.txtServerPort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtServerPort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtServerPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtServerPort.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtServerPort.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtServerPort.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.txtServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtServerPort.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtServerPort.Image = null;
            this.txtServerPort.Lines = null;
            this.txtServerPort.Location = new System.Drawing.Point(18, 111);
            this.txtServerPort.MaxLength = 5;
            this.txtServerPort.Multiline = false;
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.ReadOnly = false;
            this.txtServerPort.Size = new System.Drawing.Size(197, 25);
            this.txtServerPort.Style = MetroSet_UI.Design.Style.Light;
            this.txtServerPort.StyleManager = this.styleMain;
            this.txtServerPort.TabIndex = 12;
            this.txtServerPort.TabStop = false;
            this.txtServerPort.Text = "32400";
            this.txtServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtServerPort.ThemeAuthor = null;
            this.txtServerPort.ThemeName = null;
            this.txtServerPort.UseSystemPasswordChar = false;
            this.txtServerPort.WatermarkText = "Server Port (TCP)";
            // 
            // ctrlMain
            // 
            this.ctrlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlMain.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ctrlMain.CloseHoverForeColor = System.Drawing.Color.White;
            this.ctrlMain.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.DisabledForeColor = System.Drawing.Color.DimGray;
            this.ctrlMain.Location = new System.Drawing.Point(131, 2);
            this.ctrlMain.MaximizeBox = false;
            this.ctrlMain.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeBox = false;
            this.ctrlMain.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.Name = "ctrlMain";
            this.ctrlMain.Size = new System.Drawing.Size(100, 25);
            this.ctrlMain.Style = MetroSet_UI.Design.Style.Light;
            this.ctrlMain.StyleManager = this.styleMain;
            this.ctrlMain.TabIndex = 13;
            this.ctrlMain.Text = "Direct Connect";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // DirectConnect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(233, 205);
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.txtServerPort);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtServerIP);
            this.Name = "DirectConnect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Direct Connect";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.Load += new System.EventHandler(this.DirectConnect_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectConnect_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroSet_UI.Controls.MetroSetButton btnConnect;
        private MetroSet_UI.Controls.MetroSetTextBox txtServerIP;
        private MetroSet_UI.Controls.MetroSetTextBox txtServerPort;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
        private System.Windows.Forms.Timer t1;
    }
}