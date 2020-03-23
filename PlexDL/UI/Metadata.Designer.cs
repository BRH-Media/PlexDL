namespace PlexDL.UI
{
    partial class Metadata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metadata));
            this.flpActors = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblSizeValue = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.lblRuntimeValue = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblGenreValue = new System.Windows.Forms.Label();
            this.lblResolutionValue = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.lblContainerValue = new System.Windows.Forms.Label();
            this.lblContainer = new System.Windows.Forms.Label();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.gbPlot = new System.Windows.Forms.GroupBox();
            this.txtPlotSynopsis = new System.Windows.Forms.TextBox();
            this.gbStarring = new System.Windows.Forms.GroupBox();
            this.gbMetadata = new System.Windows.Forms.GroupBox();
            this.tlpMetadata = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStream = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPvs = new System.Windows.Forms.ToolStripMenuItem();
            this.itmBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.itmVlc = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.gbPlot.SuspendLayout();
            this.gbStarring.SuspendLayout();
            this.gbMetadata.SuspendLayout();
            this.tlpMetadata.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpActors
            // 
            this.flpActors.AutoScroll = true;
            this.flpActors.BackColor = System.Drawing.Color.White;
            this.flpActors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActors.Location = new System.Drawing.Point(3, 16);
            this.flpActors.Margin = new System.Windows.Forms.Padding(2);
            this.flpActors.Name = "flpActors";
            this.flpActors.Size = new System.Drawing.Size(535, 231);
            this.flpActors.TabIndex = 1;
            this.tipMain.SetToolTip(this.flpActors, "Actors/Actresses in this title");
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSize.Location = new System.Drawing.Point(2, 0);
            this.lblSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(87, 34);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Size:";
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSizeValue
            // 
            this.lblSizeValue.AutoSize = true;
            this.lblSizeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeValue.Location = new System.Drawing.Point(93, 0);
            this.lblSizeValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSizeValue.Name = "lblSizeValue";
            this.lblSizeValue.Size = new System.Drawing.Size(163, 34);
            this.lblSizeValue.TabIndex = 5;
            this.lblSizeValue.Text = "Unknown";
            this.lblSizeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tipMain.SetToolTip(this.lblSizeValue, "Size");
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRuntime.Location = new System.Drawing.Point(2, 34);
            this.lblRuntime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(87, 34);
            this.lblRuntime.TabIndex = 6;
            this.lblRuntime.Text = "Runtime:";
            this.lblRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRuntimeValue
            // 
            this.lblRuntimeValue.AutoSize = true;
            this.lblRuntimeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRuntimeValue.Location = new System.Drawing.Point(93, 34);
            this.lblRuntimeValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRuntimeValue.Name = "lblRuntimeValue";
            this.lblRuntimeValue.Size = new System.Drawing.Size(163, 34);
            this.lblRuntimeValue.TabIndex = 7;
            this.lblRuntimeValue.Text = "Unknown";
            this.lblRuntimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tipMain.SetToolTip(this.lblRuntimeValue, "Runtime");
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGenre.Location = new System.Drawing.Point(2, 136);
            this.lblGenre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(87, 36);
            this.lblGenre.TabIndex = 8;
            this.lblGenre.Text = "Genre:";
            this.lblGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGenreValue
            // 
            this.lblGenreValue.AutoSize = true;
            this.lblGenreValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGenreValue.Location = new System.Drawing.Point(93, 136);
            this.lblGenreValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGenreValue.Name = "lblGenreValue";
            this.lblGenreValue.Size = new System.Drawing.Size(163, 36);
            this.lblGenreValue.TabIndex = 9;
            this.lblGenreValue.Text = "Unknown";
            this.lblGenreValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tipMain.SetToolTip(this.lblGenreValue, "Genre");
            // 
            // lblResolutionValue
            // 
            this.lblResolutionValue.AutoSize = true;
            this.lblResolutionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResolutionValue.Location = new System.Drawing.Point(93, 68);
            this.lblResolutionValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResolutionValue.Name = "lblResolutionValue";
            this.lblResolutionValue.Size = new System.Drawing.Size(163, 34);
            this.lblResolutionValue.TabIndex = 11;
            this.lblResolutionValue.Text = "Unknown";
            this.lblResolutionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tipMain.SetToolTip(this.lblResolutionValue, "Resolution");
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResolution.Location = new System.Drawing.Point(2, 68);
            this.lblResolution.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(87, 34);
            this.lblResolution.TabIndex = 10;
            this.lblResolution.Text = "Resolution:";
            this.lblResolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "pmxml";
            this.sfdExport.Filter = "PlexMovie XML|*.pmxml";
            this.sfdExport.Title = "Export PlexMovie Metadata";
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.SystemColors.Control;
            this.picPoster.BackgroundImage = global::PlexDL.Properties.Resources.image_not_available_png_8;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tlpMain.SetColumnSpan(this.picPoster, 3);
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPoster.Location = new System.Drawing.Point(2, 2);
            this.picPoster.Margin = new System.Windows.Forms.Padding(2);
            this.picPoster.Name = "picPoster";
            this.tlpMain.SetRowSpan(this.picPoster, 4);
            this.picPoster.Size = new System.Drawing.Size(266, 232);
            this.picPoster.TabIndex = 12;
            this.picPoster.TabStop = false;
            this.tipMain.SetToolTip(this.picPoster, "Poster");
            // 
            // ofdImport
            // 
            this.ofdImport.Filter = "PlexMovie XML|*.pmxml";
            this.ofdImport.Title = "Import PlexMovie Metadata";
            // 
            // lblContainerValue
            // 
            this.lblContainerValue.AutoSize = true;
            this.lblContainerValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContainerValue.Location = new System.Drawing.Point(93, 102);
            this.lblContainerValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblContainerValue.Name = "lblContainerValue";
            this.lblContainerValue.Size = new System.Drawing.Size(163, 34);
            this.lblContainerValue.TabIndex = 16;
            this.lblContainerValue.Text = "Unknown";
            this.lblContainerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tipMain.SetToolTip(this.lblContainerValue, "Container");
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContainer.Location = new System.Drawing.Point(2, 102);
            this.lblContainer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(87, 34);
            this.lblContainer.TabIndex = 15;
            this.lblContainer.Text = "Container:";
            this.lblContainer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbPlot
            // 
            this.tlpMain.SetColumnSpan(this.gbPlot, 6);
            this.gbPlot.Controls.Add(this.txtPlotSynopsis);
            this.gbPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlot.Location = new System.Drawing.Point(273, 3);
            this.gbPlot.Name = "gbPlot";
            this.tlpMain.SetRowSpan(this.gbPlot, 3);
            this.gbPlot.Size = new System.Drawing.Size(541, 171);
            this.gbPlot.TabIndex = 20;
            this.gbPlot.TabStop = false;
            this.gbPlot.Text = "Plot";
            // 
            // txtPlotSynopsis
            // 
            this.txtPlotSynopsis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlotSynopsis.Location = new System.Drawing.Point(3, 16);
            this.txtPlotSynopsis.Multiline = true;
            this.txtPlotSynopsis.Name = "txtPlotSynopsis";
            this.txtPlotSynopsis.ReadOnly = true;
            this.txtPlotSynopsis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlotSynopsis.Size = new System.Drawing.Size(535, 152);
            this.txtPlotSynopsis.TabIndex = 21;
            this.txtPlotSynopsis.Text = "Unknown";
            // 
            // gbStarring
            // 
            this.tlpMain.SetColumnSpan(this.gbStarring, 6);
            this.gbStarring.Controls.Add(this.flpActors);
            this.gbStarring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStarring.Location = new System.Drawing.Point(273, 180);
            this.gbStarring.Name = "gbStarring";
            this.tlpMain.SetRowSpan(this.gbStarring, 4);
            this.gbStarring.Size = new System.Drawing.Size(541, 250);
            this.gbStarring.TabIndex = 21;
            this.gbStarring.TabStop = false;
            this.gbStarring.Text = "Starring";
            // 
            // gbMetadata
            // 
            this.tlpMain.SetColumnSpan(this.gbMetadata, 3);
            this.gbMetadata.Controls.Add(this.tlpMetadata);
            this.gbMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMetadata.Location = new System.Drawing.Point(3, 239);
            this.gbMetadata.Name = "gbMetadata";
            this.tlpMain.SetRowSpan(this.gbMetadata, 3);
            this.gbMetadata.Size = new System.Drawing.Size(264, 191);
            this.gbMetadata.TabIndex = 22;
            this.gbMetadata.TabStop = false;
            this.gbMetadata.Text = "Metadata";
            // 
            // tlpMetadata
            // 
            this.tlpMetadata.ColumnCount = 2;
            this.tlpMetadata.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.56701F));
            this.tlpMetadata.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.43299F));
            this.tlpMetadata.Controls.Add(this.lblGenreValue, 1, 4);
            this.tlpMetadata.Controls.Add(this.lblContainerValue, 1, 3);
            this.tlpMetadata.Controls.Add(this.lblRuntimeValue, 1, 1);
            this.tlpMetadata.Controls.Add(this.lblSizeValue, 1, 0);
            this.tlpMetadata.Controls.Add(this.lblResolutionValue, 1, 2);
            this.tlpMetadata.Controls.Add(this.lblSize, 0, 0);
            this.tlpMetadata.Controls.Add(this.lblRuntime, 0, 1);
            this.tlpMetadata.Controls.Add(this.lblResolution, 0, 2);
            this.tlpMetadata.Controls.Add(this.lblGenre, 0, 4);
            this.tlpMetadata.Controls.Add(this.lblContainer, 0, 3);
            this.tlpMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMetadata.Location = new System.Drawing.Point(3, 16);
            this.tlpMetadata.Name = "tlpMetadata";
            this.tlpMetadata.RowCount = 5;
            this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMetadata.Size = new System.Drawing.Size(258, 172);
            this.tlpMetadata.TabIndex = 23;
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
            this.tlpMain.Controls.Add(this.gbMetadata, 0, 4);
            this.tlpMain.Controls.Add(this.gbPlot, 3, 0);
            this.tlpMain.Controls.Add(this.gbStarring, 3, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.75F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(817, 433);
            this.tlpMain.TabIndex = 23;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmStream,
            this.itmExit});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(817, 24);
            this.menuMain.TabIndex = 24;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmImport,
            this.itmExport});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmImport
            // 
            this.itmImport.Name = "itmImport";
            this.itmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmImport.Size = new System.Drawing.Size(180, 22);
            this.itmImport.Text = "Import";
            this.itmImport.Click += new System.EventHandler(this.itmImport_Click);
            // 
            // itmExport
            // 
            this.itmExport.Name = "itmExport";
            this.itmExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmExport.Size = new System.Drawing.Size(180, 22);
            this.itmExport.Text = "Export";
            this.itmExport.Click += new System.EventHandler(this.itmExport_Click);
            // 
            // itmStream
            // 
            this.itmStream.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmPvs,
            this.itmBrowser,
            this.itmVlc});
            this.itmStream.Enabled = false;
            this.itmStream.Name = "itmStream";
            this.itmStream.Size = new System.Drawing.Size(56, 20);
            this.itmStream.Text = "Stream";
            // 
            // itmPvs
            // 
            this.itmPvs.Name = "itmPvs";
            this.itmPvs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.itmPvs.Size = new System.Drawing.Size(180, 22);
            this.itmPvs.Text = "PVS";
            this.itmPvs.Click += new System.EventHandler(this.itmPvs_Click);
            // 
            // itmBrowser
            // 
            this.itmBrowser.Name = "itmBrowser";
            this.itmBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.itmBrowser.Size = new System.Drawing.Size(180, 22);
            this.itmBrowser.Text = "Browser";
            // 
            // itmVlc
            // 
            this.itmVlc.Name = "itmVlc";
            this.itmVlc.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.itmVlc.Size = new System.Drawing.Size(180, 22);
            this.itmVlc.Text = "VLC";
            // 
            // itmExit
            // 
            this.itmExit.Name = "itmExit";
            this.itmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itmExit.Size = new System.Drawing.Size(38, 20);
            this.itmExit.Text = "Exit";
            this.itmExit.Click += new System.EventHandler(this.itmExit_Click);
            // 
            // Metadata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(817, 457);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Metadata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metadata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Metadata_FormClosing);
            this.Load += new System.EventHandler(this.Metadata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.gbPlot.ResumeLayout(false);
            this.gbPlot.PerformLayout();
            this.gbStarring.ResumeLayout(false);
            this.gbMetadata.ResumeLayout(false);
            this.tlpMetadata.ResumeLayout(false);
            this.tlpMetadata.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpActors;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblSizeValue;
        private System.Windows.Forms.Label lblRuntime;
        private System.Windows.Forms.Label lblRuntimeValue;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblGenreValue;
        private System.Windows.Forms.Label lblResolutionValue;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.OpenFileDialog ofdImport;
        private System.Windows.Forms.Label lblContainerValue;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.ToolTip tipMain;
        private System.Windows.Forms.GroupBox gbPlot;
        private System.Windows.Forms.TextBox txtPlotSynopsis;
        private System.Windows.Forms.GroupBox gbStarring;
        private System.Windows.Forms.GroupBox gbMetadata;
        private System.Windows.Forms.TableLayoutPanel tlpMetadata;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmImport;
        private System.Windows.Forms.ToolStripMenuItem itmExport;
        private System.Windows.Forms.ToolStripMenuItem itmStream;
        private System.Windows.Forms.ToolStripMenuItem itmPvs;
        private System.Windows.Forms.ToolStripMenuItem itmBrowser;
        private System.Windows.Forms.ToolStripMenuItem itmVlc;
        private System.Windows.Forms.ToolStripMenuItem itmExit;
    }
}