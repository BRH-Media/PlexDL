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
            MetroSet_UI.Extensions.ImageSet imageSet3 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet4 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet5 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet6 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet7 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet2 = new MetroSet_UI.Extensions.ImageSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.btnExit = new MetroSet_UI.Controls.MetroSetEllipse();
            this.styleMain = new MetroSet_UI.StyleManager();
            this.tmrCopied = new System.Windows.Forms.Timer(this.components);
            this.lblTotalDuration = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblTimeSoFar = new MetroSet_UI.Controls.MetroSetLabel();
            this.tmrRefreshUI = new System.Windows.Forms.Timer(this.components);
            this.trkDuration = new System.Windows.Forms.TrackBar();
            this.btnPrevTitle = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnNextTitle = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnStop = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnSkipForward = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnSkipBack = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnPlayPause = new MetroSet_UI.Controls.MetroSetEllipse();
            this.pnlControls = new MetroSet_UI.Controls.MetroSetPanel();
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
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
            this.btnExit.Image = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            imageSet1.Focus = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp_white;
            imageSet1.Idle = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.ImageSet = imageSet1;
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
            this.btnExit.StyleManager = this.styleMain;
            this.btnExit.TabIndex = 9;
            this.btnExit.ThemeAuthor = null;
            this.btnExit.ThemeName = null;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
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
            this.lblTotalDuration.Location = new System.Drawing.Point(1149, 17);
            this.lblTotalDuration.Name = "lblTotalDuration";
            this.lblTotalDuration.Size = new System.Drawing.Size(64, 18);
            this.lblTotalDuration.Style = MetroSet_UI.Design.Style.Light;
            this.lblTotalDuration.StyleManager = this.styleMain;
            this.lblTotalDuration.TabIndex = 8;
            this.lblTotalDuration.Text = "00:00:00";
            this.lblTotalDuration.ThemeAuthor = null;
            this.lblTotalDuration.ThemeName = null;
            // 
            // lblTimeSoFar
            // 
            this.lblTimeSoFar.AutoSize = true;
            this.lblTimeSoFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblTimeSoFar.Location = new System.Drawing.Point(316, 17);
            this.lblTimeSoFar.Name = "lblTimeSoFar";
            this.lblTimeSoFar.Size = new System.Drawing.Size(64, 18);
            this.lblTimeSoFar.Style = MetroSet_UI.Design.Style.Light;
            this.lblTimeSoFar.StyleManager = this.styleMain;
            this.lblTimeSoFar.TabIndex = 6;
            this.lblTimeSoFar.Text = "00:00:00";
            this.lblTimeSoFar.ThemeAuthor = null;
            this.lblTimeSoFar.ThemeName = null;
            // 
            // trkDuration
            // 
            this.trkDuration.AutoSize = false;
            this.trkDuration.BackColor = this.BackColor;
            this.trkDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trkDuration.Location = new System.Drawing.Point(386, 17);
            this.trkDuration.Maximum = 100;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(757, 16);
            this.trkDuration.TabIndex = 7;
            this.trkDuration.TickStyle = System.Windows.Forms.TickStyle.None;
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
            this.btnPrevTitle.Image = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
            imageSet3.Focus = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp_white;
            imageSet3.Idle = global::PlexDL.Properties.Resources.baseline_skip_previous_black_18dp;
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
            this.btnPrevTitle.StyleManager = this.styleMain;
            this.btnPrevTitle.TabIndex = 2;
            this.btnPrevTitle.ThemeAuthor = null;
            this.btnPrevTitle.ThemeName = null;
            this.btnPrevTitle.Click += new System.EventHandler(this.BtnPrevTitle_Click);
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
            this.btnNextTitle.Image = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            imageSet4.Focus = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp_white;
            imageSet4.Idle = global::PlexDL.Properties.Resources.baseline_skip_next_black_18dp;
            this.btnNextTitle.ImageSet = imageSet4;
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
            this.btnNextTitle.StyleManager = this.styleMain;
            this.btnNextTitle.TabIndex = 5;
            this.btnNextTitle.ThemeAuthor = null;
            this.btnNextTitle.ThemeName = null;
            this.btnNextTitle.Click += new System.EventHandler(this.BtnNextTitle_Click);
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
            this.btnStop.Image = global::PlexDL.Properties.Resources.baseline_stop_black_18dp;
            imageSet5.Focus = global::PlexDL.Properties.Resources.baseline_stop_black_18dp_white;
            imageSet5.Idle = global::PlexDL.Properties.Resources.baseline_stop_black_18dp;
            this.btnStop.ImageSet = imageSet5;
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
            this.btnStop.StyleManager = this.styleMain;
            this.btnStop.TabIndex = 1;
            this.btnStop.ThemeAuthor = null;
            this.btnStop.ThemeName = null;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
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
            this.btnSkipForward.Image = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            imageSet6.Focus = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp_white;
            imageSet6.Idle = global::PlexDL.Properties.Resources.baseline_fast_forward_black_18dp;
            this.btnSkipForward.ImageSet = imageSet6;
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
            this.btnSkipForward.StyleManager = this.styleMain;
            this.btnSkipForward.TabIndex = 4;
            this.btnSkipForward.ThemeAuthor = null;
            this.btnSkipForward.ThemeName = null;
            this.btnSkipForward.Click += new System.EventHandler(this.BtnSkipForward_Click);
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
            this.btnSkipBack.Image = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            imageSet7.Focus = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp_white;
            imageSet7.Idle = global::PlexDL.Properties.Resources.baseline_fast_rewind_black_18dp;
            this.btnSkipBack.ImageSet = imageSet7;
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
            this.btnSkipBack.StyleManager = this.styleMain;
            this.btnSkipBack.TabIndex = 3;
            this.btnSkipBack.ThemeAuthor = null;
            this.btnSkipBack.ThemeName = null;
            this.btnSkipBack.Click += new System.EventHandler(this.BtnSkipBack_Click);
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
            this.btnPlayPause.Image = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            imageSet2.Focus = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp_white;
            imageSet2.Idle = global::PlexDL.Properties.Resources.baseline_play_arrow_black_18dp;
            this.btnPlayPause.ImageSet = imageSet2;
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
            this.btnPlayPause.StyleManager = this.styleMain;
            this.btnPlayPause.TabIndex = 0;
            this.btnPlayPause.ThemeAuthor = null;
            this.btnPlayPause.ThemeName = null;
            this.btnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
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
            this.pnlControls.StyleManager = this.styleMain;
            this.pnlControls.TabIndex = 10;
            this.pnlControls.ThemeAuthor = null;
            this.pnlControls.ThemeName = null;
            // 
            // ctrlMain
            // 
            this.ctrlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlMain.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ctrlMain.CloseHoverForeColor = System.Drawing.Color.White;
            this.ctrlMain.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.DisabledForeColor = System.Drawing.Color.DimGray;
            this.ctrlMain.Location = new System.Drawing.Point(1178, 2);
            this.ctrlMain.MaximizeBox = true;
            this.ctrlMain.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeBox = true;
            this.ctrlMain.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.Name = "ctrlMain";
            this.ctrlMain.Size = new System.Drawing.Size(100, 25);
            this.ctrlMain.Style = MetroSet_UI.Design.Style.Light;
            this.ctrlMain.StyleManager = this.styleMain;
            this.ctrlMain.TabIndex = 11;
            this.ctrlMain.Text = "Player";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // Player
            // 
            this.AllowResize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Unknown Title";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FrmPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
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
        private new System.Windows.Forms.TrackBar trkDuration;
        private MetroSet_UI.Controls.MetroSetEllipse btnSkipBack;
        private MetroSet_UI.Controls.MetroSetEllipse btnSkipForward;
        private MetroSet_UI.Controls.MetroSetEllipse btnStop;
        private MetroSet_UI.Controls.MetroSetEllipse btnNextTitle;
        private MetroSet_UI.Controls.MetroSetEllipse btnPrevTitle;
        private WMPLib.WindowsMediaPlayer axWindowsMediaPlayer1;
        private MetroSet_UI.Controls.MetroSetPanel pnlControls;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
    }
}