using System;
using System.ComponentModel;
using System.Windows.Forms;
using PlexDL.Common.Components;

namespace PlexDL.UI
{
    partial class ServerManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerManager));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmAuthenticate = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViaToken = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViaPlexTv = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.itmServers = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRelays = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDirectConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLocalLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRenderTokenColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.itmClearServers = new System.Windows.Forms.ToolStripMenuItem();
            this.itmConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtServers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmViewLink = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViewAccountToken = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvServers = new PlexDL.Common.Components.FlatDataGridView();
            this.menuMain.SuspendLayout();
            this.cxtServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmAuthenticate,
            this.itmLoad,
            this.itmClearServers,
            this.itmConnect});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 17;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmAuthenticate
            // 
            this.itmAuthenticate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmViaToken,
            this.itmViaPlexTv});
            this.itmAuthenticate.Name = "itmAuthenticate";
            this.itmAuthenticate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.itmAuthenticate.Size = new System.Drawing.Size(87, 20);
            this.itmAuthenticate.Text = "Authenticate";
            // 
            // itmViaToken
            // 
            this.itmViaToken.Name = "itmViaToken";
            this.itmViaToken.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.itmViaToken.Size = new System.Drawing.Size(168, 22);
            this.itmViaToken.Text = "Via Token";
            this.itmViaToken.Click += new System.EventHandler(this.ItmViaToken_Click);
            // 
            // itmViaPlexTv
            // 
            this.itmViaPlexTv.Name = "itmViaPlexTv";
            this.itmViaPlexTv.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.itmViaPlexTv.Size = new System.Drawing.Size(168, 22);
            this.itmViaPlexTv.Text = "Via Plex.tv";
            this.itmViaPlexTv.Click += new System.EventHandler(this.ItmViaPlexTv_Click);
            // 
            // itmLoad
            // 
            this.itmLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmServers,
            this.itmRelays,
            this.itmDirectConnection,
            this.itmLocalLink,
            this.itmRenderTokenColumn});
            this.itmLoad.Enabled = false;
            this.itmLoad.Name = "itmLoad";
            this.itmLoad.Size = new System.Drawing.Size(45, 20);
            this.itmLoad.Text = "Load";
            // 
            // itmServers
            // 
            this.itmServers.Name = "itmServers";
            this.itmServers.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmServers.Size = new System.Drawing.Size(212, 22);
            this.itmServers.Text = "Servers";
            this.itmServers.Click += new System.EventHandler(this.ItmServers_Click);
            // 
            // itmRelays
            // 
            this.itmRelays.Name = "itmRelays";
            this.itmRelays.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.itmRelays.Size = new System.Drawing.Size(212, 22);
            this.itmRelays.Text = "Relays";
            this.itmRelays.Click += new System.EventHandler(this.ItmRelays_Click);
            // 
            // itmDirectConnection
            // 
            this.itmDirectConnection.Name = "itmDirectConnection";
            this.itmDirectConnection.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.itmDirectConnection.Size = new System.Drawing.Size(212, 22);
            this.itmDirectConnection.Text = "Direct Connection";
            this.itmDirectConnection.Click += new System.EventHandler(this.ItmDirectConnection_Click);
            // 
            // itmLocalLink
            // 
            this.itmLocalLink.Name = "itmLocalLink";
            this.itmLocalLink.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.itmLocalLink.Size = new System.Drawing.Size(212, 22);
            this.itmLocalLink.Text = "Local Link";
            this.itmLocalLink.Click += new System.EventHandler(this.ItmLocalLink_Click);
            // 
            // itmRenderTokenColumn
            // 
            this.itmRenderTokenColumn.CheckOnClick = true;
            this.itmRenderTokenColumn.Name = "itmRenderTokenColumn";
            this.itmRenderTokenColumn.Size = new System.Drawing.Size(212, 22);
            this.itmRenderTokenColumn.Text = "Render Token Column";
            this.itmRenderTokenColumn.Click += new System.EventHandler(this.ItmRenderTokenColumn_Click);
            // 
            // itmClearServers
            // 
            this.itmClearServers.Enabled = false;
            this.itmClearServers.Name = "itmClearServers";
            this.itmClearServers.Size = new System.Drawing.Size(86, 20);
            this.itmClearServers.Text = "Clear Servers";
            this.itmClearServers.Click += new System.EventHandler(this.ItmClearServers_Click);
            // 
            // itmConnect
            // 
            this.itmConnect.Enabled = false;
            this.itmConnect.Name = "itmConnect";
            this.itmConnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.itmConnect.Size = new System.Drawing.Size(134, 20);
            this.itmConnect.Text = "Connect to this server";
            this.itmConnect.Click += new System.EventHandler(this.itmConnect_Click);
            // 
            // cxtServers
            // 
            this.cxtServers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmViewLink,
            this.itmViewAccountToken});
            this.cxtServers.Name = "cxtServers";
            this.cxtServers.Size = new System.Drawing.Size(236, 48);
            this.cxtServers.Opening += new System.ComponentModel.CancelEventHandler(this.CxtServers_Opening);
            // 
            // itmViewLink
            // 
            this.itmViewLink.Name = "itmViewLink";
            this.itmViewLink.Size = new System.Drawing.Size(235, 22);
            this.itmViewLink.Text = "View Connection Link (Debug)";
            this.itmViewLink.Click += new System.EventHandler(this.ItmViewLink_Click);
            // 
            // itmViewAccountToken
            // 
            this.itmViewAccountToken.Name = "itmViewAccountToken";
            this.itmViewAccountToken.Size = new System.Drawing.Size(235, 22);
            this.itmViewAccountToken.Text = "View Account Token (Debug)";
            this.itmViewAccountToken.Click += new System.EventHandler(this.ItmViewAccountToken_Click);
            // 
            // dgvServers
            // 
            this.dgvServers.AllowUserToAddRows = false;
            this.dgvServers.AllowUserToDeleteRows = false;
            this.dgvServers.AllowUserToOrderColumns = true;
            this.dgvServers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvServers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServers.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvServers.IsContentTable = false;
            this.dgvServers.Location = new System.Drawing.Point(0, 24);
            this.dgvServers.MultiSelect = false;
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.RowHeadersVisible = false;
            this.dgvServers.RowsEmptyText = "No Servers Found";
            this.dgvServers.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServers.Size = new System.Drawing.Size(800, 426);
            this.dgvServers.TabIndex = 18;
            this.dgvServers.DoubleClick += new EventHandler(this.DgvServers_DoubleClick);
            // 
            // ServerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvServers);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Manager";
            this.Load += new System.EventHandler(this.ServerManager_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.cxtServers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuMain;
        private ToolStripMenuItem itmAuthenticate;
        private ToolStripMenuItem itmLoad;
        private ToolStripMenuItem itmServers;
        private ToolStripMenuItem itmRelays;
        private ToolStripMenuItem itmDirectConnection;
        private ToolStripMenuItem itmConnect;
        private ToolStripMenuItem itmViaToken;
        private ToolStripMenuItem itmViaPlexTv;
        private ToolStripMenuItem itmClearServers;
        private ToolStripMenuItem itmLocalLink;
        private ToolStripMenuItem itmRenderTokenColumn;
        private ContextMenuStrip cxtServers;
        private ToolStripMenuItem itmViewLink;
        private ToolStripMenuItem itmViewAccountToken;
        private FlatDataGridView dgvServers;
    }
}