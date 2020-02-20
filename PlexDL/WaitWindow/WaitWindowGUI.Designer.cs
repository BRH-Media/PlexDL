namespace PlexDL.WaitWindow
{
    partial class WaitWindowGUI
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.MessageLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.Marquee = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.BackColor = System.Drawing.Color.White;
            this.MessageLabel.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11F);
            this.MessageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MessageLabel.Location = new System.Drawing.Point(12, 70);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(103, 19);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Updating Data";
            // 
            // Marquee
            // 
            this.Marquee.Location = new System.Drawing.Point(12, 90);
            this.Marquee.Name = "Marquee";
            this.Marquee.Size = new System.Drawing.Size(284, 23);
            this.Marquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Marquee.TabIndex = 3;
            // 
            // WaitWindowGUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(308, 125);
            this.Controls.Add(this.Marquee);
            this.Controls.Add(this.MessageLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WaitWindowGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please Wait";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.ControlBox = false;

        }
        public MetroSet_UI.Controls.MetroSetLabel MessageLabel;
        private System.Windows.Forms.ProgressBar Marquee;
    }
}
