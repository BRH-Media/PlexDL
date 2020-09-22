using PlexDL.Common.Components;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace PlexDL.WaitWindow
{
    /// <summary>
    ///     The dialogue displayed by a WaitWindow instance.
    /// </summary>
    public partial class WaitWindowGUI
    {
        private readonly WaitWindow _Parent;
        internal Exception _Error;

        private int _dotCount = 0;

        internal object _Result;

        private IContainer components;

        private string LabelText = "";

        public Label MessageLabel;
        private CircularProgressBar ProgressMain;
        private IAsyncResult threadResult;
        private Timer tmrDots;

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
            _Parent._WorkerMethod?.Invoke(this, e);

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

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            _dotCount = 0;
            tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDots.Stop();
        }

        private void TmrDots_Tick(object sender, EventArgs e)
        {
            if (_dotCount < 3)
            {
                _dotCount++;
                MessageLabel.Text += '.';
            }
            else
            {
                _dotCount = 0;
                MessageLabel.Text = LabelText;
            }
        }

        private delegate T FunctionInvoker<T>();
    }
}