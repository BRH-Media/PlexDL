using System.Windows.Forms;

namespace PlexDL.WaitWindow
{
    public partial class WaitWindowGui : Form
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
            this._tmrDots = new System.Windows.Forms.Timer(this.components);
            this._progressMain = new PlexDL.Common.Components.CircularProgressBar();
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
            this._tmrDots.Interval = 200;
            this._tmrDots.Tick += new System.EventHandler(this.TmrDots_Tick);
            // 
            // ProgressMain
            // 
            this._progressMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._progressMain.AnimationFunction = PlexDL.Animation.WinFormAnimation.KnownAnimationFunctions.QuadraticEaseInOut;
            this._progressMain.AnimationSpeed = 500;
            this._progressMain.BackColor = System.Drawing.Color.Transparent;
            this._progressMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this._progressMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._progressMain.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._progressMain.InnerMargin = 2;
            this._progressMain.InnerWidth = -1;
            this._progressMain.Location = new System.Drawing.Point(3, 10);
            this._progressMain.MarqueeAnimationSpeed = 2000;
            this._progressMain.Name = "_progressMain";
            this._progressMain.OuterColor = System.Drawing.Color.Gray;
            this._progressMain.OuterMargin = -25;
            this._progressMain.OuterWidth = 26;
            this._progressMain.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this._progressMain.ProgressWidth = 18;
            this._progressMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this._progressMain.Size = new System.Drawing.Size(108, 108);
            this._progressMain.StartAngle = 270;
            this._progressMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this._progressMain.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this._progressMain.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this._progressMain.SubscriptText = "";
            this._progressMain.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this._progressMain.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this._progressMain.SuperscriptText = "";
            this._progressMain.TabIndex = 3;
            this._progressMain.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._progressMain.Value = 68;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this._progressMain, 0, 0);
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
            this.Name = "WaitWindowGui";
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
