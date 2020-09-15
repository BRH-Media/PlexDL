using System.Windows.Forms;

namespace PlexDL.WaitWindow
{
    public partial class WaitWindowGUI : Form
    {
        /// <summary>
        ///     Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        ///     This method is required for Windows Forms designer support.
        ///     Do not change the method contents inside the source code editor. The Forms designer might
        ///     not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.tmrDots = new System.Windows.Forms.Timer(this.components);
            this.ProgressMain = new PlexDL.Common.Components.CircularProgressBar();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(117, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(196, 129);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Working on it";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDots
            // 
            this.tmrDots.Interval = 200;
            this.tmrDots.Tick += new System.EventHandler(this.TmrDots_Tick);
            // 
            // ProgressMain
            // 
            this.ProgressMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProgressMain.AnimationFunction = PlexDL.Animation.WinFormAnimation.KnownAnimationFunctions.QuadraticEaseInOut;
            this.ProgressMain.AnimationSpeed = 500;
            this.ProgressMain.BackColor = System.Drawing.Color.Transparent;
            this.ProgressMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.ProgressMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ProgressMain.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProgressMain.InnerMargin = 2;
            this.ProgressMain.InnerWidth = -1;
            this.ProgressMain.Location = new System.Drawing.Point(3, 10);
            this.ProgressMain.MarqueeAnimationSpeed = 2000;
            this.ProgressMain.Name = "ProgressMain";
            this.ProgressMain.OuterColor = System.Drawing.Color.Gray;
            this.ProgressMain.OuterMargin = -25;
            this.ProgressMain.OuterWidth = 26;
            this.ProgressMain.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ProgressMain.ProgressWidth = 18;
            this.ProgressMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.ProgressMain.Size = new System.Drawing.Size(108, 108);
            this.ProgressMain.StartAngle = 270;
            this.ProgressMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressMain.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ProgressMain.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.ProgressMain.SubscriptText = "";
            this.ProgressMain.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ProgressMain.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.ProgressMain.SuperscriptText = "";
            this.ProgressMain.TabIndex = 3;
            this.ProgressMain.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.ProgressMain.Value = 68;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.ProgressMain, 0, 0);
            this.tlpMain.Controls.Add(this.MessageLabel, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(316, 129);
            this.tlpMain.TabIndex = 4;
            // 
            // WaitWindowGUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(316, 129);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitWindowGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Working";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitWindowGUI_FormClosing);
            this.Load += new System.EventHandler(this.WaitWindowGUI_Load);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tlpMain;
    }
}
