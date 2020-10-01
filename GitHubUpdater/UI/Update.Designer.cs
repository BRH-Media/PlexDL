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
            this.tlpYourVersion = new System.Windows.Forms.TableLayoutPanel();
            this.lblYourVersion = new System.Windows.Forms.Label();
            this.lblYourVersionValue = new System.Windows.Forms.Label();
            this.lblVersionStatus = new System.Windows.Forms.Label();
            this.gbUpdateChanges.SuspendLayout();
            this.tlpControls.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpYourVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUpdateTitle
            // 
            this.lblUpdateTitle.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lblUpdateTitle, 2);
            this.lblUpdateTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateTitle.Location = new System.Drawing.Point(3, 10);
            this.lblUpdateTitle.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.lblUpdateTitle.Name = "lblUpdateTitle";
            this.lblUpdateTitle.Size = new System.Drawing.Size(794, 24);
            this.lblUpdateTitle.TabIndex = 0;
            this.lblUpdateTitle.Text = "[UPDATE_TITLE]";
            this.lblUpdateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbUpdateChanges
            // 
            this.tlpMain.SetColumnSpan(this.gbUpdateChanges, 2);
            this.gbUpdateChanges.Controls.Add(this.browserChanges);
            this.gbUpdateChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUpdateChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUpdateChanges.Location = new System.Drawing.Point(3, 47);
            this.gbUpdateChanges.Name = "gbUpdateChanges";
            this.gbUpdateChanges.Size = new System.Drawing.Size(794, 285);
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
            this.browserChanges.Size = new System.Drawing.Size(788, 260);
            this.browserChanges.TabIndex = 0;
            this.browserChanges.WebBrowserShortcutsEnabled = false;
            // 
            // btnDownloadUpdate
            // 
            this.btnDownloadUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownloadUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadUpdate.Location = new System.Drawing.Point(192, 3);
            this.btnDownloadUpdate.Name = "btnDownloadUpdate";
            this.btnDownloadUpdate.Size = new System.Drawing.Size(199, 30);
            this.btnDownloadUpdate.TabIndex = 2;
            this.btnDownloadUpdate.Text = "Download Update";
            this.btnDownloadUpdate.UseVisualStyleBackColor = true;
            this.btnDownloadUpdate.Click += new System.EventHandler(this.BtnDownloadUpdate_Click);
            // 
            // btnMaybeLater
            // 
            this.btnMaybeLater.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMaybeLater.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMaybeLater.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaybeLater.Location = new System.Drawing.Point(3, 3);
            this.btnMaybeLater.Name = "btnMaybeLater";
            this.btnMaybeLater.Size = new System.Drawing.Size(183, 30);
            this.btnMaybeLater.TabIndex = 3;
            this.btnMaybeLater.Text = "Maybe Later";
            this.btnMaybeLater.UseVisualStyleBackColor = true;
            this.btnMaybeLater.Click += new System.EventHandler(this.BtnMaybeLater_Click);
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 2;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpControls.Controls.Add(this.btnDownloadUpdate, 1, 0);
            this.tlpControls.Controls.Add(this.btnMaybeLater, 0, 0);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(403, 338);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 1;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.Size = new System.Drawing.Size(394, 36);
            this.tlpControls.TabIndex = 7;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.tlpControls, 1, 2);
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
            this.tlpMain.Size = new System.Drawing.Size(800, 377);
            this.tlpMain.TabIndex = 8;
            // 
            // tlpYourVersion
            // 
            this.tlpYourVersion.ColumnCount = 3;
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpYourVersion.Controls.Add(this.lblYourVersion, 0, 0);
            this.tlpYourVersion.Controls.Add(this.lblYourVersionValue, 1, 0);
            this.tlpYourVersion.Controls.Add(this.lblVersionStatus, 2, 0);
            this.tlpYourVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYourVersion.Location = new System.Drawing.Point(0, 335);
            this.tlpYourVersion.Margin = new System.Windows.Forms.Padding(0);
            this.tlpYourVersion.Name = "tlpYourVersion";
            this.tlpYourVersion.RowCount = 1;
            this.tlpYourVersion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpYourVersion.Size = new System.Drawing.Size(400, 42);
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
            // lblVersionStatus
            // 
            this.lblVersionStatus.AutoSize = true;
            this.lblVersionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersionStatus.ForeColor = System.Drawing.Color.Chocolate;
            this.lblVersionStatus.Location = new System.Drawing.Point(147, 3);
            this.lblVersionStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
            this.lblVersionStatus.Name = "lblVersionStatus";
            this.lblVersionStatus.Size = new System.Drawing.Size(250, 33);
            this.lblVersionStatus.TabIndex = 2;
            this.lblVersionStatus.Text = "[STATUS]";
            this.lblVersionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 377);
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
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Update_Load);
            this.gbUpdateChanges.ResumeLayout(false);
            this.tlpControls.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
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
        private System.Windows.Forms.Label lblVersionStatus;
    }
}