using MetroSet_UI.Forms;
using System;
using System.Windows.Forms;

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

            this._Parent = parent;

            //	Position the window in the top right of the main screen.
            this.Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width - 32;
        }

        private WaitWindow _Parent;

        private delegate T FunctionInvoker<T>();

        internal object _Result;
        internal Exception _Error;
        private PVS.MediaPlayer.VideoDisplay videoDisplay1;
        private CircularProgressBar.CircularProgressBar pbMain;
        private IAsyncResult threadResult;

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            //	Paint a 3D border
            //ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Raised);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //   Create Delegate
            FunctionInvoker<object> threadController = new FunctionInvoker<object>(this.DoWork);

            //   Execute on secondary thread.
            this.threadResult = threadController.BeginInvoke(this.WorkComplete, threadController);
        }

        internal object DoWork()
        {
            //	Invoke the worker method and return any results.
            WaitWindowEventArgs e = new WaitWindowEventArgs(this._Parent, this._Parent._Args);
            if ((this._Parent._WorkerMethod != null))
            {
                this._Parent._WorkerMethod(this, e);
            }
            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(this.WorkComplete), results);
                }
                else
                {
                    //	Capture the result
                    try
                    {
                        this._Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
                    }
                    catch (Exception ex)
                    {
                        //	Grab the Exception for rethrowing after the WaitWindow has closed.
                        this._Error = ex;
                    }
                    this.Close();
                }
            }
        }

        internal void SetMessage(string message)
        {
            this.MessageLabel.Text = message;
        }

        internal void Cancel()
        {
            this.Invoke(new MethodInvoker(this.Close), null);
        }

        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.MessageLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.styleMain = new MetroSet_UI.StyleManager();
            this.pbMain = new CircularProgressBar.CircularProgressBar();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(128, 97);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(173, 86);
            this.MessageLabel.Style = MetroSet_UI.Design.Style.Light;
            this.MessageLabel.StyleManager = this.styleMain;
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Updating Data";
            this.MessageLabel.ThemeAuthor = null;
            this.MessageLabel.ThemeName = null;
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
            // 
            // pbMain
            // 
            this.pbMain.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.QuadraticEaseInOut;
            this.pbMain.AnimationSpeed = 500;
            this.pbMain.BackColor = System.Drawing.Color.Transparent;
            this.pbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.pbMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbMain.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbMain.InnerMargin = 2;
            this.pbMain.InnerWidth = -1;
            this.pbMain.Location = new System.Drawing.Point(15, 73);
            this.pbMain.MarqueeAnimationSpeed = 2000;
            this.pbMain.Name = "pbMain";
            this.pbMain.OuterColor = System.Drawing.Color.Gray;
            this.pbMain.OuterMargin = -25;
            this.pbMain.OuterWidth = 26;
            this.pbMain.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.pbMain.ProgressWidth = 25;
            this.pbMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.pbMain.Size = new System.Drawing.Size(107, 107);
            this.pbMain.StartAngle = 270;
            this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbMain.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.pbMain.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.pbMain.SubscriptText = ".23";
            this.pbMain.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.pbMain.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.pbMain.SuperscriptText = "°C";
            this.pbMain.TabIndex = 3;
            this.pbMain.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.pbMain.Value = 68;
            // 
            // WaitWindowGUI
            // 
            this.AllowResize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(316, 195);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.MessageLabel);
            this.DropShadowEffect = false;
            this.Name = "WaitWindowGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Please Wait";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.ResumeLayout(false);

        }

        public MetroSet_UI.Controls.MetroSetLabel MessageLabel;
        private MetroSet_UI.StyleManager styleMain;
    }
}