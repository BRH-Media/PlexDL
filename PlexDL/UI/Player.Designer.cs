using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.tmrCopied = new System.Windows.Forms.Timer(this.components);
            this.lblTotalDuration = new System.Windows.Forms.Label();
            this.lblTimeSoFar = new System.Windows.Forms.Label();
            this.tmrRefreshUI = new System.Windows.Forms.Timer(this.components);
            this.trkDuration = new System.Windows.Forms.TrackBar();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPrevTitle = new System.Windows.Forms.Button();
            this.btnSkipBack = new System.Windows.Forms.Button();
            this.btnSkipForward = new System.Windows.Forms.Button();
            this.btnNextTitle = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.BackColor = System.Drawing.Color.Black;
            this.pnlPlayer.Location = new System.Drawing.Point(0, 0);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(1280, 720);
            this.pnlPlayer.TabIndex = 0;
            // 
            // tmrCopied
            // 
            this.tmrCopied.Interval = 1500;
            this.tmrCopied.Tick += new System.EventHandler(this.TmrCopied_Tick);
            // 
            // lblTotalDuration
            // 
            this.lblTotalDuration.AutoSize = true;
            this.lblTotalDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTotalDuration.ForeColor = System.Drawing.Color.Black;
            this.lblTotalDuration.Location = new System.Drawing.Point(1164, 17);
            this.lblTotalDuration.Name = "lblTotalDuration";
            this.lblTotalDuration.Size = new System.Drawing.Size(64, 18);
            this.lblTotalDuration.TabIndex = 8;
            this.lblTotalDuration.Text = "00:00:00";
            // 
            // lblTimeSoFar
            // 
            this.lblTimeSoFar.AutoSize = true;
            this.lblTimeSoFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTimeSoFar.ForeColor = System.Drawing.Color.Black;
            this.lblTimeSoFar.Location = new System.Drawing.Point(274, 17);
            this.lblTimeSoFar.Name = "lblTimeSoFar";
            this.lblTimeSoFar.Size = new System.Drawing.Size(64, 18);
            this.lblTimeSoFar.TabIndex = 6;
            this.lblTimeSoFar.Text = "00:00:00";
            // 
            // trkDuration
            // 
            this.trkDuration.AutoSize = false;
            this.trkDuration.BackColor = this.BackColor;
            this.trkDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkDuration.Location = new System.Drawing.Point(344, 17);
            this.trkDuration.Maximum = 100;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(814, 16);
            this.trkDuration.TabIndex = 7;
            this.trkDuration.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnExit);
            this.pnlControls.Controls.Add(this.btnNextTitle);
            this.pnlControls.Controls.Add(this.btnSkipForward);
            this.pnlControls.Controls.Add(this.btnSkipBack);
            this.pnlControls.Controls.Add(this.btnPrevTitle);
            this.pnlControls.Controls.Add(this.btnStop);
            this.pnlControls.Controls.Add(this.btnPlayPause);
            this.pnlControls.Controls.Add(this.lblTimeSoFar);
            this.pnlControls.Controls.Add(this.trkDuration);
            this.pnlControls.Controls.Add(this.lblTotalDuration);
            this.pnlControls.Location = new System.Drawing.Point(0, 720);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1280, 50);
            this.pnlControls.TabIndex = 10;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.BackgroundImage = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            this.btnPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlayPause.Location = new System.Drawing.Point(10, 9);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(36, 32);
            this.btnPlayPause.TabIndex = 10;
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::PlexDL.Properties.Resources.baseline_stop_black_18dp;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Location = new System.Drawing.Point(54, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(34, 32);
            this.btnStop.TabIndex = 11;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPrevTitle
            // 
            this.btnPrevTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
            this.btnPrevTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevTitle.Location = new System.Drawing.Point(98, 9);
            this.btnPrevTitle.Name = "btnPrevTitle";
            this.btnPrevTitle.Size = new System.Drawing.Size(36, 32);
            this.btnPrevTitle.TabIndex = 12;
            this.btnPrevTitle.UseVisualStyleBackColor = true;
            this.btnPrevTitle.Click += new System.EventHandler(this.btnPrevTitle_Click);
            // 
            // btnSkipBack
            // 
            this.btnSkipBack.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            this.btnSkipBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipBack.Location = new System.Drawing.Point(142, 9);
            this.btnSkipBack.Name = "btnSkipBack";
            this.btnSkipBack.Size = new System.Drawing.Size(36, 32);
            this.btnSkipBack.TabIndex = 13;
            this.btnSkipBack.UseVisualStyleBackColor = true;
            this.btnSkipBack.Click += new System.EventHandler(this.btnSkipBack_Click);
            // 
            // btnSkipForward
            // 
            this.btnSkipForward.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            this.btnSkipForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipForward.Location = new System.Drawing.Point(186, 9);
            this.btnSkipForward.Name = "btnSkipForward";
            this.btnSkipForward.Size = new System.Drawing.Size(36, 32);
            this.btnSkipForward.TabIndex = 14;
            this.btnSkipForward.UseVisualStyleBackColor = true;
            this.btnSkipForward.Click += new System.EventHandler(this.btnSkipForward_Click);
            // 
            // btnNextTitle
            // 
            this.btnNextTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            this.btnNextTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNextTitle.Location = new System.Drawing.Point(230, 9);
            this.btnNextTitle.Name = "btnNextTitle";
            this.btnNextTitle.Size = new System.Drawing.Size(36, 32);
            this.btnNextTitle.TabIndex = 15;
            this.btnNextTitle.UseVisualStyleBackColor = true;
            this.btnNextTitle.Click += new System.EventHandler(this.btnNextTitle_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(1234, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 32);
            this.btnExit.TabIndex = 11;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Player
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 771);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unknown Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FrmPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlPlayer;
        private Timer tmrCopied;
        private Label lblTotalDuration;
        private Label lblTimeSoFar;
        private Timer tmrRefreshUI;
        private TrackBar trkDuration;
        private Panel pnlControls;
        private Button btnPlayPause;
        private Button btnStop;
        private Button btnPrevTitle;
        private Button btnSkipBack;
        private Button btnSkipForward;
        private Button btnNextTitle;
        private Button btnExit;
    }
}