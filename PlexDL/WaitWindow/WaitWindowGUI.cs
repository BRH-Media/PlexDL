using MetroSet_UI.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using PlexDL.Animation.CircularProgressBar;
using PlexDL.Animation.WinFormAnimation;

namespace PlexDL.WaitWindow
{
    /// <summary>
    /// The dialogue displayed by a WaitWindow instance.
    /// </summary>
    internal partial class WaitWindowGUI : Form
    {
        public WaitWindowGUI(WaitWindow parent)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            _Parent = parent;

            //	Position the window in the top right of the main screen.
            Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 32;
        }

        private readonly WaitWindow _Parent;

        private delegate T FunctionInvoker<T>();

        private int dotCount = 0;
        private string LabelText = "";

        internal object _Result;
        internal Exception _Error;
        private CircularProgressBar ProgressMain;
        private System.Windows.Forms.Timer tmrDots;
        private IAsyncResult threadResult;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //	Paint a 3D border
            //ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Raised);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //   Create Delegate
            var threadController = new FunctionInvoker<object>(DoWork);

            //   Execute on secondary thread.
            threadResult = threadController.BeginInvoke(WorkComplete, threadController);
        }

        internal object DoWork()
        {
            //	Invoke the worker method and return any results.
            var e = new WaitWindowEventArgs(_Parent, _Parent._Args);
            if (_Parent._WorkerMethod != null)
                _Parent._WorkerMethod(this, e);

            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            if (!IsDisposed)
            {
                if (InvokeRequired)
                {
                    Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(WorkComplete), results);
                }
                else
                {
                    //	Capture the result
                    try
                    {
                        _Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
                    }
                    catch (Exception ex)
                    {
                        //	Grab the Exception for rethrowing after the WaitWindow has closed.
                        _Error = ex;
                    }

                    Close();
                }
            }
        }

        internal void SetMessage(string message)
        {
            MessageLabel.Text = message;
            LabelText = message;
        }

        internal void Cancel()
        {
            Invoke(new MethodInvoker(Close), null);
        }

        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Disposes resources used by the form.
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
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ProgressMain = new PlexDL.Animation.CircularProgressBar.CircularProgressBar();
            this.tmrDots = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(130, 12);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(174, 108);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Working on it";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressMain
            // 
            this.ProgressMain.AnimationFunction = PlexDL.Animation.WinFormAnimation.KnownAnimationFunctions.QuadraticEaseInOut;
            this.ProgressMain.AnimationSpeed = 500;
            this.ProgressMain.BackColor = System.Drawing.Color.Transparent;
            this.ProgressMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.ProgressMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ProgressMain.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProgressMain.InnerMargin = 2;
            this.ProgressMain.InnerWidth = -1;
            this.ProgressMain.Location = new System.Drawing.Point(12, 12);
            this.ProgressMain.MarqueeAnimationSpeed = 2000;
            this.ProgressMain.Name = "ProgressMain";
            this.ProgressMain.OuterColor = System.Drawing.Color.Gray;
            this.ProgressMain.OuterMargin = -25;
            this.ProgressMain.OuterWidth = 26;
            this.ProgressMain.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProgressMain.ProgressWidth = 25;
            this.ProgressMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.ProgressMain.Size = new System.Drawing.Size(108, 108);
            this.ProgressMain.StartAngle = 270;
            this.ProgressMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressMain.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ProgressMain.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.ProgressMain.SubscriptText = ".23";
            this.ProgressMain.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ProgressMain.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.ProgressMain.SuperscriptText = "°C";
            this.ProgressMain.TabIndex = 3;
            this.ProgressMain.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.ProgressMain.Value = 68;
            // 
            // tmrDots
            // 
            this.tmrDots.Interval = 200;
            this.tmrDots.Tick += new System.EventHandler(this.tmrDots_Tick);
            // 
            // WaitWindowGUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(316, 129);
            this.ControlBox = false;
            this.Controls.Add(this.ProgressMain);
            this.Controls.Add(this.MessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitWindowGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Working";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitWindowGUI_FormClosing);
            this.Load += new System.EventHandler(this.WaitWindowGUI_Load);
            this.ResumeLayout(false);

        }

        public System.Windows.Forms.Label MessageLabel;

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDots.Stop();
        }

        private void tmrDots_Tick(object sender, EventArgs e)
        {
            if (dotCount < 3)
            {
                dotCount++;
                MessageLabel.Text += @".";
            }
            else
            {
                dotCount = 0;
                MessageLabel.Text = LabelText;
            }
        }
    }
}