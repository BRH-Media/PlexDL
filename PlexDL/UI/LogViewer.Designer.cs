using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class LogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.lstLogFiles = new System.Windows.Forms.ListBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.itmStartSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmThisSession = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSearchTerm = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCancelSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmGoToLine = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.itmJson = new System.Windows.Forms.ToolStripMenuItem();
            this.itmBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExportCsv = new System.Windows.Forms.SaveFileDialog();
            this.sfdExportJson = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
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
            this.tlpMain.SetColumnSpan(this.dgvMain, 2);
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dgvMain.Location = new System.Drawing.Point(227, 3);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.tlpMain.SetRowSpan(this.dgvMain, 2);
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(444, 405);
            this.dgvMain.TabIndex = 11;
            this.dgvMain.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMain_CellContentDoubleClick);
            // 
            // sfdBackup
            // 
            this.sfdBackup.DefaultExt = "zip";
            this.sfdBackup.Filter = "Zip Archives|*.zip";
            this.sfdBackup.Title = "Backup Logs";
            // 
            // lstLogFiles
            // 
            this.lstLogFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLogFiles.Location = new System.Drawing.Point(3, 3);
            this.lstLogFiles.Name = "lstLogFiles";
            this.tlpMain.SetRowSpan(this.lstLogFiles, 2);
            this.lstLogFiles.ScrollAlwaysVisible = true;
            this.lstLogFiles.Size = new System.Drawing.Size(218, 405);
            this.lstLogFiles.TabIndex = 0;
            this.lstLogFiles.SelectedValueChanged += new System.EventHandler(this.LstLogFiles_SelectedIndexChanged);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.Controls.Add(this.lstLogFiles, 0, 0);
            this.tlpMain.Controls.Add(this.dgvMain, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(674, 411);
            this.tlpMain.TabIndex = 16;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmRefresh,
            this.itmStartSearch,
            this.itmGoToLine,
            this.itmExport,
            this.itmBackup});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(674, 24);
            this.menuMain.TabIndex = 17;
            this.menuMain.Text = "menuMain";
            // 
            // itmRefresh
            // 
            this.itmRefresh.Name = "itmRefresh";
            this.itmRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.itmRefresh.Size = new System.Drawing.Size(58, 20);
            this.itmRefresh.Text = "Refresh";
            this.itmRefresh.Click += new System.EventHandler(this.ItmRefresh_Click);
            // 
            // itmStartSearch
            // 
            this.itmStartSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmThisSession,
            this.itmSearchTerm,
            this.itmCancelSearch});
            this.itmStartSearch.Name = "itmStartSearch";
            this.itmStartSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itmStartSearch.Size = new System.Drawing.Size(54, 20);
            this.itmStartSearch.Text = "Search";
            // 
            // itmThisSession
            // 
            this.itmThisSession.Name = "itmThisSession";
            this.itmThisSession.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.itmThisSession.Size = new System.Drawing.Size(231, 22);
            this.itmThisSession.Text = "Entries for this Session";
            this.itmThisSession.Click += new System.EventHandler(this.ItmThisSession_Click);
            // 
            // itmSearchTerm
            // 
            this.itmSearchTerm.Name = "itmSearchTerm";
            this.itmSearchTerm.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itmSearchTerm.Size = new System.Drawing.Size(231, 22);
            this.itmSearchTerm.Text = "Search Term";
            this.itmSearchTerm.Click += new System.EventHandler(this.ItmSearchTerm_Click);
            // 
            // itmCancelSearch
            // 
            this.itmCancelSearch.Enabled = false;
            this.itmCancelSearch.Name = "itmCancelSearch";
            this.itmCancelSearch.Size = new System.Drawing.Size(231, 22);
            this.itmCancelSearch.Text = "Cancel Search";
            this.itmCancelSearch.Click += new System.EventHandler(this.ItmCancelSearch_Click);
            // 
            // itmGoToLine
            // 
            this.itmGoToLine.Name = "itmGoToLine";
            this.itmGoToLine.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.itmGoToLine.Size = new System.Drawing.Size(74, 20);
            this.itmGoToLine.Text = "Go To Line";
            this.itmGoToLine.Click += new System.EventHandler(this.ItmGoToLine_Click);
            // 
            // itmExport
            // 
            this.itmExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCsv,
            this.itmJson});
            this.itmExport.Name = "itmExport";
            this.itmExport.Size = new System.Drawing.Size(53, 20);
            this.itmExport.Text = "Export";
            // 
            // itmCsv
            // 
            this.itmCsv.Name = "itmCsv";
            this.itmCsv.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itmCsv.Size = new System.Drawing.Size(155, 22);
            this.itmCsv.Text = "To CSV";
            this.itmCsv.Click += new System.EventHandler(this.ItmCSV_Click);
            // 
            // itmJson
            // 
            this.itmJson.Name = "itmJson";
            this.itmJson.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.itmJson.Size = new System.Drawing.Size(155, 22);
            this.itmJson.Text = "To JSON";
            this.itmJson.Click += new System.EventHandler(this.ItmJson_Click);
            // 
            // itmBackup
            // 
            this.itmBackup.Name = "itmBackup";
            this.itmBackup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.itmBackup.Size = new System.Drawing.Size(58, 20);
            this.itmBackup.Text = "Backup";
            this.itmBackup.Click += new System.EventHandler(this.ItmBackup_Click);
            // 
            // sfdExportCsv
            // 
            this.sfdExportCsv.DefaultExt = "csv";
            this.sfdExportCsv.Filter = "CSV File|*.csv";
            this.sfdExportCsv.Title = "Export";
            // 
            // sfdExportJson
            // 
            this.sfdExportJson.DefaultExt = "json";
            this.sfdExportJson.Filter = "JSON File|*.json";
            this.sfdExportJson.Title = "Export";
            // 
            // LogViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(674, 435);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MinimizeBox = false;
            this.Name = "LogViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Viewer";
            this.Load += new System.EventHandler(this.LogViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DataGridView dgvMain;
        private SaveFileDialog sfdBackup;
        private ListBox lstLogFiles;
        private TableLayoutPanel tlpMain;
        private MenuStrip menuMain;
        private ToolStripMenuItem itmRefresh;
        private ToolStripMenuItem itmStartSearch;
        private ToolStripMenuItem itmGoToLine;
        private ToolStripMenuItem itmBackup;
        private ToolStripMenuItem itmExport;
        private ToolStripMenuItem itmCsv;
        private SaveFileDialog sfdExportCsv;
        private ToolStripMenuItem itmJson;
        private SaveFileDialog sfdExportJson;
        private ToolStripMenuItem itmThisSession;
        private ToolStripMenuItem itmSearchTerm;
        private ToolStripMenuItem itmCancelSearch;
    }
}