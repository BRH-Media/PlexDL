namespace GitHubUpdater.UI
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
            this.browserChanges = new System.Windows.Forms.WebBrowser();
            this.btnDownloadUpdate = new System.Windows.Forms.Button();
            this.btnMaybeLater = new System.Windows.Forms.Button();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpUpdatedVersion = new System.Windows.Forms.TableLayoutPanel();
            this.lblUpdatedVersion = new System.Windows.Forms.Label();
            this.lblUpdatedVersionValue = new System.Windows.Forms.Label();
            this.lblUpdatedVersionStatus = new System.Windows.Forms.Label();
            this.tlpYourVersion = new System.Windows.Forms.TableLayoutPanel();
            this.lblYourVersion = new System.Windows.Forms.Label();
            this.lblYourVersionValue = new System.Windows.Forms.Label();
            this.lblYourlVersionStatus = new System.Windows.Forms.Label();
            this.gbUpdateChanges.SuspendLayout();
            this.tlpControls.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpUpdatedVersion.SuspendLayout();
            this.tlpYourVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUpdateTitle
            // 
            this.lblUpdateTitle.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lblUpdateTitle, 3);
            this.lblUpdateTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateTitle.Location = new System.Drawing.Point(3, 10);
            this.lblUpdateTitle.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.lblUpdateTitle.Name = "lblUpdateTitle";
            this.lblUpdateTitle.Size = new System.Drawing.Size(878, 24);
            this.lblUpdateTitle.TabIndex = 0;
            this.lblUpdateTitle.Text = "[UPDATE_TITLE]";
            this.lblUpdateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbUpdateChanges
            // 
            this.tlpMain.SetColumnSpan(this.gbUpdateChanges, 3);
            this.gbUpdateChanges.Controls.Add(this.browserChanges);
            this.gbUpdateChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUpdateChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUpdateChanges.Location = new System.Drawing.Point(3, 47);
            this.gbUpdateChanges.Name = "gbUpdateChanges";
            this.gbUpdateChanges.Size = new System.Drawing.Size(878, 349);
            this.gbUpdateChanges.TabIndex = 1;
            this.gbUpdateChanges.TabStop = false;
            this.gbUpdateChanges.Text = "Changes";
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
            this.browserChanges.Size = new System.Drawing.Size(872, 324);
            this.browserChanges.TabIndex = 0;
            this.browserChanges.WebBrowserShortcutsEnabled = false;
            // 
            // btnDownloadUpdate
            // 
            this.btnDownloadUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownloadUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadUpdate.Location = new System.Drawing.Point(119, 3);
            this.btnDownloadUpdate.Name = "btnDownloadUpdate";
            this.btnDownloadUpdate.Size = new System.Drawing.Size(168, 30);
            this.btnDownloadUpdate.TabIndex = 2;
            this.btnDownloadUpdate.Text = "Download Update";
            this.btnDownloadUpdate.UseVisualStyleBackColor = true;
            this.btnDownloadUpdate.Click += new System.EventHandler(this.BtnDownloadUpdate_Click);
            // 
            // btnMaybeLater
            // 
            this.btnMaybeLater.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMaybeLater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMaybeLater.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaybeLater.Location = new System.Drawing.Point(3, 3);
            this.btnMaybeLater.Name = "btnMaybeLater";
            this.btnMaybeLater.Size = new System.Drawing.Size(110, 30);
            this.btnMaybeLater.TabIndex = 3;
            this.btnMaybeLater.Text = "Maybe Later";
            this.btnMaybeLater.UseVisualStyleBackColor = true;
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 2;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpControls.Controls.Add(this.btnDownloadUpdate, 1, 0);
            this.tlpControls.Controls.Add(this.btnMaybeLater, 0, 0);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(591, 402);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 1;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.Size = new System.Drawing.Size(290, 36);
            this.tlpControls.TabIndex = 7;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.Controls.Add(this.tlpUpdatedVersion, 1, 2);
            this.tlpMain.Controls.Add(this.tlpControls, 2, 2);
            this.tlpMain.Controls.Add(this.gbUpdateChanges, 0, 1);
            this.tlpMain.Controls.Add(this.lblUpdateTitle, 0, 0);
            this.tlpMain.Controls.Add(this.tlpYourVersion, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(884, 441);
            this.tlpMain.TabIndex = 8;
            // 
            // tlpUpdatedVersion
            // 
            this.tlpUpdatedVersion.ColumnCount = 3;
            this.tlpUpdatedVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUpdatedVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUpdatedVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpUpdatedVersion.Controls.Add(this.lblUpdatedVersion, 0, 0);
            this.tlpUpdatedVersion.Controls.Add(this.lblUpdatedVersionValue, 1, 0);
            this.tlpUpdatedVersion.Controls.Add(this.lblUpdatedVersionStatus, 2, 0);
            this.tlpUpdatedVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUpdatedVersion.Location = new System.Drawing.Point(294, 399);
            this.tlpUpdatedVersion.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUpdatedVersion.Name = "tlpUpdatedVersion";
            this.tlpUpdatedVersion.RowCount = 1;
            this.tlpUpdatedVersion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUpdatedVersion.Size = new System.Drawing.Size(294, 42);
            this.tlpUpdatedVersion.TabIndex = 9;
            // 
            // lblUpdatedVersion
            // 
            this.lblUpdatedVersion.AutoSize = true;
            this.lblUpdatedVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdatedVersion.Location = new System.Drawing.Point(10, 3);
            this.lblUpdatedVersion.Margin = new System.Windows.Forms.Padding(10, 3, 3, 6);
            this.lblUpdatedVersion.Name = "lblUpdatedVersion";
            this.lblUpdatedVersion.Size = new System.Drawing.Size(89, 33);
            this.lblUpdatedVersion.TabIndex = 0;
            this.lblUpdatedVersion.Text = "Updated Version:";
            this.lblUpdatedVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUpdatedVersionValue
            // 
            this.lblUpdatedVersionValue.AutoSize = true;
            this.lblUpdatedVersionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdatedVersionValue.Location = new System.Drawing.Point(102, 3);
            this.lblUpdatedVersionValue.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
            this.lblUpdatedVersionValue.Name = "lblUpdatedVersionValue";
            this.lblUpdatedVersionValue.Size = new System.Drawing.Size(61, 33);
            this.lblUpdatedVersionValue.TabIndex = 1;
            this.lblUpdatedVersionValue.Text = "[VERSION]";
            this.lblUpdatedVersionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUpdatedVersionStatus
            // 
            this.lblUpdatedVersionStatus.AutoSize = true;
            this.lblUpdatedVersionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdatedVersionStatus.ForeColor = System.Drawing.Color.Chocolate;
            this.lblUpdatedVersionStatus.Location = new System.Drawing.Point(166, 3);
            this.lblUpdatedVersionStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
            this.lblUpdatedVersionStatus.Name = "lblUpdatedVersionStatus";
            this.lblUpdatedVersionStatus.Size = new System.Drawing.Size(125, 33);
            this.lblUpdatedVersionStatus.TabIndex = 2;
            this.lblUpdatedVersionStatus.Text = "[STATUS]";
            this.lblUpdatedVersionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpYourVersion
            // 
            this.tlpYourVersion.ColumnCount = 3;
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.Controls.Add(this.lblYourVersion, 0, 0);
            this.tlpYourVersion.Controls.Add(this.lblYourVersionValue, 1, 0);
            this.tlpYourVersion.Controls.Add(this.lblYourlVersionStatus, 2, 0);
            this.tlpYourVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYourVersion.Location = new System.Drawing.Point(0, 399);
            this.tlpYourVersion.Margin = new System.Windows.Forms.Padding(0);
            this.tlpYourVersion.Name = "tlpYourVersion";
            this.tlpYourVersion.RowCount = 1;
            this.tlpYourVersion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpYourVersion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpYourVersion.Size = new System.Drawing.Size(294, 42);
            this.tlpYourVersion.TabIndex = 8;
            // 
            // lblYourVersion
            // 
            this.lblYourVersion.AutoSize = true;
            this.lblYourVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYourVersion.Location = new System.Drawing.Point(10, 3);
            this.lblYourVersion.Margin = new System.Windows.Forms.Padding(10, 3, 3, 6);
            this.lblYourVersion.Name = "lblYourVersion";
            this.lblYourVersion.Size = new System.Drawing.Size(70, 33);
            this.lblYourVersion.TabIndex = 0;
            this.lblYourVersion.Text = "Your Version:";
            this.lblYourVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblYourVersionValue
            // 
            this.lblYourVersionValue.AutoSize = true;
            this.lblYourVersionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYourVersionValue.Location = new System.Drawing.Point(83, 3);
            this.lblYourVersionValue.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
            this.lblYourVersionValue.Name = "lblYourVersionValue";
            this.lblYourVersionValue.Size = new System.Drawing.Size(61, 33);
            this.lblYourVersionValue.TabIndex = 1;
            this.lblYourVersionValue.Text = "[VERSION]";
            this.lblYourVersionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYourlVersionStatus
            // 
            this.lblYourlVersionStatus.AutoSize = true;
            this.lblYourlVersionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYourlVersionStatus.ForeColor = System.Drawing.Color.Chocolate;
            this.lblYourlVersionStatus.Location = new System.Drawing.Point(147, 3);
            this.lblYourlVersionStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
            this.lblYourlVersionStatus.Name = "lblYourlVersionStatus";
            this.lblYourlVersionStatus.Size = new System.Drawing.Size(144, 33);
            this.lblYourlVersionStatus.TabIndex = 2;
            this.lblYourlVersionStatus.Text = "[STATUS]";
            this.lblYourlVersionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Update
            // 
            this.AcceptButton = this.btnDownloadUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 441);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Available";
            this.Load += new System.EventHandler(this.Update_Load);
            this.gbUpdateChanges.ResumeLayout(false);
            this.tlpControls.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpUpdatedVersion.ResumeLayout(false);
            this.tlpUpdatedVersion.PerformLayout();
            this.tlpYourVersion.ResumeLayout(false);
            this.tlpYourVersion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateTitle;
        private System.Windows.Forms.GroupBox gbUpdateChanges;
        private System.Windows.Forms.Button btnDownloadUpdate;
        private System.Windows.Forms.Button btnMaybeLater;
        private System.Windows.Forms.WebBrowser browserChanges;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpYourVersion;
        private System.Windows.Forms.Label lblYourVersion;
        private System.Windows.Forms.Label lblYourVersionValue;
        private System.Windows.Forms.Label lblYourlVersionStatus;
        private System.Windows.Forms.TableLayoutPanel tlpUpdatedVersion;
        private System.Windows.Forms.Label lblUpdatedVersion;
        private System.Windows.Forms.Label lblUpdatedVersionValue;
        private System.Windows.Forms.Label lblUpdatedVersionStatus;
    }
}