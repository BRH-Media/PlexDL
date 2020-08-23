using System;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wkrUpdateContentView = new System.ComponentModel.BackgroundWorker();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.fbdDownloadPath = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.wkrDownloadAsync = new System.ComponentModel.BackgroundWorker();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.wkrGetMetadata = new libbrhscgui.Components.AbortableBackgroundWorker();
            this.cxtEpisodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDownloadAllEpisodes = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtEpisodeOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmEpisodeMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeMetadataView = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeMetadataExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisEpisode = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisSeason = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVViewEpisodeDownloadLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeStream = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmEpisodeCast = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtLibrarySections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmManuallyLoadSection = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRenderKeyColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMovieOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmContentMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentMetadataView = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentMetadataExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisMovie = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVViewMovieDownloadLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentStream = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContentCast = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnStream = new System.Windows.Forms.Button();
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
            this.tabMusic = new System.Windows.Forms.TabPage();
            this.tlpMusic = new System.Windows.Forms.TableLayoutPanel();
            this.dgvArtists = new PlexDL.Common.Components.FlatDataGridView();
            this.cxtTrackOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmTrackMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackMetadataView = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackMetadataExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVDownloadThisAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDGVViewTrackDownloadLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackStream = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmTrackCast = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAlbums = new PlexDL.Common.Components.FlatDataGridView();
            this.dgvTracks = new PlexDL.Common.Components.FlatDataGridView();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tlpLog = new System.Windows.Forms.TableLayoutPanel();
            this.dgvLog = new PlexDL.Common.Components.FlatDataGridView();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpContentOptions = new System.Windows.Forms.TableLayoutPanel();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.dgvSections = new PlexDL.Common.Components.FlatDataGridView();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLoadProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSaveProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExportObj = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itmData = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSetDlDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOpenDataFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCleanupAllData = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServers = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServerManager = new System.Windows.Forms.ToolStripMenuItem();
            this.itmClearMyToken = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.itmContent = new System.Windows.Forms.ToolStripMenuItem();
            this.itmMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStartSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCast = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLogViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCaching = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCacheMetrics = new System.Windows.Forms.ToolStripMenuItem();
            this.itmClearCache = new System.Windows.Forms.ToolStripMenuItem();
            this.itmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRepo = new System.Windows.Forms.ToolStripMenuItem();
            this.itmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.lblViewing = new System.Windows.Forms.ToolStripLabel();
            this.lblViewingValue = new System.Windows.Forms.ToolStripLabel();
            this.sepViewing = new System.Windows.Forms.ToolStripSeparator();
            this.lblDownloading = new System.Windows.Forms.ToolStripLabel();
            this.lblDownloadingValue = new System.Windows.Forms.ToolStripLabel();
            this.sepDownloading = new System.Windows.Forms.ToolStripSeparator();
            this.lblSpeed = new System.Windows.Forms.ToolStripLabel();
            this.lblSpeedValue = new System.Windows.Forms.ToolStripLabel();
            this.sepSpeed = new System.Windows.Forms.ToolStripSeparator();
            this.lblEta = new System.Windows.Forms.ToolStripLabel();
            this.lblEtaValue = new System.Windows.Forms.ToolStripLabel();
            this.sepEta = new System.Windows.Forms.ToolStripSeparator();
            this.lblProgress = new System.Windows.Forms.ToolStripLabel();
            this.lblBeta = new System.Windows.Forms.ToolStripLabel();
            this.sepBeta = new System.Windows.Forms.ToolStripSeparator();
            this.lblSidValue = new System.Windows.Forms.ToolStripLabel();
            this.lblSid = new System.Windows.Forms.ToolStripLabel();
            this.tmrWorkerTimeout = new System.Windows.Forms.Timer(this.components);
            this.cxtTracks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmDownloadThisTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDownloadThisAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtEpisodes.SuspendLayout();
            this.cxtEpisodeOptions.SuspendLayout();
            this.cxtLibrarySections.SuspendLayout();
            this.cxtMovieOptions.SuspendLayout();
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
            this.tabMusic.SuspendLayout();
            this.tlpMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtists)).BeginInit();
            this.cxtTrackOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlbums)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracks)).BeginInit();
            this.tabLog.SuspendLayout();
            this.tlpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.tlpContentOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).BeginInit();
            this.menuMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.cxtTracks.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfdSave
            // 
            this.sfdSave.DefaultExt = "prof";
            this.sfdSave.Filter = "XML Profile|*.prof";
            this.sfdSave.Title = "Save XML Profile";
            // 
            // fbdDownloadPath
            // 
            this.fbdDownloadPath.Description = "Select a path to save downloaded items";
            // 
            // ofdLoad
            // 
            this.ofdLoad.Filter = "PlexDL File|*.prof;*.pxz|XML Profile|*.prof|PXZ File|*.pxz";
            this.ofdLoad.Title = "Load XML Profile";
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
            this.itmEpisodeDownload,
            this.itmEpisodeStream,
            this.itmEpisodeSearch,
            this.itmEpisodeCast});
            this.cxtEpisodeOptions.Name = "cxtEpisodeOptions";
            this.cxtEpisodeOptions.Size = new System.Drawing.Size(129, 114);
            this.cxtEpisodeOptions.Opening += new System.ComponentModel.CancelEventHandler(this.CxtEpisodeOptions_Opening);
            // 
            // itmEpisodeMetadata
            // 
            this.itmEpisodeMetadata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmEpisodeMetadataView,
            this.itmEpisodeMetadataExport});
            this.itmEpisodeMetadata.Name = "itmEpisodeMetadata";
            this.itmEpisodeMetadata.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeMetadata.Text = "Metadata";
            // 
            // itmEpisodeMetadataView
            // 
            this.itmEpisodeMetadataView.Name = "itmEpisodeMetadataView";
            this.itmEpisodeMetadataView.Size = new System.Drawing.Size(108, 22);
            this.itmEpisodeMetadataView.Text = "View";
            this.itmEpisodeMetadataView.Click += new System.EventHandler(this.ItmEpisodeMetadataView_Click);
            // 
            // itmEpisodeMetadataExport
            // 
            this.itmEpisodeMetadataExport.Name = "itmEpisodeMetadataExport";
            this.itmEpisodeMetadataExport.Size = new System.Drawing.Size(108, 22);
            this.itmEpisodeMetadataExport.Text = "Export";
            this.itmEpisodeMetadataExport.Click += new System.EventHandler(this.ItmEpisodeMetadataExport_Click);
            // 
            // itmEpisodeDownload
            // 
            this.itmEpisodeDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDGVDownloadThisEpisode,
            this.itmDGVDownloadThisSeason,
            this.itmDGVViewEpisodeDownloadLink});
            this.itmEpisodeDownload.Name = "itmEpisodeDownload";
            this.itmEpisodeDownload.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeDownload.Text = "Download";
            // 
            // itmDGVDownloadThisEpisode
            // 
            this.itmDGVDownloadThisEpisode.Name = "itmDGVDownloadThisEpisode";
            this.itmDGVDownloadThisEpisode.Size = new System.Drawing.Size(172, 22);
            this.itmDGVDownloadThisEpisode.Text = "Download Episode";
            this.itmDGVDownloadThisEpisode.Click += new System.EventHandler(this.ItmDGVDownloadThisEpisode_Click);
            // 
            // itmDGVDownloadThisSeason
            // 
            this.itmDGVDownloadThisSeason.Name = "itmDGVDownloadThisSeason";
            this.itmDGVDownloadThisSeason.Size = new System.Drawing.Size(172, 22);
            this.itmDGVDownloadThisSeason.Text = "Download Season";
            this.itmDGVDownloadThisSeason.Click += new System.EventHandler(this.ItmDGVDownloadThisSeason_Click);
            // 
            // itmDGVViewEpisodeDownloadLink
            // 
            this.itmDGVViewEpisodeDownloadLink.Name = "itmDGVViewEpisodeDownloadLink";
            this.itmDGVViewEpisodeDownloadLink.Size = new System.Drawing.Size(172, 22);
            this.itmDGVViewEpisodeDownloadLink.Text = "View Episode Link";
            this.itmDGVViewEpisodeDownloadLink.Click += new System.EventHandler(this.ItmDGVViewEpisodeDownloadLink_Click);
            // 
            // itmEpisodeStream
            // 
            this.itmEpisodeStream.Name = "itmEpisodeStream";
            this.itmEpisodeStream.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeStream.Text = "Stream";
            this.itmEpisodeStream.Click += new System.EventHandler(this.ItmEpisodeStream_Click);
            // 
            // itmEpisodeSearch
            // 
            this.itmEpisodeSearch.Name = "itmEpisodeSearch";
            this.itmEpisodeSearch.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeSearch.Text = "Search";
            this.itmEpisodeSearch.Click += new System.EventHandler(this.ItmEpisodeSearch_Click);
            // 
            // itmEpisodeCast
            // 
            this.itmEpisodeCast.Name = "itmEpisodeCast";
            this.itmEpisodeCast.Size = new System.Drawing.Size(128, 22);
            this.itmEpisodeCast.Text = "Cast";
            this.itmEpisodeCast.Click += new System.EventHandler(this.ItmEpisodeCast_Click);
            // 
            // cxtLibrarySections
            // 
            this.cxtLibrarySections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtLibrarySections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmManuallyLoadSection,
            this.itmRenderKeyColumn});
            this.cxtLibrarySections.Name = "cxtLibrarySections";
            this.cxtLibrarySections.Size = new System.Drawing.Size(195, 48);
            this.cxtLibrarySections.Opening += new System.ComponentModel.CancelEventHandler(this.CxtLibrarySections_Opening);
            // 
            // itmManuallyLoadSection
            // 
            this.itmManuallyLoadSection.Name = "itmManuallyLoadSection";
            this.itmManuallyLoadSection.Size = new System.Drawing.Size(194, 22);
            this.itmManuallyLoadSection.Text = "Manually Load Section";
            this.itmManuallyLoadSection.Click += new System.EventHandler(this.ItmManuallyLoadSection_Click);
            // 
            // itmRenderKeyColumn
            // 
            this.itmRenderKeyColumn.CheckOnClick = true;
            this.itmRenderKeyColumn.Name = "itmRenderKeyColumn";
            this.itmRenderKeyColumn.Size = new System.Drawing.Size(194, 22);
            this.itmRenderKeyColumn.Text = "Render Key Column";
            this.itmRenderKeyColumn.Click += new System.EventHandler(this.ItmRenderKeyColumn_Click);
            // 
            // cxtMovieOptions
            // 
            this.cxtMovieOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtMovieOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmContentMetadata,
            this.itmContentDownload,
            this.itmContentStream,
            this.itmContentSearch,
            this.itmContentCast});
            this.cxtMovieOptions.Name = "cxtEpisodeOptions";
            this.cxtMovieOptions.Size = new System.Drawing.Size(129, 114);
            this.cxtMovieOptions.Opening += new System.ComponentModel.CancelEventHandler(this.CxtContentOptions_Opening);
            // 
            // itmContentMetadata
            // 
            this.itmContentMetadata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmContentMetadataView,
            this.itmContentMetadataExport});
            this.itmContentMetadata.Name = "itmContentMetadata";
            this.itmContentMetadata.Size = new System.Drawing.Size(128, 22);
            this.itmContentMetadata.Text = "Metadata";
            // 
            // itmContentMetadataView
            // 
            this.itmContentMetadataView.Name = "itmContentMetadataView";
            this.itmContentMetadataView.Size = new System.Drawing.Size(108, 22);
            this.itmContentMetadataView.Text = "View";
            this.itmContentMetadataView.Click += new System.EventHandler(this.ItmContentMetadataView_Click);
            // 
            // itmContentMetadataExport
            // 
            this.itmContentMetadataExport.Name = "itmContentMetadataExport";
            this.itmContentMetadataExport.Size = new System.Drawing.Size(108, 22);
            this.itmContentMetadataExport.Text = "Export";
            this.itmContentMetadataExport.Click += new System.EventHandler(this.ItmContentMetadataExport_Click);
            // 
            // itmContentDownload
            // 
            this.itmContentDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDGVDownloadThisMovie,
            this.itmDGVViewMovieDownloadLink});
            this.itmContentDownload.Name = "itmContentDownload";
            this.itmContentDownload.Size = new System.Drawing.Size(128, 22);
            this.itmContentDownload.Text = "Download";
            // 
            // itmDGVDownloadThisMovie
            // 
            this.itmDGVDownloadThisMovie.Name = "itmDGVDownloadThisMovie";
            this.itmDGVDownloadThisMovie.Size = new System.Drawing.Size(164, 22);
            this.itmDGVDownloadThisMovie.Text = "Download Movie";
            this.itmDGVDownloadThisMovie.Click += new System.EventHandler(this.ItmDGVDownloadThisMovie_Click);
            // 
            // itmDGVViewMovieDownloadLink
            // 
            this.itmDGVViewMovieDownloadLink.Name = "itmDGVViewMovieDownloadLink";
            this.itmDGVViewMovieDownloadLink.Size = new System.Drawing.Size(164, 22);
            this.itmDGVViewMovieDownloadLink.Text = "View Link";
            this.itmDGVViewMovieDownloadLink.Click += new System.EventHandler(this.ItmDGVViewMovieDownloadLink_Click);
            // 
            // itmContentStream
            // 
            this.itmContentStream.Name = "itmContentStream";
            this.itmContentStream.Size = new System.Drawing.Size(128, 22);
            this.itmContentStream.Text = "Stream";
            this.itmContentStream.Click += new System.EventHandler(this.ItmContentStream_Click);
            // 
            // itmContentSearch
            // 
            this.itmContentSearch.Name = "itmContentSearch";
            this.itmContentSearch.Size = new System.Drawing.Size(128, 22);
            this.itmContentSearch.Text = "Search";
            this.itmContentSearch.Click += new System.EventHandler(this.ItmContentSearch_Click);
            // 
            // itmContentCast
            // 
            this.itmContentCast.Name = "itmContentCast";
            this.itmContentCast.Size = new System.Drawing.Size(128, 22);
            this.itmContentCast.Text = "Cast";
            this.itmContentCast.Click += new System.EventHandler(this.ItmContentCast_Click);
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
            this.sfdExport.DefaultExt = "pxz";
            this.sfdExport.Filter = "PXZ File |*.pxz";
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
            this.tlpContentOptionsControls.Controls.Add(this.btnStream, 2, 0);
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
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // btnStream
            // 
            this.btnStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStream.Location = new System.Drawing.Point(177, 3);
            this.btnStream.Name = "btnStream";
            this.btnStream.Size = new System.Drawing.Size(82, 30);
            this.btnStream.TabIndex = 32;
            this.btnStream.Text = "Stream";
            this.btnStream.UseVisualStyleBackColor = true;
            this.btnStream.Click += new System.EventHandler(this.BtnStream_Click);
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
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabMovies);
            this.tabMain.Controls.Add(this.tabTV);
            this.tabMain.Controls.Add(this.tabMusic);
            this.tabMain.Controls.Add(this.tabLog);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(287, 7);
            this.tabMain.Name = "tabMain";
            this.tlpMain.SetRowSpan(this.tabMain, 4);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(662, 370);
            this.tabMain.TabIndex = 26;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.TabMain_SelectedIndexChanged);
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
            this.dgvMovies.ContextMenuStrip = this.cxtMovieOptions;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMovies.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovies.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMovies.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvMovies.IsContentTable = true;
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
            this.dgvMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
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
            this.dgvTVShows.ContextMenuStrip = this.cxtEpisodeOptions;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTVShows.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTVShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTVShows.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTVShows.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvTVShows.IsContentTable = false;
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
            this.dgvTVShows.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
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
            this.dgvSeasons.ContextMenuStrip = this.cxtEpisodeOptions;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSeasons.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSeasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSeasons.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSeasons.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvSeasons.IsContentTable = false;
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
            this.dgvSeasons.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEpisodes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEpisodes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEpisodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvEpisodes.IsContentTable = true;
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
            this.dgvEpisodes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
            // 
            // tabMusic
            // 
            this.tabMusic.Controls.Add(this.tlpMusic);
            this.tabMusic.Location = new System.Drawing.Point(4, 22);
            this.tabMusic.Name = "tabMusic";
            this.tabMusic.Padding = new System.Windows.Forms.Padding(3);
            this.tabMusic.Size = new System.Drawing.Size(654, 344);
            this.tabMusic.TabIndex = 3;
            this.tabMusic.Text = "Music";
            this.tabMusic.UseVisualStyleBackColor = true;
            // 
            // tlpMusic
            // 
            this.tlpMusic.BackColor = System.Drawing.Color.White;
            this.tlpMusic.ColumnCount = 2;
            this.tlpMusic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMusic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMusic.Controls.Add(this.dgvArtists, 0, 0);
            this.tlpMusic.Controls.Add(this.dgvAlbums, 1, 0);
            this.tlpMusic.Controls.Add(this.dgvTracks, 1, 1);
            this.tlpMusic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMusic.Location = new System.Drawing.Point(3, 3);
            this.tlpMusic.Margin = new System.Windows.Forms.Padding(2);
            this.tlpMusic.Name = "tlpMusic";
            this.tlpMusic.RowCount = 2;
            this.tlpMusic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMusic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMusic.Size = new System.Drawing.Size(648, 338);
            this.tlpMusic.TabIndex = 1;
            // 
            // dgvArtists
            // 
            this.dgvArtists.AllowUserToAddRows = false;
            this.dgvArtists.AllowUserToDeleteRows = false;
            this.dgvArtists.AllowUserToOrderColumns = true;
            this.dgvArtists.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArtists.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvArtists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvArtists.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvArtists.ContextMenuStrip = this.cxtTrackOptions;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvArtists.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArtists.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvArtists.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvArtists.IsContentTable = false;
            this.dgvArtists.Location = new System.Drawing.Point(2, 2);
            this.dgvArtists.Margin = new System.Windows.Forms.Padding(2);
            this.dgvArtists.MultiSelect = false;
            this.dgvArtists.Name = "dgvArtists";
            this.dgvArtists.RowHeadersVisible = false;
            this.dgvArtists.RowsEmptyText = "No Artists Found";
            this.dgvArtists.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.tlpMusic.SetRowSpan(this.dgvArtists, 2);
            this.dgvArtists.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArtists.Size = new System.Drawing.Size(320, 334);
            this.dgvArtists.TabIndex = 27;
            this.dgvArtists.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
            this.dgvArtists.SelectionChanged += new System.EventHandler(this.DgvArtists_OnRowChange);
            // 
            // cxtTrackOptions
            // 
            this.cxtTrackOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtTrackOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmTrackMetadata,
            this.itmTrackDownload,
            this.itmTrackStream,
            this.itmTrackSearch,
            this.itmTrackCast});
            this.cxtTrackOptions.Name = "cxtEpisodeOptions";
            this.cxtTrackOptions.Size = new System.Drawing.Size(129, 114);
            this.cxtTrackOptions.Opening += new System.ComponentModel.CancelEventHandler(this.CxtTrackOptions_Opening);
            // 
            // itmTrackMetadata
            // 
            this.itmTrackMetadata.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmTrackMetadataView,
            this.itmTrackMetadataExport});
            this.itmTrackMetadata.Name = "itmTrackMetadata";
            this.itmTrackMetadata.Size = new System.Drawing.Size(128, 22);
            this.itmTrackMetadata.Text = "Metadata";
            // 
            // itmTrackMetadataView
            // 
            this.itmTrackMetadataView.Name = "itmTrackMetadataView";
            this.itmTrackMetadataView.Size = new System.Drawing.Size(108, 22);
            this.itmTrackMetadataView.Text = "View";
            this.itmTrackMetadataView.Click += new System.EventHandler(this.ItmTrackMetadataView_Click);
            // 
            // itmTrackMetadataExport
            // 
            this.itmTrackMetadataExport.Name = "itmTrackMetadataExport";
            this.itmTrackMetadataExport.Size = new System.Drawing.Size(108, 22);
            this.itmTrackMetadataExport.Text = "Export";
            this.itmTrackMetadataExport.Click += new System.EventHandler(this.ItmTrackMetadataExport_Click);
            // 
            // itmTrackDownload
            // 
            this.itmTrackDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDGVDownloadThisTrack,
            this.itmDGVDownloadThisAlbum,
            this.itmDGVViewTrackDownloadLink});
            this.itmTrackDownload.Name = "itmTrackDownload";
            this.itmTrackDownload.Size = new System.Drawing.Size(128, 22);
            this.itmTrackDownload.Text = "Download";
            // 
            // itmDGVDownloadThisTrack
            // 
            this.itmDGVDownloadThisTrack.Name = "itmDGVDownloadThisTrack";
            this.itmDGVDownloadThisTrack.Size = new System.Drawing.Size(167, 22);
            this.itmDGVDownloadThisTrack.Text = "Download Track";
            this.itmDGVDownloadThisTrack.Click += new System.EventHandler(this.ItmDGVDownloadThisTrack_Click);
            // 
            // itmDGVDownloadThisAlbum
            // 
            this.itmDGVDownloadThisAlbum.Name = "itmDGVDownloadThisAlbum";
            this.itmDGVDownloadThisAlbum.Size = new System.Drawing.Size(167, 22);
            this.itmDGVDownloadThisAlbum.Text = "Download Album";
            this.itmDGVDownloadThisAlbum.Click += new System.EventHandler(this.ItmDGVDownloadThisAlbum_Click);
            // 
            // itmDGVViewTrackDownloadLink
            // 
            this.itmDGVViewTrackDownloadLink.Name = "itmDGVViewTrackDownloadLink";
            this.itmDGVViewTrackDownloadLink.Size = new System.Drawing.Size(167, 22);
            this.itmDGVViewTrackDownloadLink.Text = "View Link";
            this.itmDGVViewTrackDownloadLink.Click += new System.EventHandler(this.ItmDGVViewTrackDownloadLink_Click);
            // 
            // itmTrackStream
            // 
            this.itmTrackStream.Name = "itmTrackStream";
            this.itmTrackStream.Size = new System.Drawing.Size(128, 22);
            this.itmTrackStream.Text = "Stream";
            this.itmTrackStream.Click += new System.EventHandler(this.ItmTrackStream_Click);
            // 
            // itmTrackSearch
            // 
            this.itmTrackSearch.Name = "itmTrackSearch";
            this.itmTrackSearch.Size = new System.Drawing.Size(128, 22);
            this.itmTrackSearch.Text = "Search";
            this.itmTrackSearch.Click += new System.EventHandler(this.ItmTrackSearch_Click);
            // 
            // itmTrackCast
            // 
            this.itmTrackCast.Name = "itmTrackCast";
            this.itmTrackCast.Size = new System.Drawing.Size(128, 22);
            this.itmTrackCast.Text = "Cast";
            this.itmTrackCast.Click += new System.EventHandler(this.ItmTrackCast_Click);
            // 
            // dgvAlbums
            // 
            this.dgvAlbums.AllowUserToAddRows = false;
            this.dgvAlbums.AllowUserToDeleteRows = false;
            this.dgvAlbums.AllowUserToOrderColumns = true;
            this.dgvAlbums.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlbums.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAlbums.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlbums.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvAlbums.ContextMenuStrip = this.cxtTrackOptions;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlbums.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAlbums.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlbums.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAlbums.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvAlbums.IsContentTable = false;
            this.dgvAlbums.Location = new System.Drawing.Point(326, 2);
            this.dgvAlbums.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAlbums.MultiSelect = false;
            this.dgvAlbums.Name = "dgvAlbums";
            this.dgvAlbums.RowHeadersVisible = false;
            this.dgvAlbums.RowsEmptyText = "No Albums Found";
            this.dgvAlbums.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvAlbums.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlbums.Size = new System.Drawing.Size(320, 165);
            this.dgvAlbums.TabIndex = 20;
            this.dgvAlbums.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
            this.dgvAlbums.SelectionChanged += new System.EventHandler(this.DgvAlbums_OnRowChange);
            // 
            // dgvTracks
            // 
            this.dgvTracks.AllowUserToAddRows = false;
            this.dgvTracks.AllowUserToDeleteRows = false;
            this.dgvTracks.AllowUserToOrderColumns = true;
            this.dgvTracks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTracks.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTracks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTracks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvTracks.ContextMenuStrip = this.cxtTrackOptions;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTracks.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTracks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvTracks.IsContentTable = true;
            this.dgvTracks.Location = new System.Drawing.Point(326, 171);
            this.dgvTracks.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTracks.MultiSelect = false;
            this.dgvTracks.Name = "dgvTracks";
            this.dgvTracks.RowHeadersVisible = false;
            this.dgvTracks.RowsEmptyText = "No Tracks Found";
            this.dgvTracks.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTracks.Size = new System.Drawing.Size(320, 165);
            this.dgvTracks.TabIndex = 21;
            this.dgvTracks.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DoubleClickProcessor);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvLog.IsContentTable = false;
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
            this.tlpMain.Controls.Add(this.dgvSections, 0, 1);
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
            // dgvSections
            // 
            this.dgvSections.AllowUserToAddRows = false;
            this.dgvSections.AllowUserToDeleteRows = false;
            this.dgvSections.AllowUserToOrderColumns = true;
            this.dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSections.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSections.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSections.ContextMenuStrip = this.cxtLibrarySections;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSections.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSections.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvSections.IsContentTable = false;
            this.dgvSections.Location = new System.Drawing.Point(6, 100);
            this.dgvSections.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSections.MultiSelect = false;
            this.dgvSections.Name = "dgvSections";
            this.dgvSections.RowHeadersVisible = false;
            this.dgvSections.RowsEmptyText = "No Library Sections Found";
            this.dgvSections.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.tlpMain.SetRowSpan(this.dgvSections, 3);
            this.dgvSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSections.Size = new System.Drawing.Size(276, 278);
            this.dgvSections.TabIndex = 16;
            this.dgvSections.SelectionChanged += new System.EventHandler(this.DgvLibrary_OnRowChange);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmData,
            this.itmServers,
            this.itmContent,
            this.itmLogging,
            this.itmCaching,
            this.itmHelp});
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
            this.itmSettings});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmLoadProfile
            // 
            this.itmLoadProfile.Name = "itmLoadProfile";
            this.itmLoadProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmLoadProfile.Size = new System.Drawing.Size(148, 22);
            this.itmLoadProfile.Text = "Load";
            this.itmLoadProfile.Click += new System.EventHandler(this.ItmLoadProfile_Click);
            // 
            // itmSaveProfile
            // 
            this.itmSaveProfile.Name = "itmSaveProfile";
            this.itmSaveProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmSaveProfile.Size = new System.Drawing.Size(148, 22);
            this.itmSaveProfile.Text = "Save";
            this.itmSaveProfile.Click += new System.EventHandler(this.ItmSaveProfile_Click);
            // 
            // itmExportObj
            // 
            this.itmExportObj.Name = "itmExportObj";
            this.itmExportObj.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itmExportObj.Size = new System.Drawing.Size(148, 22);
            this.itmExportObj.Text = "Export";
            this.itmExportObj.Click += new System.EventHandler(this.ItmExportObj_Click);
            // 
            // itmSettings
            // 
            this.itmSettings.Name = "itmSettings";
            this.itmSettings.Size = new System.Drawing.Size(148, 22);
            this.itmSettings.Text = "Settings";
            this.itmSettings.Click += new System.EventHandler(this.ItmSettings_Click);
            // 
            // itmData
            // 
            this.itmData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSetDlDirectory,
            this.itmOpenDataFolder,
            this.itmCleanupAllData});
            this.itmData.Name = "itmData";
            this.itmData.Size = new System.Drawing.Size(43, 20);
            this.itmData.Text = "Data";
            // 
            // itmSetDlDirectory
            // 
            this.itmSetDlDirectory.Name = "itmSetDlDirectory";
            this.itmSetDlDirectory.Size = new System.Drawing.Size(198, 22);
            this.itmSetDlDirectory.Text = "Set Download Directory";
            this.itmSetDlDirectory.Click += new System.EventHandler(this.ItmSetDlDirectory_Click);
            // 
            // itmOpenDataFolder
            // 
            this.itmOpenDataFolder.Name = "itmOpenDataFolder";
            this.itmOpenDataFolder.Size = new System.Drawing.Size(198, 22);
            this.itmOpenDataFolder.Text = "Open Data Folder";
            this.itmOpenDataFolder.Click += new System.EventHandler(this.ItmOpenDataFolder_Click);
            // 
            // itmCleanupAllData
            // 
            this.itmCleanupAllData.Name = "itmCleanupAllData";
            this.itmCleanupAllData.Size = new System.Drawing.Size(198, 22);
            this.itmCleanupAllData.Text = "Cleanup All Data";
            this.itmCleanupAllData.Click += new System.EventHandler(this.ItmCleanupAllData_Click);
            // 
            // itmServers
            // 
            this.itmServers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmServerManager,
            this.itmClearMyToken,
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
            this.itmServerManager.Click += new System.EventHandler(this.ItmServerManager_Click);
            // 
            // itmClearMyToken
            // 
            this.itmClearMyToken.Name = "itmClearMyToken";
            this.itmClearMyToken.Size = new System.Drawing.Size(198, 22);
            this.itmClearMyToken.Text = "Clear My Token";
            this.itmClearMyToken.Click += new System.EventHandler(this.ItmClearMyToken_Click);
            // 
            // itmDisconnect
            // 
            this.itmDisconnect.Enabled = false;
            this.itmDisconnect.Name = "itmDisconnect";
            this.itmDisconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.itmDisconnect.Size = new System.Drawing.Size(198, 22);
            this.itmDisconnect.Text = "Disconnect";
            this.itmDisconnect.Click += new System.EventHandler(this.ItmDisconnect_Click);
            // 
            // itmContent
            // 
            this.itmContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmMetadata,
            this.itmStartSearch,
            this.itmCast});
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
            this.itmMetadata.Click += new System.EventHandler(this.ItmMetadata_Click);
            // 
            // itmStartSearch
            // 
            this.itmStartSearch.Name = "itmStartSearch";
            this.itmStartSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itmStartSearch.Size = new System.Drawing.Size(176, 22);
            this.itmStartSearch.Text = "Start Search";
            this.itmStartSearch.Click += new System.EventHandler(this.ItmStartSearch_Click);
            // 
            // itmCast
            // 
            this.itmCast.Name = "itmCast";
            this.itmCast.Size = new System.Drawing.Size(176, 22);
            this.itmCast.Text = "Cast";
            this.itmCast.Click += new System.EventHandler(this.ItmCast_Click);
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
            this.itmLogViewer.Click += new System.EventHandler(this.ItmLogViewer_Click);
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
            this.itmCacheMetrics.Click += new System.EventHandler(this.ItmCacheMetrics_Click);
            // 
            // itmClearCache
            // 
            this.itmClearCache.Name = "itmClearCache";
            this.itmClearCache.Size = new System.Drawing.Size(137, 22);
            this.itmClearCache.Text = "Clear Cache";
            this.itmClearCache.Click += new System.EventHandler(this.ItmClearCache_Click);
            // 
            // itmHelp
            // 
            this.itmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCheckForUpdates,
            this.itmRepo,
            this.itmAbout});
            this.itmHelp.Name = "itmHelp";
            this.itmHelp.Size = new System.Drawing.Size(44, 20);
            this.itmHelp.Text = "Help";
            // 
            // itmCheckForUpdates
            // 
            this.itmCheckForUpdates.Name = "itmCheckForUpdates";
            this.itmCheckForUpdates.Size = new System.Drawing.Size(171, 22);
            this.itmCheckForUpdates.Text = "Check for Updates";
            this.itmCheckForUpdates.Click += new System.EventHandler(this.ItmCheckForUpdates_Click);
            // 
            // itmRepo
            // 
            this.itmRepo.Name = "itmRepo";
            this.itmRepo.Size = new System.Drawing.Size(171, 22);
            this.itmRepo.Text = "Repo";
            this.itmRepo.Click += new System.EventHandler(this.ItmRepo_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new System.Drawing.Size(171, 22);
            this.itmAbout.Text = "About";
            this.itmAbout.Click += new System.EventHandler(this.ItmAbout_Click);
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblViewing,
            this.lblViewingValue,
            this.sepViewing,
            this.lblDownloading,
            this.lblDownloadingValue,
            this.sepDownloading,
            this.lblSpeed,
            this.lblSpeedValue,
            this.sepSpeed,
            this.lblEta,
            this.lblEtaValue,
            this.sepEta,
            this.lblProgress,
            this.lblBeta,
            this.sepBeta,
            this.lblSidValue,
            this.lblSid});
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
            // sepViewing
            // 
            this.sepViewing.Name = "sepViewing";
            this.sepViewing.Size = new System.Drawing.Size(6, 23);
            // 
            // lblDownloading
            // 
            this.lblDownloading.Name = "lblDownloading";
            this.lblDownloading.Size = new System.Drawing.Size(81, 20);
            this.lblDownloading.Text = "Downloading:";
            // 
            // lblDownloadingValue
            // 
            this.lblDownloadingValue.Name = "lblDownloadingValue";
            this.lblDownloadingValue.Size = new System.Drawing.Size(15, 20);
            this.lblDownloadingValue.Text = "~";
            // 
            // sepDownloading
            // 
            this.sepDownloading.Name = "sepDownloading";
            this.sepDownloading.Size = new System.Drawing.Size(6, 23);
            // 
            // lblSpeed
            // 
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(42, 20);
            this.lblSpeed.Text = "Speed:";
            // 
            // lblSpeedValue
            // 
            this.lblSpeedValue.Name = "lblSpeedValue";
            this.lblSpeedValue.Size = new System.Drawing.Size(15, 20);
            this.lblSpeedValue.Text = "~";
            // 
            // sepSpeed
            // 
            this.sepSpeed.Name = "sepSpeed";
            this.sepSpeed.Size = new System.Drawing.Size(6, 23);
            // 
            // lblEta
            // 
            this.lblEta.Name = "lblEta";
            this.lblEta.Size = new System.Drawing.Size(29, 20);
            this.lblEta.Text = "ETA:";
            // 
            // lblEtaValue
            // 
            this.lblEtaValue.Name = "lblEtaValue";
            this.lblEtaValue.Size = new System.Drawing.Size(15, 20);
            this.lblEtaValue.Text = "~";
            // 
            // sepEta
            // 
            this.sepEta.Name = "sepEta";
            this.sepEta.Size = new System.Drawing.Size(6, 23);
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(26, 20);
            this.lblProgress.Text = "Idle";
            // 
            // lblBeta
            // 
            this.lblBeta.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblBeta.ForeColor = System.Drawing.Color.Chocolate;
            this.lblBeta.Name = "lblBeta";
            this.lblBeta.Size = new System.Drawing.Size(91, 20);
            this.lblBeta.Text = "In Development";
            // 
            // sepBeta
            // 
            this.sepBeta.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sepBeta.Name = "sepBeta";
            this.sepBeta.Size = new System.Drawing.Size(6, 23);
            // 
            // lblSidValue
            // 
            this.lblSidValue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblSidValue.Name = "lblSidValue";
            this.lblSidValue.Size = new System.Drawing.Size(71, 20);
            this.lblSidValue.Text = "[SID_VALUE]";
            // 
            // lblSid
            // 
            this.lblSid.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblSid.Name = "lblSid";
            this.lblSid.Size = new System.Drawing.Size(27, 20);
            this.lblSid.Text = "SID:";
            // 
            // tmrWorkerTimeout
            // 
            this.tmrWorkerTimeout.Interval = 3000;
            this.tmrWorkerTimeout.Tick += new System.EventHandler(this.TmrWorkerTimeout_Tick);
            // 
            // cxtTracks
            // 
            this.cxtTracks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cxtTracks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDownloadThisTrack,
            this.itmDownloadThisAlbum});
            this.cxtTracks.Name = "cxtEpisodes";
            this.cxtTracks.Size = new System.Drawing.Size(192, 48);
            // 
            // itmDownloadThisTrack
            // 
            this.itmDownloadThisTrack.Name = "itmDownloadThisTrack";
            this.itmDownloadThisTrack.Size = new System.Drawing.Size(191, 22);
            this.itmDownloadThisTrack.Text = "Download This Track";
            this.itmDownloadThisTrack.Click += new System.EventHandler(this.ItmDownloadThisTrack_Click);
            // 
            // itmDownloadThisAlbum
            // 
            this.itmDownloadThisAlbum.Name = "itmDownloadThisAlbum";
            this.itmDownloadThisAlbum.Size = new System.Drawing.Size(191, 22);
            this.itmDownloadThisAlbum.Text = "Download This Album";
            this.itmDownloadThisAlbum.Click += new System.EventHandler(this.ItmDownloadThisAlbum_Click);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.GotFocus += new System.EventHandler(this.Home_Focus);
            this.Move += new System.EventHandler(this.Home_Move);
            this.cxtEpisodes.ResumeLayout(false);
            this.cxtEpisodeOptions.ResumeLayout(false);
            this.cxtLibrarySections.ResumeLayout(false);
            this.cxtMovieOptions.ResumeLayout(false);
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
            this.tabMusic.ResumeLayout(false);
            this.tlpMusic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtists)).EndInit();
            this.cxtTrackOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlbums)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTracks)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.tlpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpContentOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.cxtTracks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public BackgroundWorker wkrUpdateContentView;
        private SaveFileDialog sfdSave;
        public FolderBrowserDialog fbdDownloadPath;
        private OpenFileDialog ofdLoad;
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
        private ContextMenuStrip cxtMovieOptions;
        private ToolStripMenuItem itmContentMetadata;
        private ToolStripMenuItem itmContentDownload;
        private OpenFileDialog ofdMetadata;
        private FlatDataGridView dgvSections;
        private NotifyIcon nfyMain;
        private ContextMenuStrip cxtStreamOptions;
        private ToolStripMenuItem itmStreamInPVS;
        private ToolStripMenuItem itmStreamInVLC;
        private ToolStripMenuItem itmStreamInBrowser;
        private SaveFileDialog sfdExport;
        private GroupBox gbStreamControl;
        private Button btnStream;
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
        private ToolStripMenuItem itmHelp;
        private TableLayoutPanel tlpContentOptions;
        private TableLayoutPanel tlpContentOptionsControls;
        private ToolStripMenuItem itmDisconnect;
        private ToolStripMenuItem itmSettings;
        private ToolStrip tsMain;
        private ToolStripSeparator sepViewing;
        private ToolStripLabel lblProgress;
        private ProgressBar pbMain;
        private ToolStripLabel lblViewingValue;
        private ToolStripLabel lblViewing;
        private Timer tmrWorkerTimeout;
        private FlatDataGridView dgvLog;
        private TableLayoutPanel tlpLog;
        private TableLayoutPanel tlpMovies;
        private ToolStripSeparator sepDownloading;
        private ToolStripLabel lblDownloading;
        private ToolStripLabel lblDownloadingValue;
        private ToolStripLabel lblSpeedValue;
        private ToolStripSeparator sepSpeed;
        private ToolStripLabel lblSpeed;
        private ToolStripLabel lblEta;
        private ToolStripSeparator sepEta;
        private ToolStripLabel lblEtaValue;
        private ToolStripLabel lblBeta;
        private ToolStripSeparator sepBeta;
        private ToolStripLabel lblSidValue;
        private ToolStripLabel lblSid;
        private TabPage tabMusic;
        private TableLayoutPanel tlpMusic;
        private FlatDataGridView dgvArtists;
        private FlatDataGridView dgvAlbums;
        private FlatDataGridView dgvTracks;
        private ContextMenuStrip cxtTracks;
        private ToolStripMenuItem itmDownloadThisTrack;
        private ToolStripMenuItem itmDownloadThisAlbum;
        private ContextMenuStrip cxtTrackOptions;
        private ToolStripMenuItem itmTrackMetadata;
        private ToolStripMenuItem itmTrackDownload;
        private ToolStripMenuItem itmDGVDownloadThisTrack;
        private ToolStripMenuItem itmDGVDownloadThisAlbum;
        private ToolStripMenuItem itmCleanupAllData;
        private ToolStripMenuItem itmCheckForUpdates;
        private ToolStripMenuItem itmAbout;
        private ToolStripMenuItem itmRepo;
        private ToolStripMenuItem itmRenderKeyColumn;
        private ToolStripMenuItem itmDGVViewEpisodeDownloadLink;
        private ToolStripMenuItem itmDGVViewTrackDownloadLink;
        private ToolStripMenuItem itmDGVDownloadThisMovie;
        private ToolStripMenuItem itmDGVViewMovieDownloadLink;
        private ToolStripMenuItem itmClearMyToken;
        private ToolStripMenuItem itmOpenDataFolder;
        private ToolStripMenuItem itmCast;
        private ToolStripMenuItem itmContentCast;
        private ToolStripMenuItem itmEpisodeCast;
        private ToolStripMenuItem itmTrackCast;
        private ToolStripMenuItem itmEpisodeStream;
        private ToolStripMenuItem itmContentStream;
        private ToolStripMenuItem itmTrackStream;
        private ToolStripMenuItem itmData;
        private ToolStripMenuItem itmEpisodeSearch;
        private ToolStripMenuItem itmTrackSearch;
        private ToolStripMenuItem itmContentSearch;
        private ToolStripMenuItem itmContentMetadataView;
        private ToolStripMenuItem itmContentMetadataExport;
        private ToolStripMenuItem itmTrackMetadataView;
        private ToolStripMenuItem itmTrackMetadataExport;
        private ToolStripMenuItem itmEpisodeMetadataView;
        private ToolStripMenuItem itmEpisodeMetadataExport;
    }
}

