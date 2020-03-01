namespace PlexDL.UI
{
    partial class Connect 
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
            this.txtAccountToken = new MetroSet_UI.Controls.MetroSetTextBox();
            this.styleMain = new MetroSet_UI.StyleManager();
            this.btnConnect = new MetroSet_UI.Controls.MetroSetButton();
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            this.cbxConnectionMode = new MetroSet_UI.Controls.MetroSetComboBox();
            this.SuspendLayout();
            // 
            // txtAccountToken
            // 
            this.txtAccountToken.AutoCompleteCustomSource = null;
            this.txtAccountToken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtAccountToken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtAccountToken.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtAccountToken.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtAccountToken.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtAccountToken.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.txtAccountToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAccountToken.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtAccountToken.Image = null;
            this.txtAccountToken.Lines = null;
            this.txtAccountToken.Location = new System.Drawing.Point(15, 85);
            this.txtAccountToken.MaxLength = 20;
            this.txtAccountToken.Multiline = false;
            this.txtAccountToken.Name = "txtAccountToken";
            this.txtAccountToken.ReadOnly = false;
            this.txtAccountToken.Size = new System.Drawing.Size(204, 26);
            this.txtAccountToken.Style = MetroSet_UI.Design.Style.Light;
            this.txtAccountToken.StyleManager = this.styleMain;
            this.txtAccountToken.TabIndex = 5;
            this.txtAccountToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAccountToken.ThemeAuthor = null;
            this.txtAccountToken.ThemeName = null;
            this.txtAccountToken.UseSystemPasswordChar = false;
            this.txtAccountToken.WatermarkText = "Account Token";
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
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
            this.btnConnect.Location = new System.Drawing.Point(15, 170);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.btnConnect.NormalTextColor = System.Drawing.Color.White;
            this.btnConnect.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnConnect.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.btnConnect.PressTextColor = System.Drawing.Color.White;
            this.btnConnect.Size = new System.Drawing.Size(204, 23);
            this.btnConnect.Style = MetroSet_UI.Design.Style.Light;
            this.btnConnect.StyleManager = this.styleMain;
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.ThemeAuthor = null;
            this.btnConnect.ThemeName = null;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            this.ctrlMain.TabIndex = 27;
            this.ctrlMain.Text = "Player";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // cbxConnectionMode
            // 
            this.cbxConnectionMode.AllowDrop = true;
            this.cbxConnectionMode.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxConnectionMode.BackColor = System.Drawing.Color.Transparent;
            this.cbxConnectionMode.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.cbxConnectionMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxConnectionMode.CausesValidation = false;
            this.cbxConnectionMode.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cbxConnectionMode.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cbxConnectionMode.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.cbxConnectionMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxConnectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConnectionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxConnectionMode.FormattingEnabled = true;
            this.cbxConnectionMode.ItemHeight = 20;
            this.cbxConnectionMode.Items.AddRange(new object[] {
            "Standard",
            "Relays Only",
            "Direct Connection"});
            this.cbxConnectionMode.Location = new System.Drawing.Point(15, 117);
            this.cbxConnectionMode.Name = "cbxConnectionMode";
            this.cbxConnectionMode.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxConnectionMode.SelectedItemForeColor = System.Drawing.Color.White;
            this.cbxConnectionMode.Size = new System.Drawing.Size(204, 26);
            this.cbxConnectionMode.Style = MetroSet_UI.Design.Style.Light;
            this.cbxConnectionMode.StyleManager = this.styleMain;
            this.cbxConnectionMode.TabIndex = 29;
            this.cbxConnectionMode.ThemeAuthor = null;
            this.cbxConnectionMode.ThemeName = null;
            // 
            // Connect
            // 
            this.AllowResize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(234, 207);
            this.Controls.Add(this.cbxConnectionMode);
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtAccountToken);
            this.DropShadowEffect = false;
            this.Name = "Connect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Plex Authentication";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConnect_FormClosing);
            this.Load += new System.EventHandler(this.frmConnect_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetTextBox txtAccountToken;
        private MetroSet_UI.Controls.MetroSetButton btnConnect;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
        private MetroSet_UI.Controls.MetroSetComboBox cbxConnectionMode;
    }
}