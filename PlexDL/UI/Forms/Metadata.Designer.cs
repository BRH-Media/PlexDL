using System.ComponentModel;
using System.Windows.Forms;
using PlexDL.Common.Components.Controls;

namespace PlexDL.UI.Forms
{
    partial class Metadata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metadata));
            PlexDL.Common.Components.Styling.BoolColour boolColour1 = new PlexDL.Common.Components.Styling.BoolColour();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flpActors = new System.Windows.Forms.FlowLayoutPanel();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.picPoster = new PlexDL.Common.Components.Controls.PreviewPictureBox();
            this.gbPlot = new System.Windows.Forms.GroupBox();
            this.pnlPlotSynopsis = new System.Windows.Forms.Panel();
            this.txtPlotSynopsis = new System.Windows.Forms.RichTextBox();
            this.gbStarring = new System.Windows.Forms.GroupBox();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.dgvAttributes = new PlexDL.Common.Components.Controls.FlatDataGridView();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStream = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPvs = new System.Windows.Forms.ToolStripMenuItem();
            this.itmBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.itmVlc = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSourceLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSourceLinkView = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSourceLinkDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.gbPlot.SuspendLayout();
            this.pnlPlotSynopsis.SuspendLayout();
            this.gbStarring.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpActors
            // 
            this.flpActors.AutoScroll = true;
            this.flpActors.BackColor = System.Drawing.Color.White;
            this.flpActors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActors.Location = new System.Drawing.Point(4, 19);
            this.flpActors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpActors.Name = "flpActors";
            this.flpActors.Size = new System.Drawing.Size(710, 284);
            this.flpActors.TabIndex = 1;
            this.tipMain.SetToolTip(this.flpActors, "Actors/Actresses in this title");
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "pmxml";
            this.sfdExport.Filter = "PXZ File|*.pxz";
            this.sfdExport.Title = "Export PlexMovie Metadata";
            // 
            // ofdImport
            // 
            this.ofdImport.Filter = "PXZ File |*.pxz";
            this.ofdImport.Title = "Import PlexMovie Metadata";
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.SystemColors.Control;
            this.tlpMain.SetColumnSpan(this.picPoster, 3);
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPoster.Location = new System.Drawing.Point(3, 2);
            this.picPoster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPoster.Name = "picPoster";
            this.picPoster.PreviewWindowEnabled = true;
            this.tlpMain.SetRowSpan(this.picPoster, 4);
            this.picPoster.Size = new System.Drawing.Size(357, 288);
            this.picPoster.TabIndex = 12;
            this.picPoster.TabStop = false;
            this.tipMain.SetToolTip(this.picPoster, "Poster");
            // 
            // gbPlot
            // 
            this.tlpMain.SetColumnSpan(this.gbPlot, 6);
            this.gbPlot.Controls.Add(this.pnlPlotSynopsis);
            this.gbPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlot.Location = new System.Drawing.Point(367, 4);
            this.gbPlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbPlot.Name = "gbPlot";
            this.gbPlot.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpMain.SetRowSpan(this.gbPlot, 3);
            this.gbPlot.Size = new System.Drawing.Size(718, 211);
            this.gbPlot.TabIndex = 20;
            this.gbPlot.TabStop = false;
            this.gbPlot.Text = "Plot";
            // 
            // pnlPlotSynopsis
            // 
            this.pnlPlotSynopsis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlotSynopsis.Controls.Add(this.txtPlotSynopsis);
            this.pnlPlotSynopsis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlotSynopsis.Location = new System.Drawing.Point(4, 19);
            this.pnlPlotSynopsis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPlotSynopsis.Name = "pnlPlotSynopsis";
            this.pnlPlotSynopsis.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPlotSynopsis.Size = new System.Drawing.Size(710, 188);
            this.pnlPlotSynopsis.TabIndex = 22;
            // 
            // txtPlotSynopsis
            // 
            this.txtPlotSynopsis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlotSynopsis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlotSynopsis.Location = new System.Drawing.Point(4, 4);
            this.txtPlotSynopsis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPlotSynopsis.Name = "txtPlotSynopsis";
            this.txtPlotSynopsis.ReadOnly = true;
            this.txtPlotSynopsis.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtPlotSynopsis.Size = new System.Drawing.Size(700, 178);
            this.txtPlotSynopsis.TabIndex = 21;
            this.txtPlotSynopsis.Text = "Unknown";
            this.txtPlotSynopsis.SelectionChanged += new System.EventHandler(this.TxtPlotSynopsis_SelectionChanged);
            // 
            // gbStarring
            // 
            this.tlpMain.SetColumnSpan(this.gbStarring, 6);
            this.gbStarring.Controls.Add(this.flpActors);
            this.gbStarring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStarring.Location = new System.Drawing.Point(367, 223);
            this.gbStarring.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbStarring.Name = "gbStarring";
            this.gbStarring.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpMain.SetRowSpan(this.gbStarring, 4);
            this.gbStarring.Size = new System.Drawing.Size(718, 307);
            this.gbStarring.TabIndex = 21;
            this.gbStarring.TabStop = false;
            this.gbStarring.Text = "Starring";
            // 
            // gbAttributes
            // 
            this.tlpMain.SetColumnSpan(this.gbAttributes, 3);
            this.gbAttributes.Controls.Add(this.dgvAttributes);
            this.gbAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAttributes.Location = new System.Drawing.Point(4, 296);
            this.gbAttributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpMain.SetRowSpan(this.gbAttributes, 3);
            this.gbAttributes.Size = new System.Drawing.Size(355, 234);
            this.gbAttributes.TabIndex = 22;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Content Attributes";
            // 
            // dgvAttributes
            // 
            this.dgvAttributes.AllowUserToAddRows = false;
            this.dgvAttributes.AllowUserToDeleteRows = false;
            this.dgvAttributes.AllowUserToOrderColumns = true;
            this.dgvAttributes.AllowUserToResizeRows = false;
            this.dgvAttributes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttributes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour1.BoolColouringEnabled = false;
            boolColour1.ColouringMode = PlexDL.Common.Components.Styling.BoolColourMode.BackColour;
            boolColour1.FalseColour = System.Drawing.Color.DarkRed;
            boolColour1.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour1.RelevantColumns")));
            boolColour1.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvAttributes.BoolColouringScheme = boolColour1;
            this.dgvAttributes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAttributes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvAttributes.CellContentClickMessage = false;
            this.dgvAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAttributes.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttributes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvAttributes.IsContentTable = false;
            this.dgvAttributes.Location = new System.Drawing.Point(4, 19);
            this.dgvAttributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAttributes.MultiSelect = false;
            this.dgvAttributes.Name = "dgvAttributes";
            this.dgvAttributes.RowHeadersVisible = false;
            this.dgvAttributes.RowHeadersWidth = 51;
            this.dgvAttributes.RowsEmptyText = "No Attributes";
            this.dgvAttributes.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttributes.Size = new System.Drawing.Size(347, 211);
            this.dgvAttributes.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 9;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tlpMain.Controls.Add(this.picPoster, 0, 0);
            this.tlpMain.Controls.Add(this.gbAttributes, 0, 4);
            this.tlpMain.Controls.Add(this.gbPlot, 3, 0);
            this.tlpMain.Controls.Add(this.gbStarring, 3, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 28);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.Size = new System.Drawing.Size(1089, 534);
            this.tlpMain.TabIndex = 23;
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmStream,
            this.itmExit});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1089, 28);
            this.menuMain.TabIndex = 24;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmImport,
            this.itmExport,
            this.itmDataExplorer});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(46, 24);
            this.itmFile.Text = "File";
            // 
            // itmImport
            // 
            this.itmImport.Name = "itmImport";
            this.itmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmImport.Size = new System.Drawing.Size(190, 26);
            this.itmImport.Text = "Import";
            this.itmImport.Click += new System.EventHandler(this.ItmImport_Click);
            // 
            // itmExport
            // 
            this.itmExport.Name = "itmExport";
            this.itmExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmExport.Size = new System.Drawing.Size(190, 26);
            this.itmExport.Text = "Export";
            this.itmExport.Click += new System.EventHandler(this.ItmExport_Click);
            // 
            // itmDataExplorer
            // 
            this.itmDataExplorer.Name = "itmDataExplorer";
            this.itmDataExplorer.Size = new System.Drawing.Size(190, 26);
            this.itmDataExplorer.Text = "Data Explorer";
            this.itmDataExplorer.Click += new System.EventHandler(this.ItmDataExplorer_Click);
            // 
            // itmStream
            // 
            this.itmStream.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmPvs,
            this.itmBrowser,
            this.itmVlc,
            this.itmSourceLink});
            this.itmStream.Enabled = false;
            this.itmStream.Name = "itmStream";
            this.itmStream.Size = new System.Drawing.Size(70, 24);
            this.itmStream.Text = "Stream";
            // 
            // itmPvs
            // 
            this.itmPvs.Name = "itmPvs";
            this.itmPvs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.itmPvs.Size = new System.Drawing.Size(196, 26);
            this.itmPvs.Text = "PVS";
            this.itmPvs.Click += new System.EventHandler(this.ItmPvs_Click);
            // 
            // itmBrowser
            // 
            this.itmBrowser.Name = "itmBrowser";
            this.itmBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.itmBrowser.Size = new System.Drawing.Size(196, 26);
            this.itmBrowser.Text = "Browser";
            this.itmBrowser.Click += new System.EventHandler(this.ItmBrowser_Click);
            // 
            // itmVlc
            // 
            this.itmVlc.Name = "itmVlc";
            this.itmVlc.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.itmVlc.Size = new System.Drawing.Size(196, 26);
            this.itmVlc.Text = "VLC";
            this.itmVlc.Click += new System.EventHandler(this.ItmVlc_Click);
            // 
            // itmSourceLink
            // 
            this.itmSourceLink.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSourceLinkView,
            this.itmSourceLinkDownload});
            this.itmSourceLink.Name = "itmSourceLink";
            this.itmSourceLink.Size = new System.Drawing.Size(196, 26);
            this.itmSourceLink.Text = "Source Link";
            // 
            // itmSourceLinkView
            // 
            this.itmSourceLinkView.Name = "itmSourceLinkView";
            this.itmSourceLinkView.Size = new System.Drawing.Size(161, 26);
            this.itmSourceLinkView.Text = "View";
            this.itmSourceLinkView.Click += new System.EventHandler(this.ItmSourceLinkView_Click);
            // 
            // itmSourceLinkDownload
            // 
            this.itmSourceLinkDownload.Name = "itmSourceLinkDownload";
            this.itmSourceLinkDownload.Size = new System.Drawing.Size(161, 26);
            this.itmSourceLinkDownload.Text = "Download";
            this.itmSourceLinkDownload.Click += new System.EventHandler(this.ItmSourceLinkDownload_Click);
            // 
            // itmExit
            // 
            this.itmExit.Name = "itmExit";
            this.itmExit.Size = new System.Drawing.Size(47, 24);
            this.itmExit.Text = "Exit";
            this.itmExit.Click += new System.EventHandler(this.ItmExit_Click);
            // 
            // Metadata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1089, 562);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Metadata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metadata";
            this.Load += new System.EventHandler(this.Metadata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.gbPlot.ResumeLayout(false);
            this.pnlPlotSynopsis.ResumeLayout(false);
            this.gbStarring.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FlowLayoutPanel flpActors;
        private SaveFileDialog sfdExport;
        private OpenFileDialog ofdImport;
        private ToolTip tipMain;
        private GroupBox gbPlot;
        private RichTextBox txtPlotSynopsis;
        private GroupBox gbStarring;
        private GroupBox gbAttributes;
        private TableLayoutPanel tlpMain;
        private MenuStrip menuMain;
        private ToolStripMenuItem itmFile;
        private ToolStripMenuItem itmImport;
        private ToolStripMenuItem itmExport;
        private ToolStripMenuItem itmStream;
        private ToolStripMenuItem itmPvs;
        private ToolStripMenuItem itmBrowser;
        private ToolStripMenuItem itmVlc;
        private ToolStripMenuItem itmExit;
        private Panel pnlPlotSynopsis;
        private FlatDataGridView dgvAttributes;
        private ToolStripMenuItem itmSourceLink;
        private ToolStripMenuItem itmDataExplorer;
        private PreviewPictureBox picPoster;
        private ToolStripMenuItem itmSourceLinkView;
        private ToolStripMenuItem itmSourceLinkDownload;
    }
}