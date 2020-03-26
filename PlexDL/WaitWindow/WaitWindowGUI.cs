using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PlexDL.Animation.WinFormAnimation;
using PlexDL.Common.Components;
using Timer = System.Windows.Forms.Timer;

namespace PlexDL.WaitWindow
{
    /// <summary>
    /// The dialogue displayed by a WaitWindow instance.
    /// </summary>
    internal class WaitWindowGUI : Form
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

        private int dotCount;
        private string LabelText = "";

        internal object _Result;
        internal Exception _Error;
        private CircularProgressBar ProgressMain;
        private Timer tmrDots;
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

        private IContainer components;

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
            components = new Container();
            MessageLabel = new Label();
            tmrDots = new Timer(components);
            ProgressMain = new CircularProgressBar();
            SuspendLayout();
            //
            // MessageLabel
            //
            MessageLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MessageLabel.Location = new Point(130, 12);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(174, 108);
            MessageLabel.TabIndex = 2;
            MessageLabel.Text = "Working on it";
            MessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // tmrDots
            //
            tmrDots.Interval = 200;
            tmrDots.Tick += tmrDots_Tick;
            //
            // ProgressMain
            //
            ProgressMain.AnimationFunction = KnownAnimationFunctions.QuadraticEaseInOut;
            ProgressMain.AnimationSpeed = 500;
            ProgressMain.BackColor = Color.Transparent;
            ProgressMain.Font = new Font("Microsoft Sans Serif", 72F, FontStyle.Bold);
            ProgressMain.ForeColor = Color.FromArgb(64, 64, 64);
            ProgressMain.InnerColor = Color.FromArgb(224, 224, 224);
            ProgressMain.InnerMargin = 2;
            ProgressMain.InnerWidth = -1;
            ProgressMain.Location = new Point(12, 12);
            ProgressMain.MarqueeAnimationSpeed = 2000;
            ProgressMain.Name = "ProgressMain";
            ProgressMain.OuterColor = Color.Gray;
            ProgressMain.OuterMargin = -25;
            ProgressMain.OuterWidth = 26;
            ProgressMain.ProgressColor = Color.FromArgb(65, 177, 225);
            ProgressMain.ProgressWidth = 25;
            ProgressMain.SecondaryFont = new Font("Microsoft Sans Serif", 36F);
            ProgressMain.Size = new Size(108, 108);
            ProgressMain.StartAngle = 270;
            ProgressMain.Style = ProgressBarStyle.Marquee;
            ProgressMain.SubscriptColor = Color.FromArgb(166, 166, 166);
            ProgressMain.SubscriptMargin = new Padding(10, -35, 0, 0);
            ProgressMain.SubscriptText = ".23";
            ProgressMain.SuperscriptColor = Color.FromArgb(166, 166, 166);
            ProgressMain.SuperscriptMargin = new Padding(10, 35, 0, 0);
            ProgressMain.SuperscriptText = "°C";
            ProgressMain.TabIndex = 3;
            ProgressMain.TextMargin = new Padding(8, 8, 0, 0);
            ProgressMain.Value = 68;
            //
            // WaitWindowGUI
            //
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(316, 129);
            ControlBox = false;
            Controls.Add(ProgressMain);
            Controls.Add(MessageLabel);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WaitWindowGUI";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Working";
            FormClosing += WaitWindowGUI_FormClosing;
            Load += WaitWindowGUI_Load;
            ResumeLayout(false);

        }

        public Label MessageLabel;

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