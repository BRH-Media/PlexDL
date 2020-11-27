namespace PlexDL.UI.Forms
{
    partial class Cast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cast));
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.gbStreamControl = new System.Windows.Forms.GroupBox();
            this.lstDevices = new System.Windows.Forms.ListBox();
            this.btnDiscover = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnCast = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.gbStreamControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.SystemColors.Control;
            this.picPoster.BackgroundImage = global::PlexDL.ResourceProvider.Properties.Resources.image_not_available_png_8;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPoster.Location = new System.Drawing.Point(8, 8);
            this.picPoster.Margin = new System.Windows.Forms.Padding(2);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(266, 232);
            this.picPoster.TabIndex = 13;
            this.picPoster.TabStop = false;
            // 
            // gbStreamControl
            // 
            this.gbStreamControl.Controls.Add(this.lstDevices);
            this.gbStreamControl.Controls.Add(this.btnDiscover);
            this.gbStreamControl.Controls.Add(this.btnPlayPause);
            this.gbStreamControl.Controls.Add(this.btnCast);
            this.gbStreamControl.Location = new System.Drawing.Point(279, 111);
            this.gbStreamControl.Name = "gbStreamControl";
            this.gbStreamControl.Size = new System.Drawing.Size(509, 94);
            this.gbStreamControl.TabIndex = 14;
            this.gbStreamControl.TabStop = false;
            this.gbStreamControl.Text = "Stream Control";
            // 
            // lstDevices
            // 
            this.lstDevices.FormattingEnabled = true;
            this.lstDevices.Location = new System.Drawing.Point(249, 19);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.ScrollAlwaysVisible = true;
            this.lstDevices.Size = new System.Drawing.Size(254, 69);
            this.lstDevices.TabIndex = 3;
            this.lstDevices.SelectedIndexChanged += new System.EventHandler(this.LstDevices_SelectedIndexChanged);
            // 
            // btnDiscover
            // 
            this.btnDiscover.Location = new System.Drawing.Point(168, 19);
            this.btnDiscover.Name = "btnDiscover";
            this.btnDiscover.Size = new System.Drawing.Size(75, 69);
            this.btnDiscover.TabIndex = 2;
            this.btnDiscover.Text = "Discover";
            this.btnDiscover.UseVisualStyleBackColor = true;
            this.btnDiscover.Click += new System.EventHandler(this.BtnDiscover_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Enabled = false;
            this.btnPlayPause.Location = new System.Drawing.Point(87, 19);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(75, 69);
            this.btnPlayPause.TabIndex = 1;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
            // 
            // btnCast
            // 
            this.btnCast.Enabled = false;
            this.btnCast.Location = new System.Drawing.Point(6, 19);
            this.btnCast.Name = "btnCast";
            this.btnCast.Size = new System.Drawing.Size(75, 69);
            this.btnCast.TabIndex = 0;
            this.btnCast.Text = "Start Casting";
            this.btnCast.UseVisualStyleBackColor = true;
            this.btnCast.Click += new System.EventHandler(this.BtnCast_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(279, 211);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(509, 29);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(279, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(509, 99);
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "[TITLE]";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Cast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 248);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gbStreamControl);
            this.Controls.Add(this.picPoster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cast";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cast";
            this.Load += new System.EventHandler(this.Cast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.gbStreamControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.GroupBox gbStreamControl;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCast;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnDiscover;
        private System.Windows.Forms.ListBox lstDevices;
    }
}