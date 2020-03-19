using MetroSet_UI.Forms;
using System;
using System.Windows.Forms;
using PlexDL.Animation.CircularProgressBar;
using PlexDL.Animation.WinFormAnimation;

namespace PlexDL.WaitWindow
{
    /// <summary>
    /// The dialogue displayed by a WaitWindow instance.
    /// </summary>
    internal partial class WaitWindowGUI : MetroSetForm
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

        internal object _Result;
        internal Exception _Error;
        private CircularProgressBar ProgressMain;
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
        }

        internal void Cancel()
        {
            Invoke(new MethodInvoker(Close), null);
        }

        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private readonly System.ComponentModel.IContainer components = null;

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
            MessageLabel = new MetroSet_UI.Controls.MetroSetLabel();
            styleMain = new MetroSet_UI.StyleManager();
            ProgressMain = new CircularProgressBar();
            SuspendLayout();
            // 
            // MessageLabel
            // 
            MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, (byte)0);
            MessageLabel.Location = new System.Drawing.Point(127, 84);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new System.Drawing.Size(174, 86);
            MessageLabel.Style = MetroSet_UI.Design.Style.Light;
            MessageLabel.StyleManager = styleMain;
            MessageLabel.TabIndex = 2;
            MessageLabel.Text = "Working on it...";
            MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            MessageLabel.ThemeAuthor = null;
            MessageLabel.ThemeName = null;
            // 
            // styleMain
            // 
            styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            styleMain.MetroForm = this;
            styleMain.Style = MetroSet_UI.Design.Style.Light;
            styleMain.ThemeAuthor = null;
            styleMain.ThemeName = null;
            // 
            // ProgressMain
            // 
            ProgressMain.AnimationFunction = KnownAnimationFunctions.QuadraticEaseInOut;
            ProgressMain.AnimationSpeed = 500;
            ProgressMain.BackColor = System.Drawing.Color.Transparent;
            ProgressMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            ProgressMain.ForeColor = System.Drawing.Color.FromArgb((int)(byte)64, (int)(byte)64, (int)(byte)64);
            ProgressMain.InnerColor = System.Drawing.Color.FromArgb((int)(byte)224, (int)(byte)224, (int)(byte)224);
            ProgressMain.InnerMargin = 2;
            ProgressMain.InnerWidth = -1;
            ProgressMain.Location = new System.Drawing.Point(15, 73);
            ProgressMain.MarqueeAnimationSpeed = 2000;
            ProgressMain.Name = "ProgressMain";
            ProgressMain.OuterColor = System.Drawing.Color.Gray;
            ProgressMain.OuterMargin = -25;
            ProgressMain.OuterWidth = 26;
            ProgressMain.ProgressColor = System.Drawing.Color.FromArgb((int)(byte)65, (int)(byte)177, (int)(byte)225);
            ProgressMain.ProgressWidth = 25;
            ProgressMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            ProgressMain.Size = new System.Drawing.Size(108, 108);
            ProgressMain.StartAngle = 270;
            ProgressMain.Style = ProgressBarStyle.Marquee;
            ProgressMain.SubscriptColor = System.Drawing.Color.FromArgb((int)(byte)166, (int)(byte)166, (int)(byte)166);
            ProgressMain.SubscriptMargin = new Padding(10, -35, 0, 0);
            ProgressMain.SubscriptText = ".23";
            ProgressMain.SuperscriptColor = System.Drawing.Color.FromArgb((int)(byte)166, (int)(byte)166, (int)(byte)166);
            ProgressMain.SuperscriptMargin = new Padding(10, 35, 0, 0);
            ProgressMain.SuperscriptText = "°C";
            ProgressMain.TabIndex = 3;
            ProgressMain.TextMargin = new Padding(8, 8, 0, 0);
            ProgressMain.Value = 68;
            // 
            // WaitWindowGUI
            // 
            AllowResize = false;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.Color.WhiteSmoke;
            ClientSize = new System.Drawing.Size(316, 195);
            Controls.Add(ProgressMain);
            Controls.Add(MessageLabel);
            DropShadowEffect = false;
            Name = "WaitWindowGUI";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            StyleManager = styleMain;
            Text = "Please Wait";
            ThemeAuthor = null;
            ThemeName = null;
            ResumeLayout(false);
        }

        public MetroSet_UI.Controls.MetroSetLabel MessageLabel;
        private MetroSet_UI.StyleManager styleMain;
    }
}