namespace PlexDL.Common.Shodan.UI
{
    partial class TokenFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenFinder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmActions = new System.Windows.Forms.ToolStripMenuItem();
            this.itmActionsBeginSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.lblViewing = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblViewingValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvTokens = new PlexDL.Common.Components.Controls.FlatDataGridView();
            this.cxtGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cxtGridStartSession = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtGridCopyToken = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.itmFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).BeginInit();
            this.cxtGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmActions,
            this.itmSettings});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFileExport,
            this.itmFileImport});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmFileExport
            // 
            this.itmFileExport.Name = "itmFileExport";
            this.itmFileExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmFileExport.Size = new System.Drawing.Size(180, 22);
            this.itmFileExport.Text = "Export";
            this.itmFileExport.Click += new System.EventHandler(this.ItmFileExport_Click);
            // 
            // itmActions
            // 
            this.itmActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmActionsBeginSearch});
            this.itmActions.Name = "itmActions";
            this.itmActions.Size = new System.Drawing.Size(59, 20);
            this.itmActions.Text = "Actions";
            // 
            // itmActionsBeginSearch
            // 
            this.itmActionsBeginSearch.Name = "itmActionsBeginSearch";
            this.itmActionsBeginSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.itmActionsBeginSearch.Size = new System.Drawing.Size(184, 22);
            this.itmActionsBeginSearch.Text = "Begin Search";
            this.itmActionsBeginSearch.Click += new System.EventHandler(this.ItmActionsBeginSearch_Click);
            // 
            // itmSettings
            // 
            this.itmSettings.Name = "itmSettings";
            this.itmSettings.Size = new System.Drawing.Size(61, 20);
            this.itmSettings.Text = "Settings";
            this.itmSettings.Click += new System.EventHandler(this.ItmSettings_Click);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblViewing,
            this.lblViewingValue});
            this.statusMain.Location = new System.Drawing.Point(0, 428);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(800, 22);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusStrip1";
            // 
            // lblViewing
            // 
            this.lblViewing.Name = "lblViewing";
            this.lblViewing.Size = new System.Drawing.Size(52, 17);
            this.lblViewing.Text = "Viewing:";
            // 
            // lblViewingValue
            // 
            this.lblViewingValue.Name = "lblViewingValue";
            this.lblViewingValue.Size = new System.Drawing.Size(24, 17);
            this.lblViewingValue.Text = "0/0";
            // 
            // dgvTokens
            // 
            this.dgvTokens.AllowUserToAddRows = false;
            this.dgvTokens.AllowUserToDeleteRows = false;
            this.dgvTokens.AllowUserToOrderColumns = true;
            this.dgvTokens.AllowUserToResizeRows = false;
            this.dgvTokens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTokens.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour1.BoolColouringEnabled = false;
            boolColour1.ColouringMode = PlexDL.Common.Components.Styling.BoolColourMode.BackColour;
            boolColour1.FalseColour = System.Drawing.Color.DarkRed;
            boolColour1.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour1.RelevantColumns")));
            boolColour1.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvTokens.BoolColouringScheme = boolColour1;
            this.dgvTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTokens.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvTokens.CellContentClickMessage = false;
            this.dgvTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTokens.ContextMenuStrip = this.cxtGrid;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTokens.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTokens.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvTokens.IsContentTable = false;
            this.dgvTokens.Location = new System.Drawing.Point(0, 24);
            this.dgvTokens.MultiSelect = false;
            this.dgvTokens.Name = "dgvTokens";
            this.dgvTokens.RowHeadersVisible = false;
            this.dgvTokens.RowsEmptyText = "No Data Found";
            this.dgvTokens.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvTokens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTokens.Size = new System.Drawing.Size(800, 404);
            this.dgvTokens.TabIndex = 2;
            this.dgvTokens.DoubleClick += new System.EventHandler(this.DgvTokens_DoubleClick);
            // 
            // cxtGrid
            // 
            this.cxtGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cxtGridStartSession,
            this.cxtGridCopyToken});
            this.cxtGrid.Name = "cxtGrid";
            this.cxtGrid.Size = new System.Drawing.Size(141, 48);
            // 
            // cxtGridStartSession
            // 
            this.cxtGridStartSession.Enabled = false;
            this.cxtGridStartSession.Name = "cxtGridStartSession";
            this.cxtGridStartSession.Size = new System.Drawing.Size(140, 22);
            this.cxtGridStartSession.Text = "Start Session";
            this.cxtGridStartSession.Click += new System.EventHandler(this.CxtGridStartSession_Click);
            // 
            // cxtGridCopyToken
            // 
            this.cxtGridCopyToken.Enabled = false;
            this.cxtGridCopyToken.Name = "cxtGridCopyToken";
            this.cxtGridCopyToken.Size = new System.Drawing.Size(140, 22);
            this.cxtGridCopyToken.Text = "Copy Token";
            this.cxtGridCopyToken.Click += new System.EventHandler(this.CxtGridCopyToken_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.Filter = "Comma Separated Values File|*.csv|JavaScript Object Notation File|*.json|Logging " +
    "Delimited File|*.logdel|eXtensible Markup Language File|*.xml";
            // 
            // itmFileImport
            // 
            this.itmFileImport.Name = "itmFileImport";
            this.itmFileImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmFileImport.Size = new System.Drawing.Size(180, 22);
            this.itmFileImport.Text = "Import";
            // 
            // ofdImport
            // 
            this.ofdImport.Filter = "Comma Separated Values File|*.csv|JavaScript Object Notation File|*.json|Logging " +
    "Delimited File|*.logdel|eXtensible Markup Language File|*.xml";
            // 
            // TokenFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTokens);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TokenFinder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Token Finder";
            this.Load += new System.EventHandler(this.TokenFinder_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).EndInit();
            this.cxtGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.StatusStrip statusMain;
        private Components.Controls.FlatDataGridView dgvTokens;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmFileExport;
        private System.Windows.Forms.ToolStripMenuItem itmSettings;
        private System.Windows.Forms.ToolStripMenuItem itmActions;
        private System.Windows.Forms.ToolStripMenuItem itmActionsBeginSearch;
        private System.Windows.Forms.ToolStripStatusLabel lblViewing;
        private System.Windows.Forms.ToolStripStatusLabel lblViewingValue;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.ContextMenuStrip cxtGrid;
        private System.Windows.Forms.ToolStripMenuItem cxtGridStartSession;
        private System.Windows.Forms.ToolStripMenuItem cxtGridCopyToken;
        private System.Windows.Forms.ToolStripMenuItem itmFileImport;
        private System.Windows.Forms.OpenFileDialog ofdImport;
    }
}