using PlexDL.Common.Components.Controls;

namespace PlexDL.Common.Pxz.UI
{
    partial class PxzExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PxzExplorer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            PlexDL.Common.Components.Styling.BoolColour boolColour2 = new PlexDL.Common.Components.Styling.BoolColour();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvRecords = new PlexDL.Common.Components.Controls.FlatDataGridView();
            this.cxtExtract = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmExtractRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAttributes = new PlexDL.Common.Components.Controls.FlatDataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.sfdExtract = new System.Windows.Forms.SaveFileDialog();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpenPxzFile = new System.Windows.Forms.OpenFileDialog();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.cxtExtract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).BeginInit();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.dgvRecords, 1, 0);
            this.tlpMain.Controls.Add(this.dgvAttributes, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(800, 426);
            this.tlpMain.TabIndex = 0;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AllowUserToOrderColumns = true;
            this.dgvRecords.AllowUserToResizeRows = false;
            this.dgvRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecords.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour1.BoolColouringEnabled = false;
            boolColour1.ColouringMode = PlexDL.Common.Components.Styling.BoolColourMode.BackColour;
            boolColour1.FalseColour = System.Drawing.Color.DarkRed;
            boolColour1.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour1.RelevantColumns")));
            boolColour1.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvRecords.BoolColouringScheme = boolColour1;
            this.dgvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvRecords.CellContentClickMessage = true;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.ContextMenuStrip = this.cxtExtract;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecords.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvRecords.IsContentTable = false;
            this.dgvRecords.Location = new System.Drawing.Point(403, 3);
            this.dgvRecords.MultiSelect = false;
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.RowHeadersVisible = false;
            this.dgvRecords.RowsEmptyText = "No Records Found";
            this.dgvRecords.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(394, 391);
            this.dgvRecords.TabIndex = 0;
            // 
            // cxtExtract
            // 
            this.cxtExtract.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmExtractRecord});
            this.cxtExtract.Name = "cxtExtract";
            this.cxtExtract.Size = new System.Drawing.Size(151, 26);
            this.cxtExtract.Opening += new System.ComponentModel.CancelEventHandler(this.CxtExtract_Opening);
            // 
            // itmExtractRecord
            // 
            this.itmExtractRecord.Name = "itmExtractRecord";
            this.itmExtractRecord.Size = new System.Drawing.Size(150, 22);
            this.itmExtractRecord.Text = "Extract Record";
            this.itmExtractRecord.Click += new System.EventHandler(this.ItmExtractRecord_Click);
            // 
            // dgvAttributes
            // 
            this.dgvAttributes.AllowUserToAddRows = false;
            this.dgvAttributes.AllowUserToDeleteRows = false;
            this.dgvAttributes.AllowUserToOrderColumns = true;
            this.dgvAttributes.AllowUserToResizeRows = false;
            this.dgvAttributes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttributes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour2.BoolColouringEnabled = false;
            boolColour2.ColouringMode = PlexDL.Common.Components.Styling.BoolColourMode.BackColour;
            boolColour2.FalseColour = System.Drawing.Color.DarkRed;
            boolColour2.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour2.RelevantColumns")));
            boolColour2.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvAttributes.BoolColouringScheme = boolColour2;
            this.dgvAttributes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAttributes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvAttributes.CellContentClickMessage = true;
            this.dgvAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAttributes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttributes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvAttributes.IsContentTable = false;
            this.dgvAttributes.Location = new System.Drawing.Point(3, 3);
            this.dgvAttributes.MultiSelect = false;
            this.dgvAttributes.Name = "dgvAttributes";
            this.dgvAttributes.RowHeadersVisible = false;
            this.dgvAttributes.RowsEmptyText = "No Attributes Found";
            this.dgvAttributes.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvAttributes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttributes.Size = new System.Drawing.Size(394, 391);
            this.dgvAttributes.TabIndex = 1;
            // 
            // btnOK
            // 
            this.tlpMain.SetColumnSpan(this.btnOK, 2);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(3, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(794, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // sfdExtract
            // 
            this.sfdExtract.AddExtension = false;
            this.sfdExtract.Filter = "All files|*.*";
            this.sfdExtract.Title = "Extract Record";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuMain";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmOpen});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmOpen
            // 
            this.itmOpen.Name = "itmOpen";
            this.itmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itmOpen.Size = new System.Drawing.Size(146, 22);
            this.itmOpen.Text = "Open";
            this.itmOpen.Click += new System.EventHandler(this.ItmOpen_Click);
            // 
            // ofdOpenPxzFile
            // 
            this.ofdOpenPxzFile.Filter = "PXZ Files|*.pxz";
            this.ofdOpenPxzFile.Title = "Open PXZ File";
            // 
            // PxzInformation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "PxzExplorer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PXZ Explorer";
            this.Load += new System.EventHandler(this.PxzInformation_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.cxtExtract.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttributes)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private FlatDataGridView dgvRecords;
        private FlatDataGridView dgvAttributes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip cxtExtract;
        private System.Windows.Forms.ToolStripMenuItem itmExtractRecord;
        private System.Windows.Forms.SaveFileDialog sfdExtract;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmOpen;
        private System.Windows.Forms.OpenFileDialog ofdOpenPxzFile;
    }
}