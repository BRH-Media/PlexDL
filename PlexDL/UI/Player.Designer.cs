using MetroSet_UI.Extensions;
namespace PlexDL.UI
{
    partial class Player
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
            this.components = new System.ComponentModel.Container();
            MetroSet_UI.Extensions.ImageSet imageSet1 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet2 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet3 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet4 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet5 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet6 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet7 = new MetroSet_UI.Extensions.ImageSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.btnExit = new MetroSet_UI.Controls.MetroSetEllipse();
            this.tmrCopied = new System.Windows.Forms.Timer(this.components);
            this.lblTotalDuration = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblTimeSoFar = new MetroSet_UI.Controls.MetroSetLabel();
            this.tmrRefreshUI = new System.Windows.Forms.Timer(this.components);
            this.trkDuration = new MetroSet_UI.Controls.MetroSetTrackBar();
            this.btnPrevTitle = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnNextTitle = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnStop = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnSkipForward = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnSkipBack = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnPlayPause = new MetroSet_UI.Controls.MetroSetEllipse();
            this.pnlControls = new MetroSet_UI.Controls.MetroSetPanel();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.BackColor = System.Drawing.Color.Black;
            this.pnlPlayer.Location = new System.Drawing.Point(0, 75);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(1280, 720);
            this.pnlPlayer.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BorderThickness = 0;
            this.btnExit.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnExit.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverTextColor = System.Drawing.Color.White;
            this.btnExit.Image = null;
            imageSet7.Idle = PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            imageSet7.Focus = PlexDL.Properties.Resources.baseline_cancel_black_18dp_white;
            this.btnExit.ImageSet = imageSet7;
            this.btnExit.ImageSize = new System.Drawing.Size(28, 28);
            this.btnExit.Location = new System.Drawing.Point(1220, 7);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnExit.NormalTextColor = System.Drawing.Color.Black;
            this.btnExit.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressTextColor = System.Drawing.Color.White;
            this.btnExit.Size = new System.Drawing.Size(44, 36);
            this.btnExit.Style = MetroSet_UI.Design.Style.Light;
            this.btnExit.StyleManager = null;
            this.btnExit.TabIndex = 9;
            this.btnExit.ThemeAuthor = "Narwin";
            this.btnExit.ThemeName = "MetroLite";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tmrCopied
            // 
            this.tmrCopied.Interval = 1500;
            this.tmrCopied.Tick += new System.EventHandler(this.tmrCopied_Tick);
            // 
            // lblTotalDuration
            // 
            this.lblTotalDuration.AutoSize = true;
            this.lblTotalDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTotalDuration.Location = new System.Drawing.Point(1149, 17);
            this.lblTotalDuration.Name = "lblTotalDuration";
            this.lblTotalDuration.Size = new System.Drawing.Size(64, 18);
            this.lblTotalDuration.Style = MetroSet_UI.Design.Style.Light;
            this.lblTotalDuration.StyleManager = null;
            this.lblTotalDuration.TabIndex = 8;
            this.lblTotalDuration.Text = "00:00:00";
            this.lblTotalDuration.ThemeAuthor = "Narwin";
            this.lblTotalDuration.ThemeName = "MetroLite";
            this.lblTotalDuration.Click += new System.EventHandler(this.materialLabel1_Click);
            // 
            // lblTimeSoFar
            // 
            this.lblTimeSoFar.AutoSize = true;
            this.lblTimeSoFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTimeSoFar.Location = new System.Drawing.Point(316, 17);
            this.lblTimeSoFar.Name = "lblTimeSoFar";
            this.lblTimeSoFar.Size = new System.Drawing.Size(64, 18);
            this.lblTimeSoFar.Style = MetroSet_UI.Design.Style.Light;
            this.lblTimeSoFar.StyleManager = null;
            this.lblTimeSoFar.TabIndex = 6;
            this.lblTimeSoFar.Text = "00:00:00";
            this.lblTimeSoFar.ThemeAuthor = "Narwin";
            this.lblTimeSoFar.ThemeName = "MetroLite";
            // 
            // trkDuration
            // 
            this.trkDuration.BackColor = System.Drawing.Color.Transparent;
            this.trkDuration.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.trkDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkDuration.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.trkDuration.DisabledBorderColor = System.Drawing.Color.Empty;
            this.trkDuration.DisabledHandlerColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.trkDuration.DisabledValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.trkDuration.HandlerColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.trkDuration.Location = new System.Drawing.Point(386, 17);
            this.trkDuration.Maximum = 100;
            this.trkDuration.Minimum = 0;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(757, 16);
            this.trkDuration.Style = MetroSet_UI.Design.Style.Light;
            this.trkDuration.StyleManager = null;
            this.trkDuration.TabIndex = 7;
            this.trkDuration.ThemeAuthor = "Narwin";
            this.trkDuration.ThemeName = "MetroLite";
            this.trkDuration.Value = 0;
            this.trkDuration.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            // 
            // btnPrevTitle
            // 
            this.btnPrevTitle.BorderThickness = 0;
            this.btnPrevTitle.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnPrevTitle.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnPrevTitle.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnPrevTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPrevTitle.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnPrevTitle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnPrevTitle.HoverTextColor = System.Drawing.Color.White;
            this.btnPrevTitle.Image = null;
            imageSet3.Idle = PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
            imageSet3.Focus = PlexDL.Properties.Resources.baseline_skip_previous_black_18dp_white;
            this.btnPrevTitle.ImageSet = imageSet3;
            this.btnPrevTitle.ImageSize = new System.Drawing.Size(28, 28);
            this.btnPrevTitle.Location = new System.Drawing.Point(109, 7);
            this.btnPrevTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPrevTitle.Name = "btnPrevTitle";
            this.btnPrevTitle.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnPrevTitle.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnPrevTitle.NormalTextColor = System.Drawing.Color.Black;
            this.btnPrevTitle.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPrevTitle.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPrevTitle.PressTextColor = System.Drawing.Color.White;
            this.btnPrevTitle.Size = new System.Drawing.Size(44, 36);
            this.btnPrevTitle.Style = MetroSet_UI.Design.Style.Light;
            this.btnPrevTitle.StyleManager = null;
            this.btnPrevTitle.TabIndex = 2;
            this.btnPrevTitle.ThemeAuthor = "Narwin";
            this.btnPrevTitle.ThemeName = "MetroLite";
            this.btnPrevTitle.Click += new System.EventHandler(this.btnPrevTitle_Click);
            // 
            // btnNextTitle
            // 
            this.btnNextTitle.BorderThickness = 0;
            this.btnNextTitle.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnNextTitle.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnNextTitle.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnNextTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNextTitle.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnNextTitle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnNextTitle.HoverTextColor = System.Drawing.Color.White;
            this.btnNextTitle.Image = null;
            imageSet6.Idle = PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            imageSet6.Focus = PlexDL.Properties.Resources.baseline_skip_next_black_18dp_white;
            this.btnNextTitle.ImageSet = imageSet6;
            this.btnNextTitle.ImageSize = new System.Drawing.Size(28, 28);
            this.btnNextTitle.Location = new System.Drawing.Point(265, 7);
            this.btnNextTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnNextTitle.Name = "btnNextTitle";
            this.btnNextTitle.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnNextTitle.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnNextTitle.NormalTextColor = System.Drawing.Color.Black;
            this.btnNextTitle.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnNextTitle.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnNextTitle.PressTextColor = System.Drawing.Color.White;
            this.btnNextTitle.Size = new System.Drawing.Size(44, 36);
            this.btnNextTitle.Style = MetroSet_UI.Design.Style.Light;
            this.btnNextTitle.StyleManager = null;
            this.btnNextTitle.TabIndex = 5;
            this.btnNextTitle.ThemeAuthor = "Narwin";
            this.btnNextTitle.ThemeName = "MetroLite";
            this.btnNextTitle.Click += new System.EventHandler(this.btnNextTitle_Click);
            // 
            // btnStop
            // 
            this.btnStop.BorderThickness = 0;
            this.btnStop.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStop.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnStop.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnStop.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStop.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStop.HoverTextColor = System.Drawing.Color.White;
            this.btnStop.Image = null;
            imageSet2.Idle = PlexDL.Properties.Resources.baseline_stop_black_18dp;
            imageSet2.Focus = PlexDL.Properties.Resources.baseline_stop_black_18dp_white;
            this.btnStop.ImageSet = imageSet2;
            this.btnStop.ImageSize = new System.Drawing.Size(28, 28);
            this.btnStop.Location = new System.Drawing.Point(57, 7);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStop.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnStop.NormalTextColor = System.Drawing.Color.Black;
            this.btnStop.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStop.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStop.PressTextColor = System.Drawing.Color.White;
            this.btnStop.Size = new System.Drawing.Size(44, 36);
            this.btnStop.Style = MetroSet_UI.Design.Style.Light;
            this.btnStop.StyleManager = null;
            this.btnStop.TabIndex = 1;
            this.btnStop.ThemeAuthor = "Narwin";
            this.btnStop.ThemeName = "MetroLite";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSkipForward
            // 
            this.btnSkipForward.BorderThickness = 0;
            this.btnSkipForward.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSkipForward.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnSkipForward.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnSkipForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSkipForward.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSkipForward.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSkipForward.HoverTextColor = System.Drawing.Color.White;
            this.btnSkipForward.Image = null;
            imageSet5.Idle = PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            imageSet5.Focus = PlexDL.Properties.Resources.baseline_fast_forward_black_18dp_white;
            this.btnSkipForward.ImageSet = imageSet5;
            this.btnSkipForward.ImageSize = new System.Drawing.Size(28, 28);
            this.btnSkipForward.Location = new System.Drawing.Point(213, 7);
            this.btnSkipForward.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSkipForward.Name = "btnSkipForward";
            this.btnSkipForward.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSkipForward.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnSkipForward.NormalTextColor = System.Drawing.Color.Black;
            this.btnSkipForward.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSkipForward.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSkipForward.PressTextColor = System.Drawing.Color.White;
            this.btnSkipForward.Size = new System.Drawing.Size(44, 36);
            this.btnSkipForward.Style = MetroSet_UI.Design.Style.Light;
            this.btnSkipForward.StyleManager = null;
            this.btnSkipForward.TabIndex = 4;
            this.btnSkipForward.ThemeAuthor = "Narwin";
            this.btnSkipForward.ThemeName = "MetroLite";
            this.btnSkipForward.Click += new System.EventHandler(this.btnSkipForward_Click);
            // 
            // btnSkipBack
            // 
            this.btnSkipBack.BorderThickness = 0;
            this.btnSkipBack.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSkipBack.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnSkipBack.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnSkipBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSkipBack.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSkipBack.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSkipBack.HoverTextColor = System.Drawing.Color.White;
            this.btnSkipBack.Image = null;
            imageSet4.Idle = PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            imageSet4.Focus = PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp_white;
            this.btnSkipBack.ImageSet = imageSet4;
            this.btnSkipBack.ImageSize = new System.Drawing.Size(28, 28);
            this.btnSkipBack.Location = new System.Drawing.Point(161, 7);
            this.btnSkipBack.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSkipBack.Name = "btnSkipBack";
            this.btnSkipBack.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnSkipBack.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnSkipBack.NormalTextColor = System.Drawing.Color.Black;
            this.btnSkipBack.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSkipBack.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSkipBack.PressTextColor = System.Drawing.Color.White;
            this.btnSkipBack.Size = new System.Drawing.Size(44, 36);
            this.btnSkipBack.Style = MetroSet_UI.Design.Style.Light;
            this.btnSkipBack.StyleManager = null;
            this.btnSkipBack.TabIndex = 3;
            this.btnSkipBack.ThemeAuthor = "Narwin";
            this.btnSkipBack.ThemeName = "MetroLite";
            this.btnSkipBack.Click += new System.EventHandler(this.btnSkipBack_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.BorderThickness = 0;
            this.btnPlayPause.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnPlayPause.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnPlayPause.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnPlayPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPlayPause.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnPlayPause.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnPlayPause.HoverTextColor = System.Drawing.Color.White;
            this.btnPlayPause.Image = null;
            imageSet1.Idle = PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            imageSet1.Focus = PlexDL.Properties.Resources.baseline_play_arrow_black_18dp_white;
            this.btnPlayPause.ImageSet = imageSet1;
            this.btnPlayPause.ImageSize = new System.Drawing.Size(28, 28);
            this.btnPlayPause.Location = new System.Drawing.Point(4, 7);
            this.btnPlayPause.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnPlayPause.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnPlayPause.NormalTextColor = System.Drawing.Color.Black;
            this.btnPlayPause.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPlayPause.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPlayPause.PressTextColor = System.Drawing.Color.White;
            this.btnPlayPause.Size = new System.Drawing.Size(44, 36);
            this.btnPlayPause.Style = MetroSet_UI.Design.Style.Light;
            this.btnPlayPause.StyleManager = null;
            this.btnPlayPause.TabIndex = 0;
            this.btnPlayPause.ThemeAuthor = "Narwin";
            this.btnPlayPause.ThemeName = "MetroLite";
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.BackgroundColor = System.Drawing.Color.White;
            this.pnlControls.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.pnlControls.BorderThickness = 0;
            this.pnlControls.Controls.Add(this.btnPlayPause);
            this.pnlControls.Controls.Add(this.btnPrevTitle);
            this.pnlControls.Controls.Add(this.trkDuration);
            this.pnlControls.Controls.Add(this.btnNextTitle);
            this.pnlControls.Controls.Add(this.btnExit);
            this.pnlControls.Controls.Add(this.btnStop);
            this.pnlControls.Controls.Add(this.lblTotalDuration);
            this.pnlControls.Controls.Add(this.btnSkipForward);
            this.pnlControls.Controls.Add(this.lblTimeSoFar);
            this.pnlControls.Controls.Add(this.btnSkipBack);
            this.pnlControls.Location = new System.Drawing.Point(0, 795);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1280, 50);
            this.pnlControls.Style = MetroSet_UI.Design.Style.Light;
            this.pnlControls.StyleManager = null;
            this.pnlControls.TabIndex = 10;
            this.pnlControls.ThemeAuthor = "Narwin";
            this.pnlControls.ThemeName = "MetroLite";
            // 
            // Player
            // 
            this.AllowResize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unknown Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPlayer_FormClosing);
            this.Load += new System.EventHandler(this.frmPlayer_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlayer;
        private MetroSet_UI.Controls.MetroSetEllipse btnExit;
        private System.Windows.Forms.Timer tmrCopied;
        private MetroSet_UI.Controls.MetroSetEllipse btnPlayPause;
        private MetroSet_UI.Controls.MetroSetLabel lblTotalDuration;
        private MetroSet_UI.Controls.MetroSetLabel lblTimeSoFar;
        private System.Windows.Forms.Timer tmrRefreshUI;
        private MetroSet_UI.Controls.MetroSetTrackBar trkDuration;
        private MetroSet_UI.Controls.MetroSetEllipse btnSkipBack;
        private MetroSet_UI.Controls.MetroSetEllipse btnSkipForward;
        private MetroSet_UI.Controls.MetroSetEllipse btnStop;
        private MetroSet_UI.Controls.MetroSetEllipse btnNextTitle;
        private MetroSet_UI.Controls.MetroSetEllipse btnPrevTitle;
        private WMPLib.WindowsMediaPlayer axWindowsMediaPlayer1;
        private MetroSet_UI.Controls.MetroSetPanel pnlControls;
    }
}