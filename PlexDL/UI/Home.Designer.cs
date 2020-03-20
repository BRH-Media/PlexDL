using System;

namespace PlexDL.UI
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wkrUpdateContentView = new System.ComponentModel.BackgroundWorker();
            this.sfdSaveProfile = new System.Windows.Forms.SaveFileDialog();
            this.fbdSave = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdLoadProfile = new System.Windows.Forms.OpenFileDialog();
            this.wkrDownloadAsync = new System.ComponentModel.BackgroundWorker();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.lstLog = new MetroSet_UI.Controls.MetroSetListBox();
            this.wkrGetMetadata = new libbrhscgui.Components.AbortableBackgroundWorker();
            this.cxtEpisodes = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.itmDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDownloadAllEpisodes = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtEpisodeOptions = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.itmEpisodeMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisSeason = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtLibrarySections = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.itmManuallyLoadSection = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtContentOptions = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.itmContentMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdMetadata = new System.Windows.Forms.OpenFileDialog();
            this.nfyMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cxtStreamOptions = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.itmStreamInPVS = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStreamInVLC = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStreamInBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbStreamControl = new System.Windows.Forms.GroupBox();
            this.tlpContentOptionsControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnHTTPPlay = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMovies = new System.Windows.Forms.TabPage();
            this.tabTV = new System.Windows.Forms.TabPage();
            this.tlpTV = new System.Windows.Forms.TableLayoutPanel();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpContentOptions = new System.Windows.Forms.TableLayoutPanel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExportObj = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSetDlDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.itmProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLoadProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSaveProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServers = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServerManager = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContent = new System.Windows.Forms.ToolStripMenuItem();
            this.itmMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStartSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSearchStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCaching = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCacheMetrics = new System.Windows.Forms.ToolStripMenuItem();
            this.itmClearCache = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCacheOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvContent = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvTVShows = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvSeasons = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvEpisodes = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvLibrary = new PlexDL.Common.Components.FlatDataGridView();
            this.cxtEpisodes.SuspendLayout();
            this.cxtEpisodeOptions.SuspendLayout();
            this.cxtLibrarySections.SuspendLayout();
            this.cxtContentOptions.SuspendLayout();
            this.cxtStreamOptions.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.gbStreamControl.SuspendLayout();
            this.tlpContentOptionsControls.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabMovies.SuspendLayout();
            this.tabTV.SuspendLayout();
            this.tlpTV.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpContentOptions.SuspendLayout();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVShows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).BeginInit();
            this.SuspendLayout();
            // 
            // sfdSaveProfile
            // 
            this.sfdSaveProfile.DefaultExt = "prof";
            this.sfdSaveProfile.Filter = "XML Profile|*.prof";
            this.sfdSaveProfile.Title = "Save XML Profile";
            // 
            // fbdSave
            // 
            this.fbdSave.Description = "Select a path to save downloaded items";
            // 
            // ofdLoadProfile
            // 
            this.ofdLoadProfile.Filter = "XML Profile|*.prof";
            this.ofdLoadProfile.Title = "Load XML Profile";
            // 
            // lstLog
            // 
            this.lstLog.BorderColor = System.Drawing.Color.LightGray;
            this.lstLog.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lstLog.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstLog.HoveredItemBackColor = System.Drawing.Color.LightGray;
            this.lstLog.HoveredItemColor = System.Drawing.Color.DimGray;
            this.lstLog.ItemHeight = 20;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Margin = new System.Windows.Forms.Padding(2);
            this.lstLog.MultiSelect = false;
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectedIndex = -1;
            this.lstLog.SelectedItem = null;
            this.lstLog.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.lstLog.SelectedItemColor = System.Drawing.Color.White;
            this.lstLog.SelectedValue = null;
            this.lstLog.ShowBorder = true;
            this.lstLog.ShowScrollBar = true;
            this.lstLog.Size = new System.Drawing.Size(660, 355);
            this.lstLog.Style = MetroSet_UI.Design.Style.Light;
            this.lstLog.StyleManager = null;
            this.lstLog.TabIndex = 13;
            this.lstLog.ThemeAuthor = null;
            this.lstLog.ThemeName = null;
            this.tipMain.SetToolTip(this.lstLog, "PlexDL Log");
            // 
            // wkrGetMetadata
            // 
            this.wkrGetMetadata.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WkrGetMetadata_DoWork);
            // 
            // cxtEpisodes
            // 
            this.cxtEpisodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtEpisodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDownloadThisEpisode,
            this.itmDownloadAllEpisodes});
            this.cxtEpisodes.Name = "cxtEpisodes";
            this.cxtEpisodes.Size = new System.Drawing.Size(197, 48);
            this.cxtEpisodes.Style = MetroSet_UI.Design.Style.Light;
            this.cxtEpisodes.StyleManager = null;
            this.cxtEpisodes.ThemeAuthor = "Narwin";
            this.cxtEpisodes.ThemeName = "MetroLite";
            // 
            // itmDownloadThisEpisode
            // 
            this.itmDownloadThisEpisode.Name = "itmDownloadThisEpisode";
            this.itmDownloadThisEpisode.Size = new System.Drawing.Size(196, 22);
            this.itmDownloadThisEpisode.Text = "Download This Episode";
            this.itmDownloadThisEpisode.Click += new System.EventHandler(this.ItmDownloadThisEpisode_Click);
            // 
            // itmDownloadAllEpisodes
            // 
            this.itmDownloadAllEpisodes.Name = "itmDownloadAllEpisodes";
            this.itmDownloadAllEpisodes.Size = new System.Drawing.Size(196, 22);
            this.itmDownloadAllEpisodes.Text = "Download This Season";
            this.itmDownloadAllEpisodes.Click += new System.EventHandler(this.ItmDownloadAllEpisodes_Click);
            // 
            // cxtEpisodeOptions
            // 
            this.cxtEpisodeOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtEpisodeOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmEpisodeMetadata,
            this.itmEpisodeDownload});
            this.cxtEpisodeOptions.Name = "cxtEpisodeOptions";
            this.cxtEpisodeOptions.Size = new System.Drawing.Size(129, 48);
            this.cxtEpisodeOptions.Style = MetroSet_UI.Design.Style.Light;
            this.cxtEpisodeOptions.StyleManager = null;
            this.cxtEpisodeOptions.ThemeAuthor = "Narwin";
            this.cxtEpisodeOptions.ThemeName = "MetroLite";
            this.cxtEpisodeOptions.Opening += new System.ComponentModel.CancelEventHandler(this.CxtEpisodeOptions_Opening);
            // 
            // itmEpisodeMetadata
            // 
            this.itmEpisodeMetadata.Name = "itmEpisodeMetadata";
            this.itmEpisodeMetadata.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeMetadata.Text = "Metadata";
            this.itmEpisodeMetadata.Click += new System.EventHandler(this.ItmEpisodeMetadata_Click);
            // 
            // itmEpisodeDownload
            // 
            this.itmEpisodeDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDGVDownloadThisEpisode,
            this.itmDGVDownloadThisSeason});
            this.itmEpisodeDownload.Name = "itmEpisodeDownload";
            this.itmEpisodeDownload.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeDownload.Text = "Download";
            // 
            // itmDGVDownloadThisEpisode
            // 
            this.itmDGVDownloadThisEpisode.Name = "itmDGVDownloadThisEpisode";
            this.itmDGVDownloadThisEpisode.Size = new System.Drawing.Size(139, 22);
            this.itmDGVDownloadThisEpisode.Text = "This Episode";
            this.itmDGVDownloadThisEpisode.Click += new System.EventHandler(this.ItmDGVDownloadThisEpisode_Click);
            // 
            // itmDGVDownloadThisSeason
            // 
            this.itmDGVDownloadThisSeason.Name = "itmDGVDownloadThisSeason";
            this.itmDGVDownloadThisSeason.Size = new System.Drawing.Size(139, 22);
            this.itmDGVDownloadThisSeason.Text = "This Season";
            this.itmDGVDownloadThisSeason.Click += new System.EventHandler(this.ItmDGVDownloadThisSeason_Click);
            // 
            // cxtLibrarySections
            // 
            this.cxtLibrarySections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtLibrarySections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmManuallyLoadSection});
            this.cxtLibrarySections.Name = "cxtLibrarySections";
            this.cxtLibrarySections.Size = new System.Drawing.Size(195, 26);
            this.cxtLibrarySections.Style = MetroSet_UI.Design.Style.Light;
            this.cxtLibrarySections.StyleManager = null;
            this.cxtLibrarySections.ThemeAuthor = "Narwin";
            this.cxtLibrarySections.ThemeName = "MetroLite";
            this.cxtLibrarySections.Opening += new System.ComponentModel.CancelEventHandler(this.CxtLibrarySections_Opening);
            // 
            // itmManuallyLoadSection
            // 
            this.itmManuallyLoadSection.Name = "itmManuallyLoadSection";
            this.itmManuallyLoadSection.Size = new System.Drawing.Size(194, 22);
            this.itmManuallyLoadSection.Text = "Manually Load Section";
            this.itmManuallyLoadSection.Click += new System.EventHandler(this.ItmManuallyLoadSection_Click);
            // 
            // cxtContentOptions
            // 
            this.cxtContentOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtContentOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmContentMetadata,
            this.itmContentDownload});
            this.cxtContentOptions.Name = "cxtEpisodeOptions";
            this.cxtContentOptions.Size = new System.Drawing.Size(129, 48);
            this.cxtContentOptions.Style = MetroSet_UI.Design.Style.Light;
            this.cxtContentOptions.StyleManager = null;
            this.cxtContentOptions.ThemeAuthor = "Narwin";
            this.cxtContentOptions.ThemeName = "MetroLite";
            this.cxtContentOptions.Opening += new System.ComponentModel.CancelEventHandler(this.CxtContentOptions_Opening);
            // 
            // itmContentMetadata
            // 
            this.itmContentMetadata.Name = "itmContentMetadata";
            this.itmContentMetadata.Size = new System.Drawing.Size(128, 22);
            this.itmContentMetadata.Text = "Metadata";
            this.itmContentMetadata.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // itmContentDownload
            // 
            this.itmContentDownload.Name = "itmContentDownload";
            this.itmContentDownload.Size = new System.Drawing.Size(128, 22);
            this.itmContentDownload.Text = "Download";
            this.itmContentDownload.Click += new System.EventHandler(this.ItmContentDownload_Click);
            // 
            // ofdMetadata
            // 
            this.ofdMetadata.Filter = "PlexMovie XML|*.pmxml";
            this.ofdMetadata.Title = "Load Metadata File";
            // 
            // nfyMain
            // 
            this.nfyMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.nfyMain.Icon = ((System.Drawing.Icon)(resources.GetObject("nfyMain.Icon")));
            this.nfyMain.Text = "PlexDL";
            this.nfyMain.Visible = true;
            this.nfyMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NfyMain_MouseDoubleClick);
            // 
            // cxtStreamOptions
            // 
            this.cxtStreamOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmStreamInPVS,
            this.itmStreamInVLC,
            this.itmStreamInBrowser});
            this.cxtStreamOptions.Name = "cxtStreamOptions";
            this.cxtStreamOptions.Size = new System.Drawing.Size(170, 70);
            this.cxtStreamOptions.Style = MetroSet_UI.Design.Style.Light;
            this.cxtStreamOptions.StyleManager = null;
            this.cxtStreamOptions.ThemeAuthor = "Narwin";
            this.cxtStreamOptions.ThemeName = "MetroLite";
            // 
            // itmStreamInPVS
            // 
            this.itmStreamInPVS.Name = "itmStreamInPVS";
            this.itmStreamInPVS.Size = new System.Drawing.Size(169, 22);
            this.itmStreamInPVS.Text = "Stream in PVS";
            this.itmStreamInPVS.Click += new System.EventHandler(this.ItmStreamInPVS_Click);
            // 
            // itmStreamInVLC
            // 
            this.itmStreamInVLC.Name = "itmStreamInVLC";
            this.itmStreamInVLC.Size = new System.Drawing.Size(169, 22);
            this.itmStreamInVLC.Text = "Stream in VLC";
            this.itmStreamInVLC.Click += new System.EventHandler(this.ItmStreamInVLC_Click);
            // 
            // itmStreamInBrowser
            // 
            this.itmStreamInBrowser.Name = "itmStreamInBrowser";
            this.itmStreamInBrowser.Size = new System.Drawing.Size(169, 22);
            this.itmStreamInBrowser.Text = "Stream in Browser";
            this.itmStreamInBrowser.Click += new System.EventHandler(this.ItmStreamInBrowser_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "pmxml";
            this.sfdExport.Filter = "PlexMovie XML|*.pmxml";
            this.sfdExport.Title = "Export PlexMovie Metadata";
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbMain,
            this.lblProgress});
            this.statusMain.Location = new System.Drawing.Point(0, 411);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(956, 22);
            this.statusMain.TabIndex = 28;
            this.statusMain.Text = "Status";
            // 
            // pbMain
            // 
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(250, 16);
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(26, 17);
            this.lblProgress.Text = "Idle";
            // 
            // gbStreamControl
            // 
            this.gbStreamControl.Controls.Add(this.tlpContentOptionsControls);
            this.gbStreamControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStreamControl.Location = new System.Drawing.Point(3, 3);
            this.gbStreamControl.Name = "gbStreamControl";
            this.gbStreamControl.Size = new System.Drawing.Size(270, 57);
            this.gbStreamControl.TabIndex = 33;
            this.gbStreamControl.TabStop = false;
            this.gbStreamControl.Text = "Content Options";
            // 
            // tlpContentOptionsControls
            // 
            this.tlpContentOptionsControls.ColumnCount = 3;
            this.tlpContentOptionsControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpContentOptionsControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpContentOptionsControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpContentOptionsControls.Controls.Add(this.btnPause, 1, 0);
            this.tlpContentOptionsControls.Controls.Add(this.btnHTTPPlay, 2, 0);
            this.tlpContentOptionsControls.Controls.Add(this.btnDownload, 0, 0);
            this.tlpContentOptionsControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContentOptionsControls.Location = new System.Drawing.Point(3, 16);
            this.tlpContentOptionsControls.Name = "tlpContentOptionsControls";
            this.tlpContentOptionsControls.RowCount = 1;
            this.tlpContentOptionsControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContentOptionsControls.Size = new System.Drawing.Size(264, 38);
            this.tlpContentOptionsControls.TabIndex = 0;
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(91, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(82, 32);
            this.btnPause.TabIndex = 31;
            this.btnPause.Text = "Resume";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnHTTPPlay
            // 
            this.btnHTTPPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHTTPPlay.Location = new System.Drawing.Point(179, 3);
            this.btnHTTPPlay.Name = "btnHTTPPlay";
            this.btnHTTPPlay.Size = new System.Drawing.Size(82, 32);
            this.btnHTTPPlay.TabIndex = 32;
            this.btnHTTPPlay.Text = "Stream";
            this.btnHTTPPlay.UseVisualStyleBackColor = true;
            this.btnHTTPPlay.Click += new System.EventHandler(this.btnHTTPPlay_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownload.Location = new System.Drawing.Point(3, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(82, 32);
            this.btnDownload.TabIndex = 30;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabMovies);
            this.tabMain.Controls.Add(this.tabTV);
            this.tabMain.Controls.Add(this.tabLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(285, 3);
            this.tabMain.Name = "tabMain";
            this.tlpMain.SetRowSpan(this.tabMain, 4);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(668, 381);
            this.tabMain.TabIndex = 26;
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.dgvContent);
            this.tabMovies.Location = new System.Drawing.Point(4, 22);
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovies.Size = new System.Drawing.Size(660, 355);
            this.tabMovies.TabIndex = 0;
            this.tabMovies.Text = "Movies";
            this.tabMovies.UseVisualStyleBackColor = true;
            // 
            // tabTV
            // 
            this.tabTV.Controls.Add(this.tlpTV);
            this.tabTV.Location = new System.Drawing.Point(4, 22);
            this.tabTV.Name = "tabTV";
            this.tabTV.Padding = new System.Windows.Forms.Padding(3);
            this.tabTV.Size = new System.Drawing.Size(660, 355);
            this.tabTV.TabIndex = 1;
            this.tabTV.Text = "TV";
            this.tabTV.UseVisualStyleBackColor = true;
            // 
            // tlpTV
            // 
            this.tlpTV.BackColor = System.Drawing.Color.White;
            this.tlpTV.ColumnCount = 2;
            this.tlpTV.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTV.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTV.Controls.Add(this.dgvTVShows, 0, 0);
            this.tlpTV.Controls.Add(this.dgvSeasons, 1, 0);
            this.tlpTV.Controls.Add(this.dgvEpisodes, 1, 1);
            this.tlpTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTV.Location = new System.Drawing.Point(3, 3);
            this.tlpTV.Margin = new System.Windows.Forms.Padding(2);
            this.tlpTV.Name = "tlpTV";
            this.tlpTV.RowCount = 2;
            this.tlpTV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTV.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTV.Size = new System.Drawing.Size(654, 349);
            this.tlpTV.TabIndex = 0;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.lstLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(660, 355);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.60251F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.39749F));
            this.tlpMain.Controls.Add(this.tlpContentOptions, 0, 0);
            this.tlpMain.Controls.Add(this.tabMain, 1, 0);
            this.tlpMain.Controls.Add(this.dgvLibrary, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.Size = new System.Drawing.Size(956, 387);
            this.tlpMain.TabIndex = 29;
            this.tlpMain.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpMain_Paint);
            // 
            // tlpContentOptions
            // 
            this.tlpContentOptions.ColumnCount = 1;
            this.tlpContentOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContentOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpContentOptions.Controls.Add(this.gbStreamControl, 0, 0);
            this.tlpContentOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContentOptions.Location = new System.Drawing.Point(3, 3);
            this.tlpContentOptions.Name = "tlpContentOptions";
            this.tlpContentOptions.RowCount = 2;
            this.tlpContentOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpContentOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpContentOptions.Size = new System.Drawing.Size(276, 90);
            this.tlpContentOptions.TabIndex = 31;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmProfile,
            this.itmServers,
            this.itmContent,
            this.itmLogging,
            this.itmCaching,
            this.itmAbout});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(956, 24);
            this.menuMain.TabIndex = 30;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmExportObj,
            this.itmSetDlDirectory});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmExportObj
            // 
            this.itmExportObj.Name = "itmExportObj";
            this.itmExportObj.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itmExportObj.Size = new System.Drawing.Size(198, 22);
            this.itmExportObj.Text = "Export PMXML";
            this.itmExportObj.Click += new System.EventHandler(this.itmExportObj_Click);
            // 
            // itmSetDlDirectory
            // 
            this.itmSetDlDirectory.Name = "itmSetDlDirectory";
            this.itmSetDlDirectory.Size = new System.Drawing.Size(198, 22);
            this.itmSetDlDirectory.Text = "Set Download Directory";
            this.itmSetDlDirectory.Click += new System.EventHandler(this.itmSetDlDirectory_Click);
            // 
            // itmProfile
            // 
            this.itmProfile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmLoadProfile,
            this.itmSaveProfile});
            this.itmProfile.Name = "itmProfile";
            this.itmProfile.Size = new System.Drawing.Size(53, 20);
            this.itmProfile.Text = "Profile";
            // 
            // itmLoadProfile
            // 
            this.itmLoadProfile.Name = "itmLoadProfile";
            this.itmLoadProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmLoadProfile.Size = new System.Drawing.Size(143, 22);
            this.itmLoadProfile.Text = "Load";
            this.itmLoadProfile.Click += new System.EventHandler(this.itmLoadProfile_Click);
            // 
            // itmSaveProfile
            // 
            this.itmSaveProfile.Name = "itmSaveProfile";
            this.itmSaveProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmSaveProfile.Size = new System.Drawing.Size(143, 22);
            this.itmSaveProfile.Text = "Save";
            this.itmSaveProfile.Click += new System.EventHandler(this.itmSaveProfile_Click);
            // 
            // itmServers
            // 
            this.itmServers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmServerManager});
            this.itmServers.Name = "itmServers";
            this.itmServers.Size = new System.Drawing.Size(56, 20);
            this.itmServers.Text = "Servers";
            // 
            // itmServerManager
            // 
            this.itmServerManager.Name = "itmServerManager";
            this.itmServerManager.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.itmServerManager.Size = new System.Drawing.Size(198, 22);
            this.itmServerManager.Text = "Server Manager";
            this.itmServerManager.Click += new System.EventHandler(this.itmServerManager_Click);
            // 
            // itmContent
            // 
            this.itmContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmMetadata,
            this.itmStartSearch,
            this.itmSearchStatus});
            this.itmContent.Name = "itmContent";
            this.itmContent.Size = new System.Drawing.Size(62, 20);
            this.itmContent.Text = "Content";
            // 
            // itmMetadata
            // 
            this.itmMetadata.Name = "itmMetadata";
            this.itmMetadata.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.itmMetadata.Size = new System.Drawing.Size(176, 22);
            this.itmMetadata.Text = "Metadata";
            this.itmMetadata.Click += new System.EventHandler(this.itmMetadata_Click);
            // 
            // itmStartSearch
            // 
            this.itmStartSearch.Name = "itmStartSearch";
            this.itmStartSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itmStartSearch.Size = new System.Drawing.Size(176, 22);
            this.itmStartSearch.Text = "Start Search";
            this.itmStartSearch.Click += new System.EventHandler(this.itmStartSearch_Click);
            // 
            // itmSearchStatus
            // 
            this.itmSearchStatus.Enabled = false;
            this.itmSearchStatus.Name = "itmSearchStatus";
            this.itmSearchStatus.Size = new System.Drawing.Size(176, 22);
            this.itmSearchStatus.Text = "Not Filtered";
            // 
            // itmLogging
            // 
            this.itmLogging.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmLogViewer});
            this.itmLogging.Name = "itmLogging";
            this.itmLogging.Size = new System.Drawing.Size(63, 20);
            this.itmLogging.Text = "Logging";
            // 
            // itmLogViewer
            // 
            this.itmLogViewer.Name = "itmLogViewer";
            this.itmLogViewer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.itmLogViewer.Size = new System.Drawing.Size(172, 22);
            this.itmLogViewer.Text = "Log Viewer";
            this.itmLogViewer.Click += new System.EventHandler(this.itmLogViewer_Click);
            // 
            // itmCaching
            // 
            this.itmCaching.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCacheMetrics,
            this.itmClearCache,
            this.itmCacheOptions});
            this.itmCaching.Name = "itmCaching";
            this.itmCaching.Size = new System.Drawing.Size(63, 20);
            this.itmCaching.Text = "Caching";
            // 
            // itmCacheMetrics
            // 
            this.itmCacheMetrics.Name = "itmCacheMetrics";
            this.itmCacheMetrics.Size = new System.Drawing.Size(180, 22);
            this.itmCacheMetrics.Text = "Metrics";
            // 
            // itmClearCache
            // 
            this.itmClearCache.Name = "itmClearCache";
            this.itmClearCache.Size = new System.Drawing.Size(180, 22);
            this.itmClearCache.Text = "Clear Cache";
            // 
            // itmCacheOptions
            // 
            this.itmCacheOptions.Name = "itmCacheOptions";
            this.itmCacheOptions.Size = new System.Drawing.Size(180, 22);
            this.itmCacheOptions.Text = "Options";
            this.itmCacheOptions.Click += new System.EventHandler(this.itmCacheOptions_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new System.Drawing.Size(52, 20);
            this.itmAbout.Text = "About";
            // 
            // dgvContent
            // 
            this.dgvContent.AllowUserToAddRows = false;
            this.dgvContent.AllowUserToDeleteRows = false;
            this.dgvContent.AllowUserToOrderColumns = true;
            this.dgvContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvContent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvContent.ContextMenuStrip = this.cxtContentOptions;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvContent.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvContent.Location = new System.Drawing.Point(3, 3);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.RowHeadersVisible = false;
            this.dgvContent.RowsEmptyText = "No Movies Found";
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContent.Size = new System.Drawing.Size(654, 349);
            this.dgvContent.TabIndex = 18;
            this.dgvContent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvContent_CellContentDoubleClick);
            this.dgvContent.SelectionChanged += new System.EventHandler(this.DgvContent_OnRowChange);
            // 
            // dgvTVShows
            // 
            this.dgvTVShows.AllowUserToAddRows = false;
            this.dgvTVShows.AllowUserToDeleteRows = false;
            this.dgvTVShows.AllowUserToOrderColumns = true;
            this.dgvTVShows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTVShows.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTVShows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTVShows.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTVShows.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTVShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTVShows.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTVShows.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvTVShows.Location = new System.Drawing.Point(2, 2);
            this.dgvTVShows.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTVShows.MultiSelect = false;
            this.dgvTVShows.Name = "dgvTVShows";
            this.dgvTVShows.RowHeadersVisible = false;
            this.dgvTVShows.RowsEmptyText = "No TV Shows Found";
            this.tlpTV.SetRowSpan(this.dgvTVShows, 2);
            this.dgvTVShows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTVShows.Size = new System.Drawing.Size(323, 345);
            this.dgvTVShows.TabIndex = 27;
            this.dgvTVShows.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvTVShows_CellContentDoubleClick);
            this.dgvTVShows.SelectionChanged += new System.EventHandler(this.DgvTVShows_OnRowChange);
            // 
            // dgvSeasons
            // 
            this.dgvSeasons.AllowUserToAddRows = false;
            this.dgvSeasons.AllowUserToDeleteRows = false;
            this.dgvSeasons.AllowUserToOrderColumns = true;
            this.dgvSeasons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSeasons.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSeasons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSeasons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSeasons.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSeasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSeasons.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSeasons.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvSeasons.Location = new System.Drawing.Point(329, 2);
            this.dgvSeasons.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSeasons.MultiSelect = false;
            this.dgvSeasons.Name = "dgvSeasons";
            this.dgvSeasons.RowHeadersVisible = false;
            this.dgvSeasons.RowsEmptyText = "No TV Seasons Found";
            this.dgvSeasons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSeasons.Size = new System.Drawing.Size(323, 170);
            this.dgvSeasons.TabIndex = 20;
            this.dgvSeasons.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSeasons_CellContentDoubleClick);
            this.dgvSeasons.SelectionChanged += new System.EventHandler(this.DgvSeasons_OnRowChange);
            // 
            // dgvEpisodes
            // 
            this.dgvEpisodes.AllowUserToAddRows = false;
            this.dgvEpisodes.AllowUserToDeleteRows = false;
            this.dgvEpisodes.AllowUserToOrderColumns = true;
            this.dgvEpisodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEpisodes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEpisodes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvEpisodes.ContextMenuStrip = this.cxtEpisodeOptions;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEpisodes.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEpisodes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEpisodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvEpisodes.Location = new System.Drawing.Point(329, 176);
            this.dgvEpisodes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEpisodes.MultiSelect = false;
            this.dgvEpisodes.Name = "dgvEpisodes";
            this.dgvEpisodes.RowHeadersVisible = false;
            this.dgvEpisodes.RowsEmptyText = "No TV Episodes Found";
            this.dgvEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEpisodes.Size = new System.Drawing.Size(323, 171);
            this.dgvEpisodes.TabIndex = 21;
            this.dgvEpisodes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvEpisodes_CellContentDoubleClick);
            // 
            // dgvLibrary
            // 
            this.dgvLibrary.AllowUserToAddRows = false;
            this.dgvLibrary.AllowUserToDeleteRows = false;
            this.dgvLibrary.AllowUserToOrderColumns = true;
            this.dgvLibrary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLibrary.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvLibrary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLibrary.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLibrary.ContextMenuStrip = this.cxtLibrarySections;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLibrary.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLibrary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLibrary.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvLibrary.Location = new System.Drawing.Point(2, 98);
            this.dgvLibrary.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLibrary.MultiSelect = false;
            this.dgvLibrary.Name = "dgvLibrary";
            this.dgvLibrary.RowHeadersVisible = false;
            this.dgvLibrary.RowsEmptyText = "No Library Sections Found";
            this.tlpMain.SetRowSpan(this.dgvLibrary, 3);
            this.dgvLibrary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLibrary.Size = new System.Drawing.Size(278, 287);
            this.dgvLibrary.TabIndex = 16;
            this.dgvLibrary.SelectionChanged += new System.EventHandler(this.DgvLibrary_OnRowChange);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 433);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlexDL by BRH Media";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.cxtEpisodes.ResumeLayout(false);
            this.cxtEpisodeOptions.ResumeLayout(false);
            this.cxtLibrarySections.ResumeLayout(false);
            this.cxtContentOptions.ResumeLayout(false);
            this.cxtStreamOptions.ResumeLayout(false);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.gbStreamControl.ResumeLayout(false);
            this.tlpContentOptionsControls.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMovies.ResumeLayout(false);
            this.tabTV.ResumeLayout(false);
            this.tlpTV.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpContentOptions.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVShows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.ComponentModel.BackgroundWorker wkrUpdateContentView;
        private System.Windows.Forms.SaveFileDialog sfdSaveProfile;
        public System.Windows.Forms.FolderBrowserDialog fbdSave;
        private System.Windows.Forms.OpenFileDialog ofdLoadProfile;
        private System.ComponentModel.BackgroundWorker wkrDownloadAsync;
        private System.Windows.Forms.ToolTip tipMain;
        private libbrhscgui.Components.AbortableBackgroundWorker wkrGetMetadata;
        private MetroSet_UI.Controls.MetroSetContextMenuStrip cxtEpisodes;
        private System.Windows.Forms.ToolStripMenuItem itmDownloadThisEpisode;
        private System.Windows.Forms.ToolStripMenuItem itmDownloadAllEpisodes;
        private MetroSet_UI.Controls.MetroSetContextMenuStrip cxtLibrarySections;
        private System.Windows.Forms.ToolStripMenuItem itmManuallyLoadSection;
        private MetroSet_UI.Controls.MetroSetContextMenuStrip cxtEpisodeOptions;
        private System.Windows.Forms.ToolStripMenuItem itmEpisodeMetadata;
        private System.Windows.Forms.ToolStripMenuItem itmEpisodeDownload;
        private System.Windows.Forms.ToolStripMenuItem itmDGVDownloadThisEpisode;
        private System.Windows.Forms.ToolStripMenuItem itmDGVDownloadThisSeason;
        private MetroSet_UI.Controls.MetroSetContextMenuStrip cxtContentOptions;
        private System.Windows.Forms.ToolStripMenuItem itmContentMetadata;
        private System.Windows.Forms.ToolStripMenuItem itmContentDownload;
        private System.Windows.Forms.OpenFileDialog ofdMetadata;
        private PlexDL.Common.Components.FlatDataGridView dgvLibrary;
        private System.Windows.Forms.NotifyIcon nfyMain;
        private MetroSet_UI.Controls.MetroSetContextMenuStrip cxtStreamOptions;
        private System.Windows.Forms.ToolStripMenuItem itmStreamInPVS;
        private System.Windows.Forms.ToolStripMenuItem itmStreamInVLC;
        private System.Windows.Forms.ToolStripMenuItem itmStreamInBrowser;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripProgressBar pbMain;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.GroupBox gbStreamControl;
        private System.Windows.Forms.Button btnHTTPPlay;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabMovies;
        private Common.Components.FlatDataGridView dgvContent;
        private System.Windows.Forms.TabPage tabTV;
        private System.Windows.Forms.TableLayoutPanel tlpTV;
        private Common.Components.FlatDataGridView dgvTVShows;
        private Common.Components.FlatDataGridView dgvSeasons;
        private Common.Components.FlatDataGridView dgvEpisodes;
        private System.Windows.Forms.TabPage tabLog;
        private MetroSet_UI.Controls.MetroSetListBox lstLog;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmExportObj;
        private System.Windows.Forms.ToolStripMenuItem itmSetDlDirectory;
        private System.Windows.Forms.ToolStripMenuItem itmProfile;
        private System.Windows.Forms.ToolStripMenuItem itmLoadProfile;
        private System.Windows.Forms.ToolStripMenuItem itmSaveProfile;
        private System.Windows.Forms.ToolStripMenuItem itmServers;
        private System.Windows.Forms.ToolStripMenuItem itmServerManager;
        private System.Windows.Forms.ToolStripMenuItem itmContent;
        private System.Windows.Forms.ToolStripMenuItem itmMetadata;
        private System.Windows.Forms.ToolStripMenuItem itmStartSearch;
        private System.Windows.Forms.ToolStripMenuItem itmSearchStatus;
        private System.Windows.Forms.ToolStripMenuItem itmLogging;
        private System.Windows.Forms.ToolStripMenuItem itmLogViewer;
        private System.Windows.Forms.ToolStripMenuItem itmCaching;
        private System.Windows.Forms.ToolStripMenuItem itmCacheMetrics;
        private System.Windows.Forms.ToolStripMenuItem itmClearCache;
        private System.Windows.Forms.ToolStripMenuItem itmCacheOptions;
        private System.Windows.Forms.ToolStripMenuItem itmAbout;
        private System.Windows.Forms.TableLayoutPanel tlpContentOptions;
        private System.Windows.Forms.TableLayoutPanel tlpContentOptionsControls;
    }
}

