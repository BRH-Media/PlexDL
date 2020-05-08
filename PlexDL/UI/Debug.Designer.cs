namespace PlexDL.UI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmrAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.tlpDebug = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbExportFormat = new System.Windows.Forms.GroupBox();
            this.cbxExportFormat = new System.Windows.Forms.ComboBox();
            this.gbExportMode = new System.Windows.Forms.GroupBox();
            this.pnlExportMode = new System.Windows.Forms.Panel();
            this.radModeTable = new System.Windows.Forms.RadioButton();
            this.radModeView = new System.Windows.Forms.RadioButton();
            this.gbDataExport = new System.Windows.Forms.GroupBox();
            this.btnExportFiltered = new System.Windows.Forms.Button();
            this.btnExportSections = new System.Windows.Forms.Button();
            this.btnExportEpisodes = new System.Windows.Forms.Button();
            this.btnExportTitles = new System.Windows.Forms.Button();
            this.btnExportSeasons = new System.Windows.Forms.Button();
            this.gbGlobalFlags = new System.Windows.Forms.GroupBox();
            this.dgvGlobalFlags = new PlexDL.Common.Components.FlatDataGridView();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPollRate = new System.Windows.Forms.TableLayoutPanel();
            this.lblPollRate = new System.Windows.Forms.Label();
            this.numPollRateValue = new System.Windows.Forms.NumericUpDown();
            this.tlpRefreshCount = new System.Windows.Forms.TableLayoutPanel();
            this.lblRefreshCountValue = new System.Windows.Forms.Label();
            this.lblRefreshCount = new System.Windows.Forms.Label();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.tlpDebug.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbExportFormat.SuspendLayout();
            this.gbExportMode.SuspendLayout();
            this.pnlExportMode.SuspendLayout();
            this.gbDataExport.SuspendLayout();
            this.gbGlobalFlags.SuspendLayout();
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
            this.tlpDebug.Controls.Add(this.groupBox1, 1, 0);
            this.tlpDebug.Controls.Add(this.gbGlobalFlags, 0, 0);
            this.tlpDebug.Controls.Add(this.tlpControls, 0, 9);
            this.tlpDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDebug.Location = new System.Drawing.Point(0, 0);
            this.tlpDebug.Name = "tlpDebug";
            this.tlpDebug.RowCount = 10;
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.48566F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.97323F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDebug.Size = new System.Drawing.Size(450, 523);
            this.tlpDebug.TabIndex = 0;
            this.tlpDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpDebug_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbExportFormat);
            this.groupBox1.Controls.Add(this.gbExportMode);
            this.groupBox1.Controls.Add(this.gbDataExport);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(228, 3);
            this.groupBox1.Name = "groupBox1";
            this.tlpDebug.SetRowSpan(this.groupBox1, 9);
            this.groupBox1.Size = new System.Drawing.Size(219, 422);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Values";
            // 
            // gbExportFormat
            // 
            this.gbExportFormat.Controls.Add(this.cbxExportFormat);
            this.gbExportFormat.Location = new System.Drawing.Point(6, 189);
            this.gbExportFormat.Name = "gbExportFormat";
            this.gbExportFormat.Size = new System.Drawing.Size(204, 49);
            this.gbExportFormat.TabIndex = 9;
            this.gbExportFormat.TabStop = false;
            this.gbExportFormat.Text = "Export Format";
            // 
            // cbxExportFormat
            // 
            this.cbxExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExportFormat.FormattingEnabled = true;
            this.cbxExportFormat.Items.AddRange(new object[] {
            "CSV",
            "JSON",
            "XML",
            "LOGDEL"});
            this.cbxExportFormat.Location = new System.Drawing.Point(6, 19);
            this.cbxExportFormat.Name = "cbxExportFormat";
            this.cbxExportFormat.Size = new System.Drawing.Size(192, 21);
            this.cbxExportFormat.TabIndex = 0;
            // 
            // gbExportMode
            // 
            this.gbExportMode.Controls.Add(this.pnlExportMode);
            this.gbExportMode.Location = new System.Drawing.Point(6, 251);
            this.gbExportMode.Name = "gbExportMode";
            this.gbExportMode.Size = new System.Drawing.Size(204, 49);
            this.gbExportMode.TabIndex = 8;
            this.gbExportMode.TabStop = false;
            this.gbExportMode.Text = "Export Mode";
            // 
            // pnlExportMode
            // 
            this.pnlExportMode.Controls.Add(this.radModeTable);
            this.pnlExportMode.Controls.Add(this.radModeView);
            this.pnlExportMode.Location = new System.Drawing.Point(6, 19);
            this.pnlExportMode.Name = "pnlExportMode";
            this.pnlExportMode.Size = new System.Drawing.Size(114, 24);
            this.pnlExportMode.TabIndex = 7;
            // 
            // radModeTable
            // 
            this.radModeTable.AutoSize = true;
            this.radModeTable.Checked = true;
            this.radModeTable.Location = new System.Drawing.Point(3, 3);
            this.radModeTable.Name = "radModeTable";
            this.radModeTable.Size = new System.Drawing.Size(52, 17);
            this.radModeTable.TabIndex = 6;
            this.radModeTable.TabStop = true;
            this.radModeTable.Text = "Table";
            this.radModeTable.UseVisualStyleBackColor = true;
            // 
            // radModeView
            // 
            this.radModeView.AutoSize = true;
            this.radModeView.Location = new System.Drawing.Point(61, 3);
            this.radModeView.Name = "radModeView";
            this.radModeView.Size = new System.Drawing.Size(48, 17);
            this.radModeView.TabIndex = 5;
            this.radModeView.Text = "View";
            this.radModeView.UseVisualStyleBackColor = true;
            // 
            // gbDataExport
            // 
            this.gbDataExport.Controls.Add(this.btnExportFiltered);
            this.gbDataExport.Controls.Add(this.btnExportSections);
            this.gbDataExport.Controls.Add(this.btnExportEpisodes);
            this.gbDataExport.Controls.Add(this.btnExportTitles);
            this.gbDataExport.Controls.Add(this.btnExportSeasons);
            this.gbDataExport.Location = new System.Drawing.Point(6, 19);
            this.gbDataExport.Name = "gbDataExport";
            this.gbDataExport.Size = new System.Drawing.Size(207, 164);
            this.gbDataExport.TabIndex = 4;
            this.gbDataExport.TabStop = false;
            this.gbDataExport.Text = "Data Export";
            // 
            // btnExportFiltered
            // 
            this.btnExportFiltered.Location = new System.Drawing.Point(6, 77);
            this.btnExportFiltered.Name = "btnExportFiltered";
            this.btnExportFiltered.Size = new System.Drawing.Size(195, 23);
            this.btnExportFiltered.TabIndex = 4;
            this.btnExportFiltered.Text = "Filtered";
            this.btnExportFiltered.UseVisualStyleBackColor = true;
            this.btnExportFiltered.Click += new System.EventHandler(this.btnExportFiltered_Click);
            // 
            // btnExportSections
            // 
            this.btnExportSections.Location = new System.Drawing.Point(6, 19);
            this.btnExportSections.Name = "btnExportSections";
            this.btnExportSections.Size = new System.Drawing.Size(195, 23);
            this.btnExportSections.TabIndex = 1;
            this.btnExportSections.Text = "Sections";
            this.btnExportSections.UseVisualStyleBackColor = true;
            this.btnExportSections.Click += new System.EventHandler(this.btnExportSections_Click);
            // 
            // btnExportEpisodes
            // 
            this.btnExportEpisodes.Location = new System.Drawing.Point(6, 135);
            this.btnExportEpisodes.Name = "btnExportEpisodes";
            this.btnExportEpisodes.Size = new System.Drawing.Size(195, 23);
            this.btnExportEpisodes.TabIndex = 3;
            this.btnExportEpisodes.Text = "TV Episodes";
            this.btnExportEpisodes.UseVisualStyleBackColor = true;
            this.btnExportEpisodes.Click += new System.EventHandler(this.btnExportEpisodes_Click);
            // 
            // btnExportTitles
            // 
            this.btnExportTitles.Location = new System.Drawing.Point(6, 48);
            this.btnExportTitles.Name = "btnExportTitles";
            this.btnExportTitles.Size = new System.Drawing.Size(195, 23);
            this.btnExportTitles.TabIndex = 0;
            this.btnExportTitles.Text = "Titles";
            this.btnExportTitles.UseVisualStyleBackColor = true;
            this.btnExportTitles.Click += new System.EventHandler(this.btnExportTitles_Click);
            // 
            // btnExportSeasons
            // 
            this.btnExportSeasons.Location = new System.Drawing.Point(6, 106);
            this.btnExportSeasons.Name = "btnExportSeasons";
            this.btnExportSeasons.Size = new System.Drawing.Size(195, 23);
            this.btnExportSeasons.TabIndex = 2;
            this.btnExportSeasons.Text = "TV Seasons";
            this.btnExportSeasons.UseVisualStyleBackColor = true;
            this.btnExportSeasons.Click += new System.EventHandler(this.btnExportSeasons_Click);
            // 
            // gbGlobalFlags
            // 
            this.gbGlobalFlags.Controls.Add(this.dgvGlobalFlags);
            this.gbGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGlobalFlags.Location = new System.Drawing.Point(3, 3);
            this.gbGlobalFlags.Name = "gbGlobalFlags";
            this.tlpDebug.SetRowSpan(this.gbGlobalFlags, 9);
            this.gbGlobalFlags.Size = new System.Drawing.Size(219, 422);
            this.gbGlobalFlags.TabIndex = 14;
            this.gbGlobalFlags.TabStop = false;
            this.gbGlobalFlags.Text = "Global Flags";
            // 
            // dgvGlobalFlags
            // 
            this.dgvGlobalFlags.AllowUserToAddRows = false;
            this.dgvGlobalFlags.AllowUserToDeleteRows = false;
            this.dgvGlobalFlags.AllowUserToOrderColumns = true;
            this.dgvGlobalFlags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGlobalFlags.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvGlobalFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGlobalFlags.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvGlobalFlags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlobalFlags.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGlobalFlags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGlobalFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvGlobalFlags.Location = new System.Drawing.Point(3, 16);
            this.dgvGlobalFlags.MultiSelect = false;
            this.dgvGlobalFlags.Name = "dgvGlobalFlags";
            this.dgvGlobalFlags.RowHeadersVisible = false;
            this.dgvGlobalFlags.RowsEmptyText = "No Flags Found";
            this.dgvGlobalFlags.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvGlobalFlags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGlobalFlags.Size = new System.Drawing.Size(213, 403);
            this.dgvGlobalFlags.TabIndex = 2;
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
            this.tlpControls.Location = new System.Drawing.Point(3, 431);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 2;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpControls.Size = new System.Drawing.Size(444, 89);
            this.tlpControls.TabIndex = 15;
            // 
            // tlpPollRate
            // 
            this.tlpPollRate.ColumnCount = 2;
            this.tlpControls.SetColumnSpan(this.tlpPollRate, 2);
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPollRate.Controls.Add(this.lblPollRate, 0, 0);
            this.tlpPollRate.Controls.Add(this.numPollRateValue, 1, 0);
            this.tlpPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPollRate.Location = new System.Drawing.Point(225, 3);
            this.tlpPollRate.Name = "tlpPollRate";
            this.tlpPollRate.RowCount = 1;
            this.tlpPollRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPollRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpPollRate.Size = new System.Drawing.Size(216, 38);
            this.tlpPollRate.TabIndex = 19;
            // 
            // lblPollRate
            // 
            this.lblPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPollRate.Location = new System.Drawing.Point(3, 0);
            this.lblPollRate.Name = "lblPollRate";
            this.lblPollRate.Size = new System.Drawing.Size(102, 38);
            this.lblPollRate.TabIndex = 4;
            this.lblPollRate.Text = "Poll Rate (ms):";
            this.lblPollRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numPollRateValue
            // 
            this.numPollRateValue.Location = new System.Drawing.Point(111, 3);
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
            this.numPollRateValue.Size = new System.Drawing.Size(102, 20);
            this.numPollRateValue.TabIndex = 5;
            this.numPollRateValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numPollRateValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPollRateValue.ValueChanged += new System.EventHandler(this.numPollRateValue_ValueChanged);
            // 
            // tlpRefreshCount
            // 
            this.tlpRefreshCount.ColumnCount = 2;
            this.tlpControls.SetColumnSpan(this.tlpRefreshCount, 2);
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCountValue, 1, 0);
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCount, 0, 0);
            this.tlpRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRefreshCount.Location = new System.Drawing.Point(3, 3);
            this.tlpRefreshCount.Name = "tlpRefreshCount";
            this.tlpRefreshCount.RowCount = 1;
            this.tlpRefreshCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRefreshCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpRefreshCount.Size = new System.Drawing.Size(216, 38);
            this.tlpRefreshCount.TabIndex = 6;
            // 
            // lblRefreshCountValue
            // 
            this.lblRefreshCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCountValue.Location = new System.Drawing.Point(111, 0);
            this.lblRefreshCountValue.Name = "lblRefreshCountValue";
            this.lblRefreshCountValue.Size = new System.Drawing.Size(102, 38);
            this.lblRefreshCountValue.TabIndex = 5;
            this.lblRefreshCountValue.Text = "0";
            this.lblRefreshCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefreshCount
            // 
            this.lblRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCount.Location = new System.Drawing.Point(3, 0);
            this.lblRefreshCount.Name = "lblRefreshCount";
            this.lblRefreshCount.Size = new System.Drawing.Size(102, 38);
            this.lblRefreshCount.TabIndex = 4;
            this.lblRefreshCount.Text = "Refresh Count:";
            this.lblRefreshCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTimer
            // 
            this.tlpControls.SetColumnSpan(this.btnTimer, 2);
            this.btnTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimer.Location = new System.Drawing.Point(114, 47);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(216, 39);
            this.btnTimer.TabIndex = 17;
            this.btnTimer.Text = "Auto-refresh On";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.Location = new System.Drawing.Point(3, 47);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 39);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(336, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 39);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "csv";
            this.sfdExport.Title = "Export";
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 523);
            this.ControlBox = false;
            this.Controls.Add(this.tlpDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.tlpDebug.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbExportFormat.ResumeLayout(false);
            this.gbExportMode.ResumeLayout(false);
            this.pnlExportMode.ResumeLayout(false);
            this.pnlExportMode.PerformLayout();
            this.gbDataExport.ResumeLayout(false);
            this.gbGlobalFlags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).EndInit();
            this.tlpControls.ResumeLayout(false);
            this.tlpPollRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPollRateValue)).EndInit();
            this.tlpRefreshCount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrAutoRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpDebug;
        private System.Windows.Forms.GroupBox gbGlobalFlags;
        private Common.Components.FlatDataGridView dgvGlobalFlags;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRefreshCount;
        private System.Windows.Forms.Label lblRefreshCountValue;
        private System.Windows.Forms.TableLayoutPanel tlpRefreshCount;
        private System.Windows.Forms.TableLayoutPanel tlpPollRate;
        private System.Windows.Forms.Label lblPollRate;
        private System.Windows.Forms.Button btnExportTitles;
        private System.Windows.Forms.Button btnExportSections;
        private System.Windows.Forms.Button btnExportSeasons;
        private System.Windows.Forms.Button btnExportEpisodes;
        private System.Windows.Forms.GroupBox gbDataExport;
        private System.Windows.Forms.NumericUpDown numPollRateValue;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.Button btnExportFiltered;
        private System.Windows.Forms.RadioButton radModeView;
        private System.Windows.Forms.RadioButton radModeTable;
        private System.Windows.Forms.Panel pnlExportMode;
        private System.Windows.Forms.GroupBox gbExportMode;
        private System.Windows.Forms.GroupBox gbExportFormat;
        private System.Windows.Forms.ComboBox cbxExportFormat;
    }
}