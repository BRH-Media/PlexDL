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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNextTitle = new System.Windows.Forms.Button();
            this.btnSkipForward = new System.Windows.Forms.Button();
            this.btnSkipBack = new System.Windows.Forms.Button();
            this.btnPrevTitle = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.tlpPlayerControls = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
            this.tlpPlayerControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.BackColor = System.Drawing.Color.Black;
            this.pnlPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlayer.Location = new System.Drawing.Point(0, 0);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(1280, 771);
            this.pnlPlayer.TabIndex = 0;
            // 
            // tmrCopied
            // 
            this.tmrCopied.Interval = 1500;
            this.tmrCopied.Tick += new System.EventHandler(this.TmrCopied_Tick);
            // 
            // lblTotalDuration
            // 
            this.lblTotalDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTotalDuration.AutoSize = true;
            this.lblTotalDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTotalDuration.ForeColor = System.Drawing.Color.Black;
            this.lblTotalDuration.Location = new System.Drawing.Point(1173, 16);
            this.lblTotalDuration.Name = "lblTotalDuration";
            this.lblTotalDuration.Size = new System.Drawing.Size(64, 18);
            this.lblTotalDuration.TabIndex = 8;
            this.lblTotalDuration.Text = "00:00:00";
            // 
            // lblTimeSoFar
            // 
            this.lblTimeSoFar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTimeSoFar.AutoSize = true;
            this.lblTimeSoFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTimeSoFar.ForeColor = System.Drawing.Color.Black;
            this.lblTimeSoFar.Location = new System.Drawing.Point(253, 16);
            this.lblTimeSoFar.Name = "lblTimeSoFar";
            this.lblTimeSoFar.Size = new System.Drawing.Size(64, 18);
            this.lblTimeSoFar.TabIndex = 6;
            this.lblTimeSoFar.Text = "00:00:00";
            // 
            // trkDuration
            // 
            this.trkDuration.BackColor = this.BackColor;
            this.trkDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkDuration.Location = new System.Drawing.Point(323, 3);
            this.trkDuration.Maximum = 100;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(844, 45);
            this.trkDuration.TabIndex = 7;
            this.trkDuration.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExit.AutoSize = true;
            this.btnExit.BackgroundImage = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(1243, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 32);
            this.btnExit.TabIndex = 11;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnNextTitle
            // 
            this.btnNextTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextTitle.AutoSize = true;
            this.btnNextTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            this.btnNextTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNextTitle.Location = new System.Drawing.Point(211, 9);
            this.btnNextTitle.Name = "btnNextTitle";
            this.btnNextTitle.Size = new System.Drawing.Size(36, 32);
            this.btnNextTitle.TabIndex = 15;
            this.btnNextTitle.UseVisualStyleBackColor = true;
            this.btnNextTitle.Click += new System.EventHandler(this.BtnNextTitle_Click);
            // 
            // btnSkipForward
            // 
            this.btnSkipForward.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSkipForward.AutoSize = true;
            this.btnSkipForward.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            this.btnSkipForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipForward.Location = new System.Drawing.Point(169, 9);
            this.btnSkipForward.Name = "btnSkipForward";
            this.btnSkipForward.Size = new System.Drawing.Size(36, 32);
            this.btnSkipForward.TabIndex = 14;
            this.btnSkipForward.UseVisualStyleBackColor = true;
            this.btnSkipForward.Click += new System.EventHandler(this.BtnSkipForward_Click);
            // 
            // btnSkipBack
            // 
            this.btnSkipBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSkipBack.AutoSize = true;
            this.btnSkipBack.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            this.btnSkipBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipBack.Location = new System.Drawing.Point(127, 9);
            this.btnSkipBack.Name = "btnSkipBack";
            this.btnSkipBack.Size = new System.Drawing.Size(36, 32);
            this.btnSkipBack.TabIndex = 13;
            this.btnSkipBack.UseVisualStyleBackColor = true;
            this.btnSkipBack.Click += new System.EventHandler(this.BtnSkipBack_Click);
            // 
            // btnPrevTitle
            // 
            this.btnPrevTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrevTitle.AutoSize = true;
            this.btnPrevTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
            this.btnPrevTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevTitle.Location = new System.Drawing.Point(85, 9);
            this.btnPrevTitle.Name = "btnPrevTitle";
            this.btnPrevTitle.Size = new System.Drawing.Size(36, 32);
            this.btnPrevTitle.TabIndex = 12;
            this.btnPrevTitle.UseVisualStyleBackColor = true;
            this.btnPrevTitle.Click += new System.EventHandler(this.BtnPrevTitle_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStop.AutoSize = true;
            this.btnStop.BackgroundImage = global::PlexDL.Properties.Resources.baseline_stop_black_18dp;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Location = new System.Drawing.Point(45, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(34, 32);
            this.btnStop.TabIndex = 11;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlayPause.AutoSize = true;
            this.btnPlayPause.BackgroundImage = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            this.btnPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlayPause.Location = new System.Drawing.Point(3, 9);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(36, 32);
            this.btnPlayPause.TabIndex = 10;
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
            // 
            // tlpPlayerControls
            // 
            this.tlpPlayerControls.AutoSize = true;
            this.tlpPlayerControls.ColumnCount = 10;
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.Controls.Add(this.btnExit, 9, 0);
            this.tlpPlayerControls.Controls.Add(this.btnPlayPause, 0, 0);
            this.tlpPlayerControls.Controls.Add(this.lblTotalDuration, 8, 0);
            this.tlpPlayerControls.Controls.Add(this.trkDuration, 7, 0);
            this.tlpPlayerControls.Controls.Add(this.lblTimeSoFar, 6, 0);
            this.tlpPlayerControls.Controls.Add(this.btnNextTitle, 5, 0);
            this.tlpPlayerControls.Controls.Add(this.btnStop, 1, 0);
            this.tlpPlayerControls.Controls.Add(this.btnSkipForward, 4, 0);
            this.tlpPlayerControls.Controls.Add(this.btnPrevTitle, 2, 0);
            this.tlpPlayerControls.Controls.Add(this.btnSkipBack, 3, 0);
            this.tlpPlayerControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpPlayerControls.Location = new System.Drawing.Point(0, 720);
            this.tlpPlayerControls.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPlayerControls.Name = "tlpPlayerControls";
            this.tlpPlayerControls.RowCount = 1;
            this.tlpPlayerControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPlayerControls.Size = new System.Drawing.Size(1280, 51);
            this.tlpPlayerControls.TabIndex = 11;
            this.tlpPlayerControls.Paint += new System.Windows.Forms.PaintEventHandler(this.TlpPlayerControls_Paint);
            // 
            // Player
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 771);
            this.Controls.Add(this.tlpPlayerControls);
            this.Controls.Add(this.pnlPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unknown Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FrmPlayer_Load);
            this.Resize += new System.EventHandler(this.FrmPlayer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
            this.tlpPlayerControls.ResumeLayout(false);
            this.tlpPlayerControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel pnlPlayer;
        private Timer tmrCopied;
        private Label lblTotalDuration;
        private Label lblTimeSoFar;
        private Timer tmrRefreshUI;
        private TrackBar trkDuration;
        private Button btnPlayPause;
        private Button btnStop;
        private Button btnPrevTitle;
        private Button btnSkipBack;
        private Button btnSkipForward;
        private Button btnNextTitle;
        private Button btnExit;
        private TableLayoutPanel tlpPlayerControls;
    }
}