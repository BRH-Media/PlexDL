namespace GitHubUpdater
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.lblUpdateTitle = new System.Windows.Forms.Label();
            this.gbUpdateChanges = new System.Windows.Forms.GroupBox();
            this.btnDownloadUpdate = new System.Windows.Forms.Button();
            this.btnMaybeLater = new System.Windows.Forms.Button();
            this.browserChanges = new System.Windows.Forms.WebBrowser();
            this.lblDownloads = new System.Windows.Forms.Label();
            this.tlpDownloadCount = new System.Windows.Forms.TableLayoutPanel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblDownloadsValue = new System.Windows.Forms.Label();
            this.gbUpdateChanges.SuspendLayout();
            this.tlpDownloadCount.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUpdateTitle
            // 
            this.lblUpdateTitle.AutoSize = true;
            this.lblUpdateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateTitle.Location = new System.Drawing.Point(291, 9);
            this.lblUpdateTitle.Name = "lblUpdateTitle";
            this.lblUpdateTitle.Size = new System.Drawing.Size(157, 24);
            this.lblUpdateTitle.TabIndex = 0;
            this.lblUpdateTitle.Text = "[UPDATE_TITLE]";
            // 
            // gbUpdateChanges
            // 
            this.gbUpdateChanges.Controls.Add(this.browserChanges);
            this.gbUpdateChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUpdateChanges.Location = new System.Drawing.Point(12, 62);
            this.gbUpdateChanges.Name = "gbUpdateChanges";
            this.gbUpdateChanges.Size = new System.Drawing.Size(776, 267);
            this.gbUpdateChanges.TabIndex = 1;
            this.gbUpdateChanges.TabStop = false;
            this.gbUpdateChanges.Text = "Changes";
            // 
            // btnDownloadUpdate
            // 
            this.btnDownloadUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadUpdate.Location = new System.Drawing.Point(192, 3);
            this.btnDownloadUpdate.Name = "btnDownloadUpdate";
            this.btnDownloadUpdate.Size = new System.Drawing.Size(183, 28);
            this.btnDownloadUpdate.TabIndex = 2;
            this.btnDownloadUpdate.Text = "Download Update";
            this.btnDownloadUpdate.UseVisualStyleBackColor = true;
            this.btnDownloadUpdate.Click += new System.EventHandler(this.BtnDownloadUpdate_Click);
            // 
            // btnMaybeLater
            // 
            this.btnMaybeLater.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMaybeLater.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaybeLater.Location = new System.Drawing.Point(3, 3);
            this.btnMaybeLater.Name = "btnMaybeLater";
            this.btnMaybeLater.Size = new System.Drawing.Size(183, 28);
            this.btnMaybeLater.TabIndex = 3;
            this.btnMaybeLater.Text = "Maybe Later";
            this.btnMaybeLater.UseVisualStyleBackColor = true;
            this.btnMaybeLater.Click += new System.EventHandler(this.BtnMaybeLater_Click);
            // 
            // browserChanges
            // 
            this.browserChanges.AllowNavigation = false;
            this.browserChanges.AllowWebBrowserDrop = false;
            this.browserChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserChanges.IsWebBrowserContextMenuEnabled = false;
            this.browserChanges.Location = new System.Drawing.Point(3, 22);
            this.browserChanges.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserChanges.Name = "browserChanges";
            this.browserChanges.ScriptErrorsSuppressed = true;
            this.browserChanges.Size = new System.Drawing.Size(770, 242);
            this.browserChanges.TabIndex = 0;
            this.browserChanges.WebBrowserShortcutsEnabled = false;
            // 
            // lblDownloads
            // 
            this.lblDownloads.AutoSize = true;
            this.lblDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloads.Location = new System.Drawing.Point(3, 0);
            this.lblDownloads.Name = "lblDownloads";
            this.lblDownloads.Size = new System.Drawing.Size(126, 28);
            this.lblDownloads.TabIndex = 4;
            this.lblDownloads.Text = "Version download count:";
            this.lblDownloads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpDownloadCount
            // 
            this.tlpDownloadCount.ColumnCount = 2;
            this.tlpDownloadCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDownloadCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDownloadCount.Controls.Add(this.lblDownloadsValue, 1, 0);
            this.tlpDownloadCount.Controls.Add(this.lblDownloads, 0, 0);
            this.tlpDownloadCount.Location = new System.Drawing.Point(12, 335);
            this.tlpDownloadCount.Name = "tlpDownloadCount";
            this.tlpDownloadCount.RowCount = 1;
            this.tlpDownloadCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDownloadCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDownloadCount.Size = new System.Drawing.Size(265, 28);
            this.tlpDownloadCount.TabIndex = 5;
            this.tlpDownloadCount.Visible = false;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnMaybeLater);
            this.pnlControls.Controls.Add(this.btnDownloadUpdate);
            this.pnlControls.Location = new System.Drawing.Point(410, 332);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(378, 34);
            this.pnlControls.TabIndex = 6;
            // 
            // lblDownloadsValue
            // 
            this.lblDownloadsValue.AutoSize = true;
            this.lblDownloadsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloadsValue.Location = new System.Drawing.Point(135, 0);
            this.lblDownloadsValue.Name = "lblDownloadsValue";
            this.lblDownloadsValue.Size = new System.Drawing.Size(127, 28);
            this.lblDownloadsValue.TabIndex = 5;
            this.lblDownloadsValue.Text = "0";
            this.lblDownloadsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 372);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.tlpDownloadCount);
            this.Controls.Add(this.gbUpdateChanges);
            this.Controls.Add(this.lblUpdateTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Available";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Update_Load);
            this.gbUpdateChanges.ResumeLayout(false);
            this.tlpDownloadCount.ResumeLayout(false);
            this.tlpDownloadCount.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateTitle;
        private System.Windows.Forms.GroupBox gbUpdateChanges;
        private System.Windows.Forms.Button btnDownloadUpdate;
        private System.Windows.Forms.Button btnMaybeLater;
        private System.Windows.Forms.WebBrowser browserChanges;
        private System.Windows.Forms.Label lblDownloads;
        private System.Windows.Forms.TableLayoutPanel tlpDownloadCount;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label lblDownloadsValue;
    }
}