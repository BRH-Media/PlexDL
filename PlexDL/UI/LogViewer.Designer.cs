namespace PlexDL.UI
{
    partial class LogViewer
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
            MetroSet_UI.Extensions.ImageSet imageSet1 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet5 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet4 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet3 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet2 = new MetroSet_UI.Extensions.ImageSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
            this.btnRefresh = new MetroSet_UI.Controls.MetroSetEllipse();
            this.styleMain = new MetroSet_UI.StyleManager();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnSearch = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnGoToLine = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnBackup = new MetroSet_UI.Controls.MetroSetEllipse();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new MetroSet_UI.Controls.MetroSetEllipse();
            this.lstLogFiles = new MetroSet_UI.Controls.MetroSetListBox();
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BorderThickness = 0;
            this.btnRefresh.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnRefresh.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnRefresh.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnRefresh.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnRefresh.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnRefresh.HoverTextColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::PlexDL.Properties.Resources.baseline_refresh_black_18dp;
            imageSet1.Focus = global::PlexDL.Properties.Resources.baseline_refresh_black_18dp_white;
            imageSet1.Idle = global::PlexDL.Properties.Resources.baseline_refresh_black_18dp;
            this.btnRefresh.ImageSet = imageSet1;
            this.btnRefresh.ImageSize = new System.Drawing.Size(28, 28);
            this.btnRefresh.Location = new System.Drawing.Point(13, 386);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnRefresh.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnRefresh.NormalTextColor = System.Drawing.Color.Black;
            this.btnRefresh.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnRefresh.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnRefresh.PressTextColor = System.Drawing.Color.White;
            this.btnRefresh.Size = new System.Drawing.Size(44, 36);
            this.btnRefresh.Style = MetroSet_UI.Design.Style.Light;
            this.btnRefresh.StyleManager = this.styleMain;
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.ThemeAuthor = null;
            this.btnRefresh.ThemeName = null;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToOrderColumns = true;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvMain.Location = new System.Drawing.Point(161, 74);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(501, 303);
            this.dgvMain.TabIndex = 11;
            this.dgvMain.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMain_CellContentDoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.BorderThickness = 0;
            this.btnSearch.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSearch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnSearch.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSearch.HoverTextColor = System.Drawing.Color.White;
            this.btnSearch.Image = global::PlexDL.Properties.Resources.baseline_search_black_18dp;
            imageSet5.Focus = global::PlexDL.Properties.Resources.baseline_search_black_18dp_white;
            imageSet5.Idle = global::PlexDL.Properties.Resources.baseline_search_black_18dp;
            this.btnSearch.ImageSet = imageSet5;
            this.btnSearch.ImageSize = new System.Drawing.Size(28, 28);
            this.btnSearch.Location = new System.Drawing.Point(65, 386);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnSearch.NormalTextColor = System.Drawing.Color.Black;
            this.btnSearch.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSearch.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSearch.PressTextColor = System.Drawing.Color.White;
            this.btnSearch.Size = new System.Drawing.Size(44, 36);
            this.btnSearch.Style = MetroSet_UI.Design.Style.Light;
            this.btnSearch.StyleManager = this.styleMain;
            this.btnSearch.TabIndex = 12;
            this.btnSearch.ThemeAuthor = null;
            this.btnSearch.ThemeName = null;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGoToLine
            // 
            this.btnGoToLine.BorderThickness = 0;
            this.btnGoToLine.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnGoToLine.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnGoToLine.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnGoToLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnGoToLine.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnGoToLine.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnGoToLine.HoverTextColor = System.Drawing.Color.White;
            this.btnGoToLine.Image = global::PlexDL.Properties.Resources.baseline_directions_black_18dp;
            imageSet4.Focus = global::PlexDL.Properties.Resources.baseline_directions_black_18dp_white;
            imageSet4.Idle = global::PlexDL.Properties.Resources.baseline_directions_black_18dp;
            this.btnGoToLine.ImageSet = imageSet4;
            this.btnGoToLine.ImageSize = new System.Drawing.Size(28, 28);
            this.btnGoToLine.Location = new System.Drawing.Point(117, 386);
            this.btnGoToLine.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnGoToLine.Name = "btnGoToLine";
            this.btnGoToLine.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnGoToLine.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnGoToLine.NormalTextColor = System.Drawing.Color.Black;
            this.btnGoToLine.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnGoToLine.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnGoToLine.PressTextColor = System.Drawing.Color.White;
            this.btnGoToLine.Size = new System.Drawing.Size(44, 36);
            this.btnGoToLine.Style = MetroSet_UI.Design.Style.Light;
            this.btnGoToLine.StyleManager = this.styleMain;
            this.btnGoToLine.TabIndex = 13;
            this.btnGoToLine.ThemeAuthor = null;
            this.btnGoToLine.ThemeName = null;
            this.btnGoToLine.Click += new System.EventHandler(this.btnGoToLine_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BorderThickness = 0;
            this.btnBackup.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnBackup.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnBackup.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBackup.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnBackup.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnBackup.HoverTextColor = System.Drawing.Color.White;
            this.btnBackup.Image = global::PlexDL.Properties.Resources.baseline_backup_black_18dp;
            imageSet3.Focus = global::PlexDL.Properties.Resources.baseline_backup_black_18dp_white;
            imageSet3.Idle = global::PlexDL.Properties.Resources.baseline_backup_black_18dp;
            this.btnBackup.ImageSet = imageSet3;
            this.btnBackup.ImageSize = new System.Drawing.Size(28, 28);
            this.btnBackup.Location = new System.Drawing.Point(169, 386);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnBackup.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnBackup.NormalTextColor = System.Drawing.Color.Black;
            this.btnBackup.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnBackup.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnBackup.PressTextColor = System.Drawing.Color.White;
            this.btnBackup.Size = new System.Drawing.Size(44, 36);
            this.btnBackup.Style = MetroSet_UI.Design.Style.Light;
            this.btnBackup.StyleManager = this.styleMain;
            this.btnBackup.TabIndex = 14;
            this.btnBackup.ThemeAuthor = null;
            this.btnBackup.ThemeName = null;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // sfdBackup
            // 
            this.sfdBackup.DefaultExt = "zip";
            this.sfdBackup.Filter = "Zip Archives|*.zip";
            this.sfdBackup.Title = "Backup Logs";
            // 
            // t1
            // 
            this.t1.Interval = 10;
            // 
            // btnExit
            // 
            this.btnExit.BorderThickness = 0;
            this.btnExit.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnExit.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverTextColor = System.Drawing.Color.White;
            this.btnExit.Image = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            imageSet2.Focus = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp_white;
            imageSet2.Idle = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.ImageSet = imageSet2;
            this.btnExit.ImageSize = new System.Drawing.Size(28, 28);
            this.btnExit.Location = new System.Drawing.Point(611, 386);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnExit.NormalTextColor = System.Drawing.Color.Black;
            this.btnExit.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressTextColor = System.Drawing.Color.White;
            this.btnExit.Size = new System.Drawing.Size(44, 36);
            this.btnExit.Style = MetroSet_UI.Design.Style.Light;
            this.btnExit.StyleManager = this.styleMain;
            this.btnExit.TabIndex = 15;
            this.btnExit.ThemeAuthor = null;
            this.btnExit.ThemeName = null;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lstLogFiles
            // 
            this.lstLogFiles.BorderColor = System.Drawing.Color.LightGray;
            this.lstLogFiles.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lstLogFiles.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lstLogFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstLogFiles.HoveredItemBackColor = System.Drawing.Color.LightGray;
            this.lstLogFiles.HoveredItemColor = System.Drawing.Color.DimGray;
            this.lstLogFiles.ItemHeight = 20;
            this.lstLogFiles.Location = new System.Drawing.Point(12, 74);
            this.lstLogFiles.MultiSelect = false;
            this.lstLogFiles.Name = "lstLogFiles";
            this.lstLogFiles.SelectedIndex = -1;
            this.lstLogFiles.SelectedItem = null;
            this.lstLogFiles.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.lstLogFiles.SelectedItemColor = System.Drawing.Color.White;
            this.lstLogFiles.SelectedValue = null;
            this.lstLogFiles.ShowBorder = false;
            this.lstLogFiles.ShowScrollBar = false;
            this.lstLogFiles.Size = new System.Drawing.Size(142, 284);
            this.lstLogFiles.Style = MetroSet_UI.Design.Style.Light;
            this.lstLogFiles.StyleManager = this.styleMain;
            this.lstLogFiles.TabIndex = 0;
            this.lstLogFiles.ThemeAuthor = null;
            this.lstLogFiles.ThemeName = null;
            this.lstLogFiles.SelectedIndexChanged += new MetroSet_UI.Controls.MetroSetListBox.SelectedIndexChangedEventHandler(this.lstLogFiles_SelectedIndexChanged);
            // 
            // ctrlMain
            // 
            this.ctrlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlMain.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ctrlMain.CloseHoverForeColor = System.Drawing.Color.White;
            this.ctrlMain.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.DisabledForeColor = System.Drawing.Color.DimGray;
            this.ctrlMain.Location = new System.Drawing.Point(572, 2);
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
            this.ctrlMain.TabIndex = 16;
            this.ctrlMain.Text = "Log Viewer";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // LogViewer
            // 
            this.AllowResize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(674, 435);
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnGoToLine);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lstLogFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Log Viewer";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogViewer_FormClosing);
            this.Load += new System.EventHandler(this.LogViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetEllipse btnRefresh;
        public System.Windows.Forms.DataGridView dgvMain;
        private MetroSet_UI.Controls.MetroSetEllipse btnSearch;
        private MetroSet_UI.Controls.MetroSetEllipse btnGoToLine;
        private MetroSet_UI.Controls.MetroSetEllipse btnBackup;
        private System.Windows.Forms.SaveFileDialog sfdBackup;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer t1;
        private MetroSet_UI.Controls.MetroSetEllipse btnExit;
        private MetroSet_UI.Controls.MetroSetListBox lstLogFiles;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
    }
}