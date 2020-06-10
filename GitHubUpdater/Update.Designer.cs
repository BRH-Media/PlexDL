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
            this.txtUpdateChanges = new System.Windows.Forms.RichTextBox();
            this.btnDownloadUpdate = new System.Windows.Forms.Button();
            this.btnMaybeLater = new System.Windows.Forms.Button();
            this.gbUpdateChanges.SuspendLayout();
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
            this.gbUpdateChanges.Controls.Add(this.txtUpdateChanges);
            this.gbUpdateChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUpdateChanges.Location = new System.Drawing.Point(12, 62);
            this.gbUpdateChanges.Name = "gbUpdateChanges";
            this.gbUpdateChanges.Size = new System.Drawing.Size(776, 267);
            this.gbUpdateChanges.TabIndex = 1;
            this.gbUpdateChanges.TabStop = false;
            this.gbUpdateChanges.Text = "Changes";
            // 
            // txtUpdateChanges
            // 
            this.txtUpdateChanges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpdateChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateChanges.Location = new System.Drawing.Point(3, 22);
            this.txtUpdateChanges.Name = "txtUpdateChanges";
            this.txtUpdateChanges.ReadOnly = true;
            this.txtUpdateChanges.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtUpdateChanges.Size = new System.Drawing.Size(770, 242);
            this.txtUpdateChanges.TabIndex = 0;
            this.txtUpdateChanges.Text = "Couldn\'t get the update\'s body-text";
            // 
            // btnDownloadUpdate
            // 
            this.btnDownloadUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadUpdate.Location = new System.Drawing.Point(605, 335);
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
            this.btnMaybeLater.Location = new System.Drawing.Point(416, 335);
            this.btnMaybeLater.Name = "btnMaybeLater";
            this.btnMaybeLater.Size = new System.Drawing.Size(183, 28);
            this.btnMaybeLater.TabIndex = 3;
            this.btnMaybeLater.Text = "Maybe Later";
            this.btnMaybeLater.UseVisualStyleBackColor = true;
            this.btnMaybeLater.Click += new System.EventHandler(this.BtnMaybeLater_Click);
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 372);
            this.ControlBox = false;
            this.Controls.Add(this.btnMaybeLater);
            this.Controls.Add(this.btnDownloadUpdate);
            this.Controls.Add(this.gbUpdateChanges);
            this.Controls.Add(this.lblUpdateTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Available!";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Update_Load);
            this.gbUpdateChanges.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateTitle;
        private System.Windows.Forms.GroupBox gbUpdateChanges;
        private System.Windows.Forms.RichTextBox txtUpdateChanges;
        private System.Windows.Forms.Button btnDownloadUpdate;
        private System.Windows.Forms.Button btnMaybeLater;
    }
}