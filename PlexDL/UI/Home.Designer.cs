using System.ComponentModel;
using System.Windows.Forms;
using libbrhscgui.Components;
using PlexDL.Common.Components;

namespace PlexDL.UI
{
    partial class Home
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wkrUpdateContentView = new System.ComponentModel.BackgroundWorker();
            this.sfdSaveProfile = new System.Windows.Forms.SaveFileDialog();
            this.fbdSave = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdLoadProfile = new System.Windows.Forms.OpenFileDialog();
            this.wkrDownloadAsync = new System.ComponentModel.BackgroundWorker();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.wkrGetMetadata = new libbrhscgui.Components.AbortableBackgroundWorker();
            this.cxtEpisodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDownloadAllEpisodes = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtEpisodeOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmEpisodeMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisSeason = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtLibrarySections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmManuallyLoadSection = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtContentOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmContentMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdMetadata = new System.Windows.Forms.OpenFileDialog();
            this.nfyMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cxtStreamOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmStreamInPVS = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStreamInVLC = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStreamInBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.gbStreamControl = new System.Windows.Forms.GroupBox();
            this.tlpContentOptionsControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnHTTPPlay = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMovies = new System.Windows.Forms.TabPage();
            this.tlpMovies = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMovies = new PlexDL.Common.Components.FlatDataGridView();
            this.tabTV = new System.Windows.Forms.TabPage();
            this.tlpTV = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTVShows = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvSeasons = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvEpisodes = new PlexDL.Common.Components.FlatDataGridView();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tlpLog = new System.Windows.Forms.TableLayoutPanel();
            this.dgvLog = new PlexDL.Common.Components.FlatDataGridView();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpContentOptions = new System.Windows.Forms.TableLayoutPanel();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.dgvLibrary = new PlexDL.Common.Components.FlatDataGridView();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLoadProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSaveProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExportObj = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSetDlDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServers = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServerManager = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContent = new System.Windows.Forms.ToolStripMenuItem();
            this.itmMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStartSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCaching = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCacheMetrics = new System.Windows.Forms.ToolStripMenuItem();
            this.itmClearCache = new System.Windows.Forms.ToolStripMenuItem();
            this.itmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.lblViewing = new System.Windows.Forms.ToolStripLabel();
            this.lblViewingValue = new System.Windows.Forms.ToolStripLabel();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblProgress = new System.Windows.Forms.ToolStripLabel();
            this.tmrWorkerTimeout = new System.Windows.Forms.Timer(this.components);
            this.cxtEpisodes.SuspendLayout();
            this.cxtEpisodeOptions.SuspendLayout();
            this.cxtLibrarySections.SuspendLayout();
            this.cxtContentOptions.SuspendLayout();
            this.cxtStreamOptions.SuspendLayout();
            this.gbStreamControl.SuspendLayout();
            this.tlpContentOptionsControls.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabMovies.SuspendLayout();
            this.tlpMovies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
            this.tabTV.SuspendLayout();
            this.tlpTV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVShows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            this.tabLog.SuspendLayout();
            this.tlpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.tlpContentOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).BeginInit();
            this.menuMain.SuspendLayout();
            this.tsMain.SuspendLayout();
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
            // gbStreamControl
            // 
            this.tlpContentOptions.SetColumnSpan(this.gbStreamControl, 2);
            this.gbStreamControl.Controls.Add(this.tlpContentOptionsControls);
            this.gbStreamControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStreamControl.Location = new System.Drawing.Point(3, 3);
            this.gbStreamControl.Name = "gbStreamControl";
            this.gbStreamControl.Size = new System.Drawing.Size(268, 55);
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
            this.tlpContentOptionsControls.Size = new System.Drawing.Size(262, 36);
            this.tlpContentOptionsControls.TabIndex = 0;
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(90, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(81, 30);
            this.btnPause.TabIndex = 31;
            this.btnPause.Text = "Resume";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnHTTPPlay
            // 
            this.btnHTTPPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHTTPPlay.Location = new System.Drawing.Point(177, 3);
            this.btnHTTPPlay.Name = "btnHTTPPlay";
            this.btnHTTPPlay.Size = new System.Drawing.Size(82, 30);
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
            this.btnDownload.Size = new System.Drawing.Size(81, 30);
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
            this.tabMain.Location = new System.Drawing.Point(287, 7);
            this.tabMain.Name = "tabMain";
            this.tlpMain.SetRowSpan(this.tabMain, 4);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(662, 370);
            this.tabMain.TabIndex = 26;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.tlpMovies);
            this.tabMovies.Location = new System.Drawing.Point(4, 22);
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovies.Size = new System.Drawing.Size(654, 344);
            this.tabMovies.TabIndex = 0;
            this.tabMovies.Text = "Movies";
            this.tabMovies.UseVisualStyleBackColor = true;
            // 
            // tlpMovies
            // 
            this.tlpMovies.ColumnCount = 1;
            this.tlpMovies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMovies.Controls.Add(this.dgvMovies, 0, 0);
            this.tlpMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMovies.Location = new System.Drawing.Point(3, 3);
            this.tlpMovies.Name = "tlpMovies";
            this.tlpMovies.RowCount = 1;
            this.tlpMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMovies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tlpMovies.Size = new System.Drawing.Size(648, 338);
            this.tlpMovies.TabIndex = 19;
            // 
            // dgvMovies
            // 
            this.dgvMovies.AllowUserToAddRows = false;
            this.dgvMovies.AllowUserToDeleteRows = false;
            this.dgvMovies.AllowUserToOrderColumns = true;
            this.dgvMovies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovies.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMovies.ContextMenuStrip = this.cxtContentOptions;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMovies.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovies.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMovies.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvMovies.Location = new System.Drawing.Point(2, 2);
            this.dgvMovies.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMovies.MultiSelect = false;
            this.dgvMovies.Name = "dgvMovies";
            this.dgvMovies.RowHeadersVisible = false;
            this.dgvMovies.RowsEmptyText = "No Movies Found";
            this.dgvMovies.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovies.Size = new System.Drawing.Size(644, 334);
            this.dgvMovies.TabIndex = 18;
            this.dgvMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMovies_CellContentDoubleClick);
            this.dgvMovies.SelectionChanged += new System.EventHandler(this.dgvMovies_OnRowChange);
            // 
            // tabTV
            // 
            this.tabTV.Controls.Add(this.tlpTV);
            this.tabTV.Location = new System.Drawing.Point(4, 22);
            this.tabTV.Name = "tabTV";
            this.tabTV.Padding = new System.Windows.Forms.Padding(3);
            this.tabTV.Size = new System.Drawing.Size(654, 344);
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
            this.tlpTV.Size = new System.Drawing.Size(648, 338);
            this.tlpTV.TabIndex = 0;
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
            this.dgvTVShows.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.tlpTV.SetRowSpan(this.dgvTVShows, 2);
            this.dgvTVShows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTVShows.Size = new System.Drawing.Size(320, 334);
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
            this.dgvSeasons.Location = new System.Drawing.Point(326, 2);
            this.dgvSeasons.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSeasons.MultiSelect = false;
            this.dgvSeasons.Name = "dgvSeasons";
            this.dgvSeasons.RowHeadersVisible = false;
            this.dgvSeasons.RowsEmptyText = "No TV Seasons Found";
            this.dgvSeasons.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvSeasons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSeasons.Size = new System.Drawing.Size(320, 165);
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
            this.dgvEpisodes.Location = new System.Drawing.Point(326, 171);
            this.dgvEpisodes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEpisodes.MultiSelect = false;
            this.dgvEpisodes.Name = "dgvEpisodes";
            this.dgvEpisodes.RowHeadersVisible = false;
            this.dgvEpisodes.RowsEmptyText = "No TV Episodes Found";
            this.dgvEpisodes.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEpisodes.Size = new System.Drawing.Size(320, 165);
            this.dgvEpisodes.TabIndex = 21;
            this.dgvEpisodes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvEpisodes_CellContentDoubleClick);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tlpLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(654, 344);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // tlpLog
            // 
            this.tlpLog.ColumnCount = 1;
            this.tlpLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLog.Controls.Add(this.dgvLog, 0, 0);
            this.tlpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLog.Location = new System.Drawing.Point(3, 3);
            this.tlpLog.Name = "tlpLog";
            this.tlpLog.RowCount = 1;
            this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tlpLog.Size = new System.Drawing.Size(648, 338);
            this.tlpLog.TabIndex = 1;
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToOrderColumns = true;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvLog.Location = new System.Drawing.Point(3, 3);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.RowsEmptyText = "No Log Data";
            this.dgvLog.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(642, 332);
            this.dgvLog.TabIndex = 0;
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
            this.tlpMain.Padding = new System.Windows.Forms.Padding(4);
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.Size = new System.Drawing.Size(956, 384);
            this.tlpMain.TabIndex = 29;
            this.tlpMain.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpMain_Paint);
            // 
            // tlpContentOptions
            // 
            this.tlpContentOptions.ColumnCount = 2;
            this.tlpContentOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContentOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContentOptions.Controls.Add(this.gbStreamControl, 0, 0);
            this.tlpContentOptions.Controls.Add(this.pbMain, 0, 1);
            this.tlpContentOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContentOptions.Location = new System.Drawing.Point(7, 7);
            this.tlpContentOptions.Name = "tlpContentOptions";
            this.tlpContentOptions.RowCount = 2;
            this.tlpContentOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpContentOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpContentOptions.Size = new System.Drawing.Size(274, 88);
            this.tlpContentOptions.TabIndex = 31;
            // 
            // pbMain
            // 
            this.tlpContentOptions.SetColumnSpan(this.pbMain, 2);
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(3, 64);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(268, 21);
            this.pbMain.TabIndex = 34;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLibrary.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLibrary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLibrary.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvLibrary.Location = new System.Drawing.Point(6, 100);
            this.dgvLibrary.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLibrary.MultiSelect = false;
            this.dgvLibrary.Name = "dgvLibrary";
            this.dgvLibrary.RowHeadersVisible = false;
            this.dgvLibrary.RowsEmptyText = "No Library Sections Found";
            this.dgvLibrary.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.tlpMain.SetRowSpan(this.dgvLibrary, 3);
            this.dgvLibrary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLibrary.Size = new System.Drawing.Size(276, 278);
            this.dgvLibrary.TabIndex = 16;
            this.dgvLibrary.SelectionChanged += new System.EventHandler(this.DgvLibrary_OnRowChange);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
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
            this.itmLoadProfile,
            this.itmSaveProfile,
            this.itmExportObj,
            this.itmSetDlDirectory,
            this.itmSettings});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmLoadProfile
            // 
            this.itmLoadProfile.Name = "itmLoadProfile";
            this.itmLoadProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmLoadProfile.Size = new System.Drawing.Size(198, 22);
            this.itmLoadProfile.Text = "Load";
            this.itmLoadProfile.Click += new System.EventHandler(this.itmLoadProfile_Click);
            // 
            // itmSaveProfile
            // 
            this.itmSaveProfile.Name = "itmSaveProfile";
            this.itmSaveProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmSaveProfile.Size = new System.Drawing.Size(198, 22);
            this.itmSaveProfile.Text = "Save";
            this.itmSaveProfile.Click += new System.EventHandler(this.itmSaveProfile_Click);
            // 
            // itmExportObj
            // 
            this.itmExportObj.Name = "itmExportObj";
            this.itmExportObj.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itmExportObj.Size = new System.Drawing.Size(198, 22);
            this.itmExportObj.Text = "Export";
            this.itmExportObj.Click += new System.EventHandler(this.itmExportObj_Click);
            // 
            // itmSetDlDirectory
            // 
            this.itmSetDlDirectory.Name = "itmSetDlDirectory";
            this.itmSetDlDirectory.Size = new System.Drawing.Size(198, 22);
            this.itmSetDlDirectory.Text = "Set Download Directory";
            this.itmSetDlDirectory.Click += new System.EventHandler(this.itmSetDlDirectory_Click);
            // 
            // itmSettings
            // 
            this.itmSettings.Name = "itmSettings";
            this.itmSettings.Size = new System.Drawing.Size(198, 22);
            this.itmSettings.Text = "Settings";
            this.itmSettings.Click += new System.EventHandler(this.itmSettings_Click);
            // 
            // itmServers
            // 
            this.itmServers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmServerManager,
            this.itmDisconnect});
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
            // itmDisconnect
            // 
            this.itmDisconnect.Enabled = false;
            this.itmDisconnect.Name = "itmDisconnect";
            this.itmDisconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.itmDisconnect.Size = new System.Drawing.Size(198, 22);
            this.itmDisconnect.Text = "Disconnect";
            this.itmDisconnect.Click += new System.EventHandler(this.itmDisconnect_Click);
            // 
            // itmContent
            // 
            this.itmContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmMetadata,
            this.itmStartSearch});
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
            this.itmClearCache});
            this.itmCaching.Name = "itmCaching";
            this.itmCaching.Size = new System.Drawing.Size(63, 20);
            this.itmCaching.Text = "Caching";
            // 
            // itmCacheMetrics
            // 
            this.itmCacheMetrics.Name = "itmCacheMetrics";
            this.itmCacheMetrics.Size = new System.Drawing.Size(137, 22);
            this.itmCacheMetrics.Text = "Metrics";
            this.itmCacheMetrics.Click += new System.EventHandler(this.itmCacheMetrics_Click);
            // 
            // itmClearCache
            // 
            this.itmClearCache.Name = "itmClearCache";
            this.itmClearCache.Size = new System.Drawing.Size(137, 22);
            this.itmClearCache.Text = "Clear Cache";
            this.itmClearCache.Click += new System.EventHandler(this.itmClearCache_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new System.Drawing.Size(52, 20);
            this.itmAbout.Text = "About";
            this.itmAbout.Click += new System.EventHandler(this.itmAbout_Click);
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblViewing,
            this.lblViewingValue,
            this.tsSeparator1,
            this.lblProgress});
            this.tsMain.Location = new System.Drawing.Point(0, 408);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.tsMain.Size = new System.Drawing.Size(956, 25);
            this.tsMain.TabIndex = 31;
            this.tsMain.Text = "toolStrip1";
            // 
            // lblViewing
            // 
            this.lblViewing.Name = "lblViewing";
            this.lblViewing.Size = new System.Drawing.Size(52, 20);
            this.lblViewing.Text = "Viewing:";
            // 
            // lblViewingValue
            // 
            this.lblViewingValue.Name = "lblViewingValue";
            this.lblViewingValue.Size = new System.Drawing.Size(24, 20);
            this.lblViewingValue.Text = "0/0";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(26, 20);
            this.lblProgress.Text = "Idle";
            // 
            // tmrWorkerTimeout
            // 
            this.tmrWorkerTimeout.Interval = 3000;
            this.tmrWorkerTimeout.Tick += new System.EventHandler(this.tmrWorkerTimeout_Tick);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 433);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.menuMain);
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
            this.gbStreamControl.ResumeLayout(false);
            this.tlpContentOptionsControls.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMovies.ResumeLayout(false);
            this.tlpMovies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
            this.tabTV.ResumeLayout(false);
            this.tlpTV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVShows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.tlpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpContentOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibrary)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public BackgroundWorker wkrUpdateContentView;
        private SaveFileDialog sfdSaveProfile;
        public FolderBrowserDialog fbdSave;
        private OpenFileDialog ofdLoadProfile;
        private BackgroundWorker wkrDownloadAsync;
        private ToolTip tipMain;
        private AbortableBackgroundWorker wkrGetMetadata;
        private ContextMenuStrip cxtEpisodes;
        private ToolStripMenuItem itmDownloadThisEpisode;
        private ToolStripMenuItem itmDownloadAllEpisodes;
        private ContextMenuStrip cxtLibrarySections;
        private ToolStripMenuItem itmManuallyLoadSection;
        private ContextMenuStrip cxtEpisodeOptions;
        private ToolStripMenuItem itmEpisodeMetadata;
        private ToolStripMenuItem itmEpisodeDownload;
        private ToolStripMenuItem itmDGVDownloadThisEpisode;
        private ToolStripMenuItem itmDGVDownloadThisSeason;
        private ContextMenuStrip cxtContentOptions;
        private ToolStripMenuItem itmContentMetadata;
        private ToolStripMenuItem itmContentDownload;
        private OpenFileDialog ofdMetadata;
        private FlatDataGridView dgvLibrary;
        private NotifyIcon nfyMain;
        private ContextMenuStrip cxtStreamOptions;
        private ToolStripMenuItem itmStreamInPVS;
        private ToolStripMenuItem itmStreamInVLC;
        private ToolStripMenuItem itmStreamInBrowser;
        private SaveFileDialog sfdExport;
        private GroupBox gbStreamControl;
        private Button btnHTTPPlay;
        private Button btnDownload;
        private Button btnPause;
        private TabControl tabMain;
        private TabPage tabMovies;
        private FlatDataGridView dgvMovies;
        private TabPage tabTV;
        private TableLayoutPanel tlpTV;
        private FlatDataGridView dgvTVShows;
        private FlatDataGridView dgvSeasons;
        private FlatDataGridView dgvEpisodes;
        private TabPage tabLog;
        private TableLayoutPanel tlpMain;
        private MenuStrip menuMain;
        private ToolStripMenuItem itmFile;
        private ToolStripMenuItem itmExportObj;
        private ToolStripMenuItem itmSetDlDirectory;
        private ToolStripMenuItem itmLoadProfile;
        private ToolStripMenuItem itmSaveProfile;
        private ToolStripMenuItem itmServers;
        private ToolStripMenuItem itmServerManager;
        private ToolStripMenuItem itmContent;
        private ToolStripMenuItem itmMetadata;
        private ToolStripMenuItem itmStartSearch;
        private ToolStripMenuItem itmLogging;
        private ToolStripMenuItem itmLogViewer;
        private ToolStripMenuItem itmCaching;
        private ToolStripMenuItem itmCacheMetrics;
        private ToolStripMenuItem itmClearCache;
        private ToolStripMenuItem itmAbout;
        private TableLayoutPanel tlpContentOptions;
        private TableLayoutPanel tlpContentOptionsControls;
        private ToolStripMenuItem itmDisconnect;
        private ToolStripMenuItem itmSettings;
        private ToolStrip tsMain;
        private ToolStripSeparator tsSeparator1;
        private ToolStripLabel lblProgress;
        private ProgressBar pbMain;
        private ToolStripLabel lblViewingValue;
        private ToolStripLabel lblViewing;
        private Timer tmrWorkerTimeout;
        private FlatDataGridView dgvLog;
        private TableLayoutPanel tlpLog;
        private TableLayoutPanel tlpMovies;
    }
}

