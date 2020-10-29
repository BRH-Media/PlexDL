using System.ComponentModel;
using System.Windows.Forms;
using PlexDL.Common.Components;

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
            this.btnSkipBack = new System.Windows.Forms.Button();
            this.btnPrevTitle = new System.Windows.Forms.Button();
            this.btnSkipForward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnNextTitle = new System.Windows.Forms.Button();
            this.lblTimeSoFar = new System.Windows.Forms.Label();
            this.trkDuration = new System.Windows.Forms.TrackBar();
            this.lblTotalDuration = new System.Windows.Forms.Label();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpPlayerControls = new System.Windows.Forms.TableLayoutPanel();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.trkVolume = new System.Windows.Forms.TrackBar();
            this.tlpMaster = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.picFramerate = new System.Windows.Forms.PictureBox();
            this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.tmrFramerate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
            this.tlpPlayerControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).BeginInit();
            this.tlpMaster.SuspendLayout();
            this.pnlPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFramerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmpMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSkipBack
            // 
            this.btnSkipBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSkipBack.AutoSize = true;
            this.btnSkipBack.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            this.btnSkipBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipBack.Location = new System.Drawing.Point(117, 9);
            this.btnSkipBack.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSkipBack.Name = "btnSkipBack";
            this.btnSkipBack.Size = new System.Drawing.Size(32, 32);
            this.btnSkipBack.TabIndex = 3;
            this.tipMain.SetToolTip(this.btnSkipBack, "Step Backwards");
            this.btnSkipBack.UseVisualStyleBackColor = true;
            this.btnSkipBack.Click += new System.EventHandler(this.BtnSkipBack_Click);
            // 
            // btnPrevTitle
            // 
            this.btnPrevTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrevTitle.AutoSize = true;
            this.btnPrevTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
            this.btnPrevTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrevTitle.Location = new System.Drawing.Point(79, 9);
            this.btnPrevTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrevTitle.Name = "btnPrevTitle";
            this.btnPrevTitle.Size = new System.Drawing.Size(32, 32);
            this.btnPrevTitle.TabIndex = 2;
            this.tipMain.SetToolTip(this.btnPrevTitle, "Previous Title");
            this.btnPrevTitle.UseVisualStyleBackColor = true;
            this.btnPrevTitle.Click += new System.EventHandler(this.BtnPrevTitle_Click);
            // 
            // btnSkipForward
            // 
            this.btnSkipForward.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSkipForward.AutoSize = true;
            this.btnSkipForward.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            this.btnSkipForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSkipForward.Location = new System.Drawing.Point(155, 9);
            this.btnSkipForward.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSkipForward.Name = "btnSkipForward";
            this.btnSkipForward.Size = new System.Drawing.Size(32, 32);
            this.btnSkipForward.TabIndex = 4;
            this.tipMain.SetToolTip(this.btnSkipForward, "Step Forwards");
            this.btnSkipForward.UseVisualStyleBackColor = true;
            this.btnSkipForward.Click += new System.EventHandler(this.BtnSkipForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStop.AutoSize = true;
            this.btnStop.BackgroundImage = global::PlexDL.Properties.Resources.baseline_stop_black_18dp;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(41, 9);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 1;
            this.tipMain.SetToolTip(this.btnStop, "Stop Playback");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnNextTitle
            // 
            this.btnNextTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextTitle.AutoSize = true;
            this.btnNextTitle.BackgroundImage = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            this.btnNextTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNextTitle.Location = new System.Drawing.Point(193, 9);
            this.btnNextTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnNextTitle.Name = "btnNextTitle";
            this.btnNextTitle.Size = new System.Drawing.Size(32, 32);
            this.btnNextTitle.TabIndex = 5;
            this.tipMain.SetToolTip(this.btnNextTitle, "Next Title");
            this.btnNextTitle.UseVisualStyleBackColor = true;
            this.btnNextTitle.Click += new System.EventHandler(this.BtnNextTitle_Click);
            // 
            // lblTimeSoFar
            // 
            this.lblTimeSoFar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTimeSoFar.AutoSize = true;
            this.lblTimeSoFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTimeSoFar.ForeColor = System.Drawing.Color.Black;
            this.lblTimeSoFar.Location = new System.Drawing.Point(231, 16);
            this.lblTimeSoFar.Name = "lblTimeSoFar";
            this.lblTimeSoFar.Size = new System.Drawing.Size(64, 18);
            this.lblTimeSoFar.TabIndex = 6;
            this.lblTimeSoFar.Text = "00:00:00";
            this.tipMain.SetToolTip(this.lblTimeSoFar, "Time Passed");
            // 
            // trkDuration
            // 
            this.trkDuration.BackColor = this.BackColor;
            this.trkDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkDuration.Location = new System.Drawing.Point(298, 0);
            this.trkDuration.Margin = new System.Windows.Forms.Padding(0);
            this.trkDuration.Maximum = 100;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(675, 51);
            this.trkDuration.TabIndex = 7;
            this.trkDuration.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tipMain.SetToolTip(this.trkDuration, "Seek");
            // 
            // lblTotalDuration
            // 
            this.lblTotalDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTotalDuration.AutoSize = true;
            this.lblTotalDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTotalDuration.ForeColor = System.Drawing.Color.Black;
            this.lblTotalDuration.Location = new System.Drawing.Point(976, 16);
            this.lblTotalDuration.Name = "lblTotalDuration";
            this.lblTotalDuration.Size = new System.Drawing.Size(64, 18);
            this.lblTotalDuration.TabIndex = 8;
            this.lblTotalDuration.Text = "00:00:00";
            this.tipMain.SetToolTip(this.lblTotalDuration, "Time Remaining");
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlayPause.AutoSize = true;
            this.btnPlayPause.BackgroundImage = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            this.btnPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlayPause.Location = new System.Drawing.Point(3, 9);
            this.btnPlayPause.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(32, 32);
            this.btnPlayPause.TabIndex = 0;
            this.tipMain.SetToolTip(this.btnPlayPause, "Play Title");
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.AutoSize = true;
            this.btnExit.BackgroundImage = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(1245, 9);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 12;
            this.tipMain.SetToolTip(this.btnExit, "Stop Playback and Quit");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // tlpPlayerControls
            // 
            this.tlpPlayerControls.AutoSize = true;
            this.tlpPlayerControls.ColumnCount = 13;
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
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPlayerControls.Controls.Add(this.lblVolume, 9, 0);
            this.tlpPlayerControls.Controls.Add(this.btnFullScreen, 11, 0);
            this.tlpPlayerControls.Controls.Add(this.btnExit, 12, 0);
            this.tlpPlayerControls.Controls.Add(this.btnPlayPause, 0, 0);
            this.tlpPlayerControls.Controls.Add(this.lblTotalDuration, 8, 0);
            this.tlpPlayerControls.Controls.Add(this.trkDuration, 7, 0);
            this.tlpPlayerControls.Controls.Add(this.lblTimeSoFar, 6, 0);
            this.tlpPlayerControls.Controls.Add(this.btnNextTitle, 5, 0);
            this.tlpPlayerControls.Controls.Add(this.btnStop, 1, 0);
            this.tlpPlayerControls.Controls.Add(this.btnSkipForward, 4, 0);
            this.tlpPlayerControls.Controls.Add(this.btnPrevTitle, 2, 0);
            this.tlpPlayerControls.Controls.Add(this.btnSkipBack, 3, 0);
            this.tlpPlayerControls.Controls.Add(this.trkVolume, 9, 0);
            this.tlpPlayerControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPlayerControls.Location = new System.Drawing.Point(0, 720);
            this.tlpPlayerControls.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPlayerControls.Name = "tlpPlayerControls";
            this.tlpPlayerControls.RowCount = 1;
            this.tlpPlayerControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPlayerControls.Size = new System.Drawing.Size(1280, 51);
            this.tlpPlayerControls.TabIndex = 11;
            // 
            // lblVolume
            // 
            this.lblVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblVolume.ForeColor = System.Drawing.Color.Black;
            this.lblVolume.Location = new System.Drawing.Point(1156, 16);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(45, 18);
            this.lblVolume.TabIndex = 10;
            this.lblVolume.Text = "100%";
            this.tipMain.SetToolTip(this.lblVolume, "Audio Volume");
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFullScreen.AutoSize = true;
            this.btnFullScreen.BackgroundImage = global::PlexDL.Properties.Resources.baseline_fullscreen_black_18dp;
            this.btnFullScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFullScreen.Location = new System.Drawing.Point(1207, 9);
            this.btnFullScreen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(32, 32);
            this.btnFullScreen.TabIndex = 11;
            this.tipMain.SetToolTip(this.btnFullScreen, "Fullscreen Toggle");
            this.btnFullScreen.UseVisualStyleBackColor = true;
            this.btnFullScreen.Click += new System.EventHandler(this.BtnFullScreen_Click);
            // 
            // trkVolume
            // 
            this.trkVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkVolume.Location = new System.Drawing.Point(1046, 3);
            this.trkVolume.Maximum = 100;
            this.trkVolume.Name = "trkVolume";
            this.trkVolume.Size = new System.Drawing.Size(104, 45);
            this.trkVolume.TabIndex = 9;
            this.trkVolume.TickFrequency = 10;
            this.trkVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tipMain.SetToolTip(this.trkVolume, "Audio Volume");
            this.trkVolume.Value = 100;
            this.trkVolume.Scroll += new System.EventHandler(this.TrkVolume_Scroll);
            // 
            // tlpMaster
            // 
            this.tlpMaster.ColumnCount = 1;
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaster.Controls.Add(this.pnlPlayer, 0, 0);
            this.tlpMaster.Controls.Add(this.tlpPlayerControls, 0, 1);
            this.tlpMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMaster.Location = new System.Drawing.Point(0, 0);
            this.tlpMaster.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMaster.Name = "tlpMaster";
            this.tlpMaster.RowCount = 2;
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMaster.Size = new System.Drawing.Size(1280, 771);
            this.tlpMaster.TabIndex = 12;
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.BackColor = System.Drawing.Color.Black;
            this.pnlPlayer.Controls.Add(this.picFramerate);
            this.pnlPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlayer.Location = new System.Drawing.Point(0, 0);
            this.pnlPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(1280, 720);
            this.pnlPlayer.TabIndex = 0;
            // 
            // picFramerate
            // 
            this.picFramerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFramerate.Location = new System.Drawing.Point(0, 0);
            this.picFramerate.Name = "picFramerate";
            this.picFramerate.Size = new System.Drawing.Size(100, 50);
            this.picFramerate.TabIndex = 0;
            this.picFramerate.TabStop = false;
            this.picFramerate.Visible = false;
            // 
            // wmpMain
            // 
            this.wmpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmpMain.Enabled = true;
            this.wmpMain.Location = new System.Drawing.Point(0, 0);
            this.wmpMain.Name = "wmpMain";
            this.wmpMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMain.OcxState")));
            this.wmpMain.Size = new System.Drawing.Size(1280, 771);
            this.wmpMain.TabIndex = 0;
            // 
            // tmrFramerate
            // 
            this.tmrFramerate.Interval = 1000;
            this.tmrFramerate.Tick += new System.EventHandler(this.TmrFramerate_Tick);
            // 
            // Player
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 771);
            this.Controls.Add(this.tlpMaster);
            this.Controls.Add(this.wmpMain);
            this.DoubleBufferedHack = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unknown Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FrmPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
            this.tlpPlayerControls.ResumeLayout(false);
            this.tlpPlayerControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).EndInit();
            this.tlpMaster.ResumeLayout(false);
            this.tlpMaster.PerformLayout();
            this.pnlPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFramerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wmpMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnSkipBack;
        private Button btnPrevTitle;
        private Button btnSkipForward;
        private Button btnStop;
        private Button btnNextTitle;
        private Label lblTimeSoFar;
        private TrackBar trkDuration;
        private TableLayoutPanel tlpPlayerControls;
        private Button btnExit;
        private Button btnPlayPause;
        private Label lblTotalDuration;
        private TableLayoutPanel tlpMaster;
        private Button btnFullScreen;
        private AxWMPLib.AxWindowsMediaPlayer wmpMain;
        private TrackBar trkVolume;
        private Label lblVolume;
        private ToolTip tipMain;
        private Panel pnlPlayer;
        private PictureBox picFramerate;
        private Timer tmrFramerate;
    }
}