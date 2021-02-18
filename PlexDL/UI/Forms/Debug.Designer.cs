using System.Windows.Forms;
using PlexDL.Common.Components.Controls;
using PlexDL.Common.Components.Styling;

namespace PlexDL.UI.Forms
{
    partial class Debug
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
            PlexDL.Common.Components.Styling.BoolColour boolColour1 = new PlexDL.Common.Components.Styling.BoolColour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmrAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.tlpDebug = new System.Windows.Forms.TableLayoutPanel();
            this.gbGlobalValues = new System.Windows.Forms.GroupBox();
            this.tlpGlobalValues = new System.Windows.Forms.TableLayoutPanel();
            this.gbDataExport = new System.Windows.Forms.GroupBox();
            this.tlpDataExport = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportSections = new System.Windows.Forms.Button();
            this.btnExportTracks = new System.Windows.Forms.Button();
            this.btnExportTitles = new System.Windows.Forms.Button();
            this.btnExportAlbums = new System.Windows.Forms.Button();
            this.btnExportFiltered = new System.Windows.Forms.Button();
            this.btnExportEpisodes = new System.Windows.Forms.Button();
            this.btnExportSeasons = new System.Windows.Forms.Button();
            this.gbExportMode = new System.Windows.Forms.GroupBox();
            this.tlpExportFormat = new System.Windows.Forms.TableLayoutPanel();
            this.radModeView = new System.Windows.Forms.RadioButton();
            this.radModeTable = new System.Windows.Forms.RadioButton();
            this.gbExportFormat = new System.Windows.Forms.GroupBox();
            this.cbxExportFormat = new System.Windows.Forms.ComboBox();
            this.gbGlobalFlags = new System.Windows.Forms.GroupBox();
            this.tlpGlobalFlags = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportGlobalFlags = new System.Windows.Forms.Button();
            this.dgvGlobalFlags = new PlexDL.Common.Components.Controls.FlatDataGridView();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPollRate = new System.Windows.Forms.TableLayoutPanel();
            this.lblPollRate = new System.Windows.Forms.Label();
            this.numPollRateValue = new System.Windows.Forms.NumericUpDown();
            this.tlpRefreshCount = new System.Windows.Forms.TableLayoutPanel();
            this.lblRefreshCountValue = new System.Windows.Forms.Label();
            this.lblRefreshCount = new System.Windows.Forms.Label();
            this.btnResetRefreshCounter = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.tlpDebug.SuspendLayout();
            this.gbGlobalValues.SuspendLayout();
            this.tlpGlobalValues.SuspendLayout();
            this.gbDataExport.SuspendLayout();
            this.tlpDataExport.SuspendLayout();
            this.gbExportMode.SuspendLayout();
            this.tlpExportFormat.SuspendLayout();
            this.gbExportFormat.SuspendLayout();
            this.gbGlobalFlags.SuspendLayout();
            this.tlpGlobalFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).BeginInit();
            this.tlpControls.SuspendLayout();
            this.tlpPollRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPollRateValue)).BeginInit();
            this.tlpRefreshCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrAutoRefresh
            // 
            this.tmrAutoRefresh.Tick += new System.EventHandler(this.TmrAutoRefresh_Tick);
            // 
            // tlpDebug
            // 
            this.tlpDebug.AutoSize = true;
            this.tlpDebug.ColumnCount = 2;
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDebug.Controls.Add(this.gbGlobalValues, 1, 0);
            this.tlpDebug.Controls.Add(this.gbGlobalFlags, 0, 0);
            this.tlpDebug.Controls.Add(this.tlpControls, 0, 9);
            this.tlpDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDebug.Location = new System.Drawing.Point(0, 0);
            this.tlpDebug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpDebug.Name = "tlpDebug";
            this.tlpDebug.RowCount = 10;
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDebug.Size = new System.Drawing.Size(600, 549);
            this.tlpDebug.TabIndex = 0;
            // 
            // gbGlobalValues
            // 
            this.gbGlobalValues.Controls.Add(this.tlpGlobalValues);
            this.gbGlobalValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGlobalValues.Location = new System.Drawing.Point(304, 4);
            this.gbGlobalValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbGlobalValues.Name = "gbGlobalValues";
            this.gbGlobalValues.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpDebug.SetRowSpan(this.gbGlobalValues, 9);
            this.gbGlobalValues.Size = new System.Drawing.Size(292, 439);
            this.gbGlobalValues.TabIndex = 1;
            this.gbGlobalValues.TabStop = false;
            this.gbGlobalValues.Text = "Global Values";
            // 
            // tlpGlobalValues
            // 
            this.tlpGlobalValues.ColumnCount = 1;
            this.tlpGlobalValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpGlobalValues.Controls.Add(this.gbDataExport, 0, 0);
            this.tlpGlobalValues.Controls.Add(this.gbExportMode, 0, 2);
            this.tlpGlobalValues.Controls.Add(this.gbExportFormat, 0, 1);
            this.tlpGlobalValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGlobalValues.Location = new System.Drawing.Point(4, 19);
            this.tlpGlobalValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpGlobalValues.Name = "tlpGlobalValues";
            this.tlpGlobalValues.RowCount = 3;
            this.tlpGlobalValues.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGlobalValues.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGlobalValues.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGlobalValues.Size = new System.Drawing.Size(284, 416);
            this.tlpGlobalValues.TabIndex = 3;
            // 
            // gbDataExport
            // 
            this.gbDataExport.Controls.Add(this.tlpDataExport);
            this.gbDataExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDataExport.Location = new System.Drawing.Point(4, 4);
            this.gbDataExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDataExport.Name = "gbDataExport";
            this.gbDataExport.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDataExport.Size = new System.Drawing.Size(276, 274);
            this.gbDataExport.TabIndex = 0;
            this.gbDataExport.TabStop = false;
            this.gbDataExport.Text = "Data Export";
            // 
            // tlpDataExport
            // 
            this.tlpDataExport.ColumnCount = 1;
            this.tlpDataExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDataExport.Controls.Add(this.btnExportSections, 0, 0);
            this.tlpDataExport.Controls.Add(this.btnExportTracks, 0, 6);
            this.tlpDataExport.Controls.Add(this.btnExportTitles, 0, 1);
            this.tlpDataExport.Controls.Add(this.btnExportAlbums, 0, 5);
            this.tlpDataExport.Controls.Add(this.btnExportFiltered, 0, 2);
            this.tlpDataExport.Controls.Add(this.btnExportEpisodes, 0, 4);
            this.tlpDataExport.Controls.Add(this.btnExportSeasons, 0, 3);
            this.tlpDataExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDataExport.Location = new System.Drawing.Point(4, 19);
            this.tlpDataExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpDataExport.Name = "tlpDataExport";
            this.tlpDataExport.RowCount = 7;
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDataExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDataExport.Size = new System.Drawing.Size(268, 251);
            this.tlpDataExport.TabIndex = 7;
            // 
            // btnExportSections
            // 
            this.btnExportSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportSections.Location = new System.Drawing.Point(4, 4);
            this.btnExportSections.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportSections.Name = "btnExportSections";
            this.btnExportSections.Size = new System.Drawing.Size(260, 28);
            this.btnExportSections.TabIndex = 0;
            this.btnExportSections.Text = "Sections";
            this.btnExportSections.UseVisualStyleBackColor = true;
            this.btnExportSections.Click += new System.EventHandler(this.BtnExportSections_Click);
            // 
            // btnExportTracks
            // 
            this.btnExportTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportTracks.Location = new System.Drawing.Point(4, 220);
            this.btnExportTracks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportTracks.Name = "btnExportTracks";
            this.btnExportTracks.Size = new System.Drawing.Size(260, 30);
            this.btnExportTracks.TabIndex = 6;
            this.btnExportTracks.Text = "Music Tracks";
            this.btnExportTracks.UseVisualStyleBackColor = true;
            this.btnExportTracks.Click += new System.EventHandler(this.BtnExportTracks_Click);
            // 
            // btnExportTitles
            // 
            this.btnExportTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportTitles.Location = new System.Drawing.Point(4, 40);
            this.btnExportTitles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportTitles.Name = "btnExportTitles";
            this.btnExportTitles.Size = new System.Drawing.Size(260, 28);
            this.btnExportTitles.TabIndex = 1;
            this.btnExportTitles.Text = "Titles";
            this.btnExportTitles.UseVisualStyleBackColor = true;
            this.btnExportTitles.Click += new System.EventHandler(this.BtnExportTitles_Click);
            // 
            // btnExportAlbums
            // 
            this.btnExportAlbums.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportAlbums.Location = new System.Drawing.Point(4, 184);
            this.btnExportAlbums.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportAlbums.Name = "btnExportAlbums";
            this.btnExportAlbums.Size = new System.Drawing.Size(260, 28);
            this.btnExportAlbums.TabIndex = 5;
            this.btnExportAlbums.Text = "Music Albums";
            this.btnExportAlbums.UseVisualStyleBackColor = true;
            this.btnExportAlbums.Click += new System.EventHandler(this.BtnExportAlbums_Click);
            // 
            // btnExportFiltered
            // 
            this.btnExportFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportFiltered.Location = new System.Drawing.Point(4, 76);
            this.btnExportFiltered.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportFiltered.Name = "btnExportFiltered";
            this.btnExportFiltered.Size = new System.Drawing.Size(260, 28);
            this.btnExportFiltered.TabIndex = 2;
            this.btnExportFiltered.Text = "Filtered";
            this.btnExportFiltered.UseVisualStyleBackColor = true;
            this.btnExportFiltered.Click += new System.EventHandler(this.BtnExportFiltered_Click);
            // 
            // btnExportEpisodes
            // 
            this.btnExportEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportEpisodes.Location = new System.Drawing.Point(4, 148);
            this.btnExportEpisodes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportEpisodes.Name = "btnExportEpisodes";
            this.btnExportEpisodes.Size = new System.Drawing.Size(260, 28);
            this.btnExportEpisodes.TabIndex = 4;
            this.btnExportEpisodes.Text = "TV Episodes";
            this.btnExportEpisodes.UseVisualStyleBackColor = true;
            this.btnExportEpisodes.Click += new System.EventHandler(this.BtnExportEpisodes_Click);
            // 
            // btnExportSeasons
            // 
            this.btnExportSeasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportSeasons.Location = new System.Drawing.Point(4, 112);
            this.btnExportSeasons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportSeasons.Name = "btnExportSeasons";
            this.btnExportSeasons.Size = new System.Drawing.Size(260, 28);
            this.btnExportSeasons.TabIndex = 3;
            this.btnExportSeasons.Text = "TV Seasons";
            this.btnExportSeasons.UseVisualStyleBackColor = true;
            this.btnExportSeasons.Click += new System.EventHandler(this.BtnExportSeasons_Click);
            // 
            // gbExportMode
            // 
            this.gbExportMode.Controls.Add(this.tlpExportFormat);
            this.gbExportMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExportMode.Location = new System.Drawing.Point(4, 354);
            this.gbExportMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbExportMode.Name = "gbExportMode";
            this.gbExportMode.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbExportMode.Size = new System.Drawing.Size(276, 60);
            this.gbExportMode.TabIndex = 2;
            this.gbExportMode.TabStop = false;
            this.gbExportMode.Text = "Export Mode";
            // 
            // tlpExportFormat
            // 
            this.tlpExportFormat.ColumnCount = 2;
            this.tlpExportFormat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpExportFormat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpExportFormat.Controls.Add(this.radModeView, 1, 0);
            this.tlpExportFormat.Controls.Add(this.radModeTable, 0, 0);
            this.tlpExportFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpExportFormat.Location = new System.Drawing.Point(4, 19);
            this.tlpExportFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpExportFormat.Name = "tlpExportFormat";
            this.tlpExportFormat.RowCount = 1;
            this.tlpExportFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpExportFormat.Size = new System.Drawing.Size(268, 37);
            this.tlpExportFormat.TabIndex = 8;
            // 
            // radModeView
            // 
            this.radModeView.AutoSize = true;
            this.radModeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radModeView.Location = new System.Drawing.Point(77, 4);
            this.radModeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radModeView.Name = "radModeView";
            this.radModeView.Size = new System.Drawing.Size(187, 29);
            this.radModeView.TabIndex = 0;
            this.radModeView.Text = "View";
            this.radModeView.UseVisualStyleBackColor = true;
            // 
            // radModeTable
            // 
            this.radModeTable.AutoSize = true;
            this.radModeTable.Checked = true;
            this.radModeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radModeTable.Location = new System.Drawing.Point(4, 4);
            this.radModeTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radModeTable.Name = "radModeTable";
            this.radModeTable.Size = new System.Drawing.Size(65, 29);
            this.radModeTable.TabIndex = 1;
            this.radModeTable.TabStop = true;
            this.radModeTable.Text = "Table";
            this.radModeTable.UseVisualStyleBackColor = true;
            // 
            // gbExportFormat
            // 
            this.gbExportFormat.Controls.Add(this.cbxExportFormat);
            this.gbExportFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExportFormat.Location = new System.Drawing.Point(4, 286);
            this.gbExportFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbExportFormat.Name = "gbExportFormat";
            this.gbExportFormat.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbExportFormat.Size = new System.Drawing.Size(276, 60);
            this.gbExportFormat.TabIndex = 1;
            this.gbExportFormat.TabStop = false;
            this.gbExportFormat.Text = "Export Format";
            // 
            // cbxExportFormat
            // 
            this.cbxExportFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExportFormat.FormattingEnabled = true;
            this.cbxExportFormat.Items.AddRange(new object[] {
            "CSV",
            "JSON",
            "XML",
            "LOGDEL"});
            this.cbxExportFormat.Location = new System.Drawing.Point(4, 19);
            this.cbxExportFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxExportFormat.Name = "cbxExportFormat";
            this.cbxExportFormat.Size = new System.Drawing.Size(268, 24);
            this.cbxExportFormat.TabIndex = 0;
            // 
            // gbGlobalFlags
            // 
            this.gbGlobalFlags.Controls.Add(this.tlpGlobalFlags);
            this.gbGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGlobalFlags.Location = new System.Drawing.Point(4, 4);
            this.gbGlobalFlags.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbGlobalFlags.Name = "gbGlobalFlags";
            this.gbGlobalFlags.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpDebug.SetRowSpan(this.gbGlobalFlags, 9);
            this.gbGlobalFlags.Size = new System.Drawing.Size(292, 439);
            this.gbGlobalFlags.TabIndex = 0;
            this.gbGlobalFlags.TabStop = false;
            this.gbGlobalFlags.Text = "Global Flags";
            // 
            // tlpGlobalFlags
            // 
            this.tlpGlobalFlags.ColumnCount = 1;
            this.tlpGlobalFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGlobalFlags.Controls.Add(this.btnExportGlobalFlags, 0, 1);
            this.tlpGlobalFlags.Controls.Add(this.dgvGlobalFlags, 0, 0);
            this.tlpGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGlobalFlags.Location = new System.Drawing.Point(4, 19);
            this.tlpGlobalFlags.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpGlobalFlags.Name = "tlpGlobalFlags";
            this.tlpGlobalFlags.RowCount = 2;
            this.tlpGlobalFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGlobalFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpGlobalFlags.Size = new System.Drawing.Size(284, 416);
            this.tlpGlobalFlags.TabIndex = 0;
            // 
            // btnExportGlobalFlags
            // 
            this.btnExportGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportGlobalFlags.Location = new System.Drawing.Point(4, 382);
            this.btnExportGlobalFlags.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportGlobalFlags.Name = "btnExportGlobalFlags";
            this.btnExportGlobalFlags.Size = new System.Drawing.Size(276, 30);
            this.btnExportGlobalFlags.TabIndex = 7;
            this.btnExportGlobalFlags.Text = "Export Flags";
            this.btnExportGlobalFlags.UseVisualStyleBackColor = true;
            this.btnExportGlobalFlags.Click += new System.EventHandler(this.BtnExportGlobalFlags_Click);
            // 
            // dgvGlobalFlags
            // 
            this.dgvGlobalFlags.AllowUserToAddRows = false;
            this.dgvGlobalFlags.AllowUserToDeleteRows = false;
            this.dgvGlobalFlags.AllowUserToOrderColumns = true;
            this.dgvGlobalFlags.AllowUserToResizeRows = false;
            this.dgvGlobalFlags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGlobalFlags.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour1.BoolColouringEnabled = true;
            boolColour1.ColouringMode = PlexDL.Common.Components.Styling.BoolColourMode.BackColour;
            boolColour1.FalseColour = System.Drawing.Color.Red;
            boolColour1.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour1.RelevantColumns")));
            boolColour1.TrueColour = System.Drawing.Color.Green;
            this.dgvGlobalFlags.BoolColouringScheme = boolColour1;
            this.dgvGlobalFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGlobalFlags.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvGlobalFlags.CellContentClickMessage = false;
            this.dgvGlobalFlags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlobalFlags.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGlobalFlags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGlobalFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvGlobalFlags.IsContentTable = false;
            this.dgvGlobalFlags.Location = new System.Drawing.Point(4, 4);
            this.dgvGlobalFlags.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvGlobalFlags.MultiSelect = false;
            this.dgvGlobalFlags.Name = "dgvGlobalFlags";
            this.dgvGlobalFlags.RowHeadersVisible = false;
            this.dgvGlobalFlags.RowHeadersWidth = 51;
            this.dgvGlobalFlags.RowsEmptyText = "No Flags";
            this.dgvGlobalFlags.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvGlobalFlags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGlobalFlags.Size = new System.Drawing.Size(276, 370);
            this.dgvGlobalFlags.TabIndex = 1;
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 4;
            this.tlpDebug.SetColumnSpan(this.tlpControls, 2);
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.Controls.Add(this.tlpPollRate, 2, 0);
            this.tlpControls.Controls.Add(this.tlpRefreshCount, 0, 0);
            this.tlpControls.Controls.Add(this.btnTimer, 0, 1);
            this.tlpControls.Controls.Add(this.btnRefresh, 0, 1);
            this.tlpControls.Controls.Add(this.btnCancel, 3, 1);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(4, 451);
            this.tlpControls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 2;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.Size = new System.Drawing.Size(592, 95);
            this.tlpControls.TabIndex = 2;
            // 
            // tlpPollRate
            // 
            this.tlpPollRate.ColumnCount = 2;
            this.tlpControls.SetColumnSpan(this.tlpPollRate, 2);
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPollRate.Controls.Add(this.lblPollRate, 0, 0);
            this.tlpPollRate.Controls.Add(this.numPollRateValue, 1, 0);
            this.tlpPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPollRate.Location = new System.Drawing.Point(300, 4);
            this.tlpPollRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpPollRate.Name = "tlpPollRate";
            this.tlpPollRate.RowCount = 1;
            this.tlpPollRate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPollRate.Size = new System.Drawing.Size(288, 51);
            this.tlpPollRate.TabIndex = 19;
            // 
            // lblPollRate
            // 
            this.lblPollRate.AutoSize = true;
            this.lblPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPollRate.Location = new System.Drawing.Point(4, 0);
            this.lblPollRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPollRate.Name = "lblPollRate";
            this.lblPollRate.Size = new System.Drawing.Size(101, 51);
            this.lblPollRate.TabIndex = 0;
            this.lblPollRate.Text = "Poll Rate (ms):";
            this.lblPollRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numPollRateValue
            // 
            this.numPollRateValue.AutoSize = true;
            this.numPollRateValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPollRateValue.Location = new System.Drawing.Point(113, 4);
            this.numPollRateValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numPollRateValue.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numPollRateValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPollRateValue.Name = "numPollRateValue";
            this.numPollRateValue.Size = new System.Drawing.Size(171, 22);
            this.numPollRateValue.TabIndex = 1;
            this.numPollRateValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numPollRateValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPollRateValue.ValueChanged += new System.EventHandler(this.NumPollRateValue_ValueChanged);
            // 
            // tlpRefreshCount
            // 
            this.tlpRefreshCount.ColumnCount = 3;
            this.tlpControls.SetColumnSpan(this.tlpRefreshCount, 2);
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCountValue, 1, 0);
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCount, 0, 0);
            this.tlpRefreshCount.Controls.Add(this.btnResetRefreshCounter, 2, 0);
            this.tlpRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRefreshCount.Location = new System.Drawing.Point(4, 4);
            this.tlpRefreshCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpRefreshCount.Name = "tlpRefreshCount";
            this.tlpRefreshCount.RowCount = 1;
            this.tlpRefreshCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRefreshCount.Size = new System.Drawing.Size(288, 51);
            this.tlpRefreshCount.TabIndex = 6;
            // 
            // lblRefreshCountValue
            // 
            this.lblRefreshCountValue.AutoSize = true;
            this.lblRefreshCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCountValue.Location = new System.Drawing.Point(223, 0);
            this.lblRefreshCountValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRefreshCountValue.Name = "lblRefreshCountValue";
            this.lblRefreshCountValue.Size = new System.Drawing.Size(16, 52);
            this.lblRefreshCountValue.TabIndex = 1;
            this.lblRefreshCountValue.Text = "0";
            this.lblRefreshCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefreshCount
            // 
            this.lblRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCount.Location = new System.Drawing.Point(4, 0);
            this.lblRefreshCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRefreshCount.Name = "lblRefreshCount";
            this.lblRefreshCount.Size = new System.Drawing.Size(211, 52);
            this.lblRefreshCount.TabIndex = 0;
            this.lblRefreshCount.Text = "Refresh Count:";
            this.lblRefreshCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResetRefreshCounter
            // 
            this.btnResetRefreshCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetRefreshCounter.Location = new System.Drawing.Point(247, 4);
            this.btnResetRefreshCounter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetRefreshCounter.Name = "btnResetRefreshCounter";
            this.btnResetRefreshCounter.Size = new System.Drawing.Size(37, 44);
            this.btnResetRefreshCounter.TabIndex = 2;
            this.btnResetRefreshCounter.Text = "C";
            this.btnResetRefreshCounter.UseVisualStyleBackColor = true;
            this.btnResetRefreshCounter.Click += new System.EventHandler(this.BtnResetRefreshCounter_Click);
            // 
            // btnTimer
            // 
            this.tlpControls.SetColumnSpan(this.btnTimer, 2);
            this.btnTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimer.Location = new System.Drawing.Point(152, 63);
            this.btnTimer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(288, 28);
            this.btnTimer.TabIndex = 1;
            this.btnTimer.Text = "Auto-refresh On";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.BtnTimer_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.Location = new System.Drawing.Point(4, 63);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 28);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(448, 63);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.Title = "Export";
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 549);
            this.Controls.Add(this.tlpDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Debug";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Debug_FormClosing);
            this.Load += new System.EventHandler(this.Debug_Load);
            this.tlpDebug.ResumeLayout(false);
            this.gbGlobalValues.ResumeLayout(false);
            this.tlpGlobalValues.ResumeLayout(false);
            this.gbDataExport.ResumeLayout(false);
            this.tlpDataExport.ResumeLayout(false);
            this.gbExportMode.ResumeLayout(false);
            this.tlpExportFormat.ResumeLayout(false);
            this.tlpExportFormat.PerformLayout();
            this.gbExportFormat.ResumeLayout(false);
            this.gbGlobalFlags.ResumeLayout(false);
            this.tlpGlobalFlags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).EndInit();
            this.tlpControls.ResumeLayout(false);
            this.tlpPollRate.ResumeLayout(false);
            this.tlpPollRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPollRateValue)).EndInit();
            this.tlpRefreshCount.ResumeLayout(false);
            this.tlpRefreshCount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrAutoRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpDebug;
        private System.Windows.Forms.GroupBox gbGlobalFlags;
        private System.Windows.Forms.GroupBox gbGlobalValues;
        private System.Windows.Forms.Button btnExportTitles;
        private System.Windows.Forms.Button btnExportSections;
        private System.Windows.Forms.Button btnExportSeasons;
        private System.Windows.Forms.Button btnExportEpisodes;
        private System.Windows.Forms.GroupBox gbDataExport;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.Button btnExportFiltered;
        private System.Windows.Forms.RadioButton radModeView;
        private System.Windows.Forms.RadioButton radModeTable;
        private System.Windows.Forms.GroupBox gbExportMode;
        private System.Windows.Forms.GroupBox gbExportFormat;
        private System.Windows.Forms.ComboBox cbxExportFormat;
        private System.Windows.Forms.Button btnExportTracks;
        private System.Windows.Forms.Button btnExportAlbums;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.TableLayoutPanel tlpPollRate;
        private System.Windows.Forms.Label lblPollRate;
        private System.Windows.Forms.NumericUpDown numPollRateValue;
        private System.Windows.Forms.TableLayoutPanel tlpRefreshCount;
        private System.Windows.Forms.Label lblRefreshCountValue;
        private System.Windows.Forms.Label lblRefreshCount;
        private System.Windows.Forms.Button btnResetRefreshCounter;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tlpDataExport;
        private System.Windows.Forms.TableLayoutPanel tlpExportFormat;
        private System.Windows.Forms.TableLayoutPanel tlpGlobalValues;
        private TableLayoutPanel tlpGlobalFlags;
        private FlatDataGridView dgvGlobalFlags;
        private Button btnExportGlobalFlags;
    }
}