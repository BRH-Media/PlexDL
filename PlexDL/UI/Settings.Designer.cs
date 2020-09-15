using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.settingsGrid = new System.Windows.Forms.PropertyGrid();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmGlobal = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSettingsControl = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCommitToDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.itmReset = new System.Windows.Forms.ToolStripMenuItem();
            this.itmFileAssoc = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCreateAssociations = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsGrid
            // 
            this.tlpMain.SetColumnSpan(this.settingsGrid, 2);
            this.settingsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsGrid.Location = new System.Drawing.Point(3, 3);
            this.settingsGrid.Name = "settingsGrid";
            this.settingsGrid.Size = new System.Drawing.Size(313, 249);
            this.settingsGrid.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.btnCancel, 0, 1);
            this.tlpMain.Controls.Add(this.settingsGrid, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(319, 284);
            this.tlpMain.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(3, 258);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(153, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(162, 258);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(154, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmGlobal});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(319, 24);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmGlobal
            // 
            this.itmGlobal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSettingsControl,
            this.itmFileAssoc});
            this.itmGlobal.Name = "itmGlobal";
            this.itmGlobal.Size = new System.Drawing.Size(53, 20);
            this.itmGlobal.Text = "Global";
            // 
            // itmSettingsControl
            // 
            this.itmSettingsControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCommitToDefault,
            this.itmReset});
            this.itmSettingsControl.Name = "itmSettingsControl";
            this.itmSettingsControl.Size = new System.Drawing.Size(180, 22);
            this.itmSettingsControl.Text = "Settings Control";
            // 
            // itmCommitToDefault
            // 
            this.itmCommitToDefault.Name = "itmCommitToDefault";
            this.itmCommitToDefault.Size = new System.Drawing.Size(180, 22);
            this.itmCommitToDefault.Text = "Commit to Default";
            this.itmCommitToDefault.Click += new System.EventHandler(this.ItmCommitToDefault_Click);
            // 
            // itmReset
            // 
            this.itmReset.Name = "itmReset";
            this.itmReset.Size = new System.Drawing.Size(180, 22);
            this.itmReset.Text = "Reset";
            this.itmReset.Click += new System.EventHandler(this.ItmReset_Click);
            // 
            // itmFileAssoc
            // 
            this.itmFileAssoc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCreateAssociations});
            this.itmFileAssoc.Name = "itmFileAssoc";
            this.itmFileAssoc.Size = new System.Drawing.Size(180, 22);
            this.itmFileAssoc.Text = "File Associations";
            // 
            // itmCreateAssociations
            // 
            this.itmCreateAssociations.Name = "itmCreateAssociations";
            this.itmCreateAssociations.Size = new System.Drawing.Size(180, 22);
            this.itmCreateAssociations.Text = "Create";
            this.itmCreateAssociations.Click += new System.EventHandler(this.ItmCreateAssociations_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(319, 308);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlexDL Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tlpMain.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PropertyGrid settingsGrid;
        private TableLayoutPanel tlpMain;
        private Button btnOK;
        private Button btnCancel;
        private MenuStrip menuMain;
        private ToolStripMenuItem itmCommitToDefault;
        private ToolStripMenuItem itmReset;
        private ToolStripMenuItem itmGlobal;
        private ToolStripMenuItem itmFileAssoc;
        private ToolStripMenuItem itmSettingsControl;
        private ToolStripMenuItem itmCreateAssociations;
    }
}